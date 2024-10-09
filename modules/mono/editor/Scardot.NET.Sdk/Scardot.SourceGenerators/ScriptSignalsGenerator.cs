using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

// TODO:
//   Determine a proper way to emit the signal.
//   'Emit(nameof(TheEvent))' creates a StringName every time and has the overhead of string marshaling.
//   I haven't decided on the best option yet. Some possibilities:
//     - Expose the generated StringName fields to the user, for use with 'Emit(...)'.
//     - Generate a 'EmitSignalName' method for each event signal.

namespace scardot.SourceGenerators
{
    [Generator]
    public class ScriptSignalsGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
        }

        public void Execute(GeneratorExecutionContext context)
        {
            if (context.IsscardotSourceGeneratorDisabled("ScriptSignals"))
                return;

            INamedTypeSymbol[] scardotClasses = context
                .Compilation.SyntaxTrees
                .SelectMany(tree =>
                    tree.GetRoot().DescendantNodes()
                        .OfType<ClassDeclarationSyntax>()
                        .SelectscardotScriptClasses(context.Compilation)
                        // Report and skip non-partial classes
                        .Where(x =>
                        {
                            if (x.cds.IsPartial())
                            {
                                if (x.cds.IsNested() && !x.cds.AreAllOuterTypesPartial(out _))
                                {
                                    return false;
                                }

                                return true;
                            }

                            return false;
                        })
                        .Select(x => x.symbol)
                )
                .Distinct<INamedTypeSymbol>(SymbolEqualityComparer.Default)
                .ToArray();

            if (scardotClasses.Length > 0)
            {
                var typeCache = new MarshalUtils.TypeCache(context.Compilation);

                foreach (var scardotClass in scardotClasses)
                {
                    VisitscardotScriptClass(context, typeCache, scardotClass);
                }
            }
        }

        internal static string SignalDelegateSuffix = "EventHandler";

        private static void VisitscardotScriptClass(
            GeneratorExecutionContext context,
            MarshalUtils.TypeCache typeCache,
            INamedTypeSymbol symbol
        )
        {
            INamespaceSymbol namespaceSymbol = symbol.ContainingNamespace;
            string classNs = namespaceSymbol != null && !namespaceSymbol.IsGlobalNamespace ?
                namespaceSymbol.FullQualifiedNameOmitGlobal() :
                string.Empty;
            bool hasNamespace = classNs.Length != 0;

            bool isInnerClass = symbol.ContainingType != null;

            string uniqueHint = symbol.FullQualifiedNameOmitGlobal().SanitizeQualifiedNameForUniqueHint()
                                + "_ScriptSignals.generated";

            var source = new StringBuilder();

            source.Append("using scardot;\n");
            source.Append("using scardot.NativeInterop;\n");
            source.Append("\n");

            if (hasNamespace)
            {
                source.Append("namespace ");
                source.Append(classNs);
                source.Append(" {\n\n");
            }

            if (isInnerClass)
            {
                var containingType = symbol.ContainingType;
                AppendPartialContainingTypeDeclarations(containingType);

                void AppendPartialContainingTypeDeclarations(INamedTypeSymbol? containingType)
                {
                    if (containingType == null)
                        return;

                    AppendPartialContainingTypeDeclarations(containingType.ContainingType);

                    source.Append("partial ");
                    source.Append(containingType.GetDeclarationKeyword());
                    source.Append(" ");
                    source.Append(containingType.NameWithTypeParameters());
                    source.Append("\n{\n");
                }
            }

            source.Append("partial class ");
            source.Append(symbol.NameWithTypeParameters());
            source.Append("\n{\n");

            var members = symbol.GetMembers();

            var signalDelegateSymbols = members
                .Where(s => s.Kind == SymbolKind.NamedType)
                .Cast<INamedTypeSymbol>()
                .Where(namedTypeSymbol => namedTypeSymbol.TypeKind == TypeKind.Delegate)
                .Where(s => s.GetAttributes()
                    .Any(a => a.AttributeClass?.IsscardotSignalAttribute() ?? false));

            List<scardotSignalDelegateData> scardotSignalDelegates = new();

            foreach (var signalDelegateSymbol in signalDelegateSymbols)
            {
                if (!signalDelegateSymbol.Name.EndsWith(SignalDelegateSuffix))
                {
                    context.ReportDiagnostic(Diagnostic.Create(
                        Common.SignalDelegateMissingSuffixRule,
                        signalDelegateSymbol.Locations.FirstLocationWithSourceTreeOrDefault(),
                        signalDelegateSymbol.ToDisplayString()
                    ));
                    continue;
                }

                string signalName = signalDelegateSymbol.Name;
                signalName = signalName.Substring(0, signalName.Length - SignalDelegateSuffix.Length);

                var invokeMethodData = signalDelegateSymbol
                    .DelegateInvokeMethod?.HasscardotCompatibleSignature(typeCache);

                if (invokeMethodData == null)
                {
                    if (signalDelegateSymbol.DelegateInvokeMethod is IMethodSymbol methodSymbol)
                    {
                        foreach (var parameter in methodSymbol.Parameters)
                        {
                            if (parameter.RefKind != RefKind.None)
                            {
                                context.ReportDiagnostic(Diagnostic.Create(
                                    Common.SignalParameterTypeNotSupportedRule,
                                    parameter.Locations.FirstLocationWithSourceTreeOrDefault(),
                                    parameter.ToDisplayString()
                                ));
                                continue;
                            }

                            var marshalType = MarshalUtils.ConvertManagedTypeToMarshalType(parameter.Type, typeCache);
                            if (marshalType == null)
                            {
                                context.ReportDiagnostic(Diagnostic.Create(
                                    Common.SignalParameterTypeNotSupportedRule,
                                    parameter.Locations.FirstLocationWithSourceTreeOrDefault(),
                                    parameter.ToDisplayString()
                                ));
                            }
                        }

                        if (!methodSymbol.ReturnsVoid)
                        {
                            context.ReportDiagnostic(Diagnostic.Create(
                                Common.SignalDelegateSignatureMustReturnVoidRule,
                                signalDelegateSymbol.Locations.FirstLocationWithSourceTreeOrDefault(),
                                signalDelegateSymbol.ToDisplayString()
                            ));
                        }
                    }

                    continue;
                }

                scardotSignalDelegates.Add(new(signalName, signalDelegateSymbol, invokeMethodData.Value));
            }

            source.Append("#pragma warning disable CS0109 // Disable warning about redundant 'new' keyword\n");

            source.Append("    /// <summary>\n")
                .Append("    /// Cached StringNames for the signals contained in this class, for fast lookup.\n")
                .Append("    /// </summary>\n");

            source.Append(
                $"    public new class SignalName : {symbol.BaseType!.FullQualifiedNameIncludeGlobal()}.SignalName {{\n");

            // Generate cached StringNames for methods and properties, for fast lookup

            foreach (var signalDelegate in scardotSignalDelegates)
            {
                string signalName = signalDelegate.Name;

                source.Append("        /// <summary>\n")
                    .Append("        /// Cached name for the '")
                    .Append(signalName)
                    .Append("' signal.\n")
                    .Append("        /// </summary>\n");

                source.Append("        public new static readonly global::scardot.StringName @");
                source.Append(signalName);
                source.Append(" = \"");
                source.Append(signalName);
                source.Append("\";\n");
            }

            source.Append("    }\n"); // class scardotInternal

            // Generate GetscardotSignalList

            if (scardotSignalDelegates.Count > 0)
            {
                const string ListType = "global::System.Collections.Generic.List<global::scardot.Bridge.MethodInfo>";

                source.Append("    /// <summary>\n")
                    .Append("    /// Get the signal information for all the signals declared in this class.\n")
                    .Append("    /// This method is used by scardot to register the available signals in the editor.\n")
                    .Append("    /// Do not call this method.\n")
                    .Append("    /// </summary>\n");

                source.Append("    [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]\n");

                source.Append("    internal new static ")
                    .Append(ListType)
                    .Append(" GetscardotSignalList()\n    {\n");

                source.Append("        var signals = new ")
                    .Append(ListType)
                    .Append("(")
                    .Append(scardotSignalDelegates.Count)
                    .Append(");\n");

                foreach (var signalDelegateData in scardotSignalDelegates)
                {
                    var methodInfo = DetermineMethodInfo(signalDelegateData);
                    AppendMethodInfo(source, methodInfo);
                }

                source.Append("        return signals;\n");
                source.Append("    }\n");
            }

            source.Append("#pragma warning restore CS0109\n");

            // Generate signal event

            foreach (var signalDelegate in scardotSignalDelegates)
            {
                string signalName = signalDelegate.Name;

                // TODO: Hide backing event from code-completion and debugger
                // The reason we have a backing field is to hide the invoke method from the event,
                // as it doesn't emit the signal, only the event delegates. This can confuse users.
                // Maybe we should directly connect the delegates, as we do with native signals?
                source.Append("    private ")
                    .Append(signalDelegate.DelegateSymbol.FullQualifiedNameIncludeGlobal())
                    .Append(" backing_")
                    .Append(signalName)
                    .Append(";\n");

                source.Append(
                    $"    /// <inheritdoc cref=\"{signalDelegate.DelegateSymbol.FullQualifiedNameIncludeGlobal()}\"/>\n");

                source.Append("    public event ")
                    .Append(signalDelegate.DelegateSymbol.FullQualifiedNameIncludeGlobal())
                    .Append(" @")
                    .Append(signalName)
                    .Append(" {\n")
                    .Append("        add => backing_")
                    .Append(signalName)
                    .Append(" += value;\n")
                    .Append("        remove => backing_")
                    .Append(signalName)
                    .Append(" -= value;\n")
                    .Append("}\n");
            }

            // Generate RaisescardotClassSignalCallbacks

            if (scardotSignalDelegates.Count > 0)
            {
                source.Append("    /// <inheritdoc/>\n");
                source.Append("    [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]\n");
                source.Append(
                    "    protected override void RaisescardotClassSignalCallbacks(in scardot_string_name signal, ");
                source.Append("NativeVariantPtrArgs args)\n    {\n");

                foreach (var signal in scardotSignalDelegates)
                {
                    GenerateSignalEventInvoker(signal, source);
                }

                source.Append("        base.RaisescardotClassSignalCallbacks(signal, args);\n");

                source.Append("    }\n");
            }

            // Generate HasscardotClassSignal

            if (scardotSignalDelegates.Count > 0)
            {
                source.Append("    /// <inheritdoc/>\n");
                source.Append("    [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]\n");
                source.Append(
                    "    protected override bool HasscardotClassSignal(in scardot_string_name signal)\n    {\n");

                foreach (var signal in scardotSignalDelegates)
                {
                    GenerateHasSignalEntry(signal.Name, source);
                }

                source.Append("        return base.HasscardotClassSignal(signal);\n");

                source.Append("    }\n");
            }

            source.Append("}\n"); // partial class

            if (isInnerClass)
            {
                var containingType = symbol.ContainingType;

                while (containingType != null)
                {
                    source.Append("}\n"); // outer class

                    containingType = containingType.ContainingType;
                }
            }

            if (hasNamespace)
            {
                source.Append("\n}\n");
            }

            context.AddSource(uniqueHint, SourceText.From(source.ToString(), Encoding.UTF8));
        }

        private static void AppendMethodInfo(StringBuilder source, MethodInfo methodInfo)
        {
            source.Append("        signals.Add(new(name: SignalName.@")
                .Append(methodInfo.Name)
                .Append(", returnVal: ");

            AppendPropertyInfo(source, methodInfo.ReturnVal);

            source.Append(", flags: (global::scardot.MethodFlags)")
                .Append((int)methodInfo.Flags)
                .Append(", arguments: ");

            if (methodInfo.Arguments is { Count: > 0 })
            {
                source.Append("new() { ");

                foreach (var param in methodInfo.Arguments)
                {
                    AppendPropertyInfo(source, param);

                    // C# allows colon after the last element
                    source.Append(", ");
                }

                source.Append(" }");
            }
            else
            {
                source.Append("null");
            }

            source.Append(", defaultArguments: null));\n");
        }

        private static void AppendPropertyInfo(StringBuilder source, PropertyInfo propertyInfo)
        {
            source.Append("new(type: (global::scardot.Variant.Type)")
                .Append((int)propertyInfo.Type)
                .Append(", name: \"")
                .Append(propertyInfo.Name)
                .Append("\", hint: (global::scardot.PropertyHint)")
                .Append((int)propertyInfo.Hint)
                .Append(", hintString: \"")
                .Append(propertyInfo.HintString)
                .Append("\", usage: (global::scardot.PropertyUsageFlags)")
                .Append((int)propertyInfo.Usage)
                .Append(", exported: ")
                .Append(propertyInfo.Exported ? "true" : "false");
            if (propertyInfo.ClassName != null)
            {
                source.Append(", className: new global::scardot.StringName(\"")
                    .Append(propertyInfo.ClassName)
                    .Append("\")");
            }
            source.Append(")");
        }

        private static MethodInfo DetermineMethodInfo(scardotSignalDelegateData signalDelegateData)
        {
            var invokeMethodData = signalDelegateData.InvokeMethodData;

            PropertyInfo returnVal;

            if (invokeMethodData.RetType != null)
            {
                returnVal = DeterminePropertyInfo(invokeMethodData.RetType.Value.MarshalType,
                    invokeMethodData.RetType.Value.TypeSymbol,
                    name: string.Empty);
            }
            else
            {
                returnVal = new PropertyInfo(VariantType.Nil, string.Empty, PropertyHint.None,
                    hintString: null, PropertyUsageFlags.Default, exported: false);
            }

            int paramCount = invokeMethodData.ParamTypes.Length;

            List<PropertyInfo>? arguments;

            if (paramCount > 0)
            {
                arguments = new(capacity: paramCount);

                for (int i = 0; i < paramCount; i++)
                {
                    arguments.Add(DeterminePropertyInfo(invokeMethodData.ParamTypes[i],
                        invokeMethodData.Method.Parameters[i].Type,
                        name: invokeMethodData.Method.Parameters[i].Name));
                }
            }
            else
            {
                arguments = null;
            }

            return new MethodInfo(signalDelegateData.Name, returnVal, MethodFlags.Default, arguments,
                defaultArguments: null);
        }

        private static PropertyInfo DeterminePropertyInfo(MarshalType marshalType, ITypeSymbol typeSymbol, string name)
        {
            var memberVariantType = MarshalUtils.ConvertMarshalTypeToVariantType(marshalType)!.Value;

            var propUsage = PropertyUsageFlags.Default;

            if (memberVariantType == VariantType.Nil)
                propUsage |= PropertyUsageFlags.NilIsVariant;

            string? className = null;
            if (memberVariantType == VariantType.Object && typeSymbol is INamedTypeSymbol namedTypeSymbol)
            {
                className = namedTypeSymbol.GetscardotScriptNativeClassName();
            }

            return new PropertyInfo(memberVariantType, name,
                PropertyHint.None, string.Empty, propUsage, className, exported: false);
        }

        private static void GenerateHasSignalEntry(
            string signalName,
            StringBuilder source
        )
        {
            source.Append("        ");
            source.Append("if (signal == SignalName.@");
            source.Append(signalName);
            source.Append(") {\n           return true;\n        }\n");
        }

        private static void GenerateSignalEventInvoker(
            scardotSignalDelegateData signal,
            StringBuilder source
        )
        {
            string signalName = signal.Name;
            var invokeMethodData = signal.InvokeMethodData;

            source.Append("        if (signal == SignalName.@");
            source.Append(signalName);
            source.Append(" && args.Count == ");
            source.Append(invokeMethodData.ParamTypes.Length);
            source.Append(") {\n");
            source.Append("            backing_");
            source.Append(signalName);
            source.Append("?.Invoke(");

            for (int i = 0; i < invokeMethodData.ParamTypes.Length; i++)
            {
                if (i != 0)
                    source.Append(", ");

                source.AppendNativeVariantToManagedExpr(string.Concat("args[", i.ToString(), "]"),
                    invokeMethodData.ParamTypeSymbols[i], invokeMethodData.ParamTypes[i]);
            }

            source.Append(");\n");

            source.Append("            return;\n");

            source.Append("        }\n");
        }
    }
}
