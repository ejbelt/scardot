using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace scardot.SourceGenerators
{
    public readonly struct scardotMethodData
    {
        public scardotMethodData(IMethodSymbol method, ImmutableArray<MarshalType> paramTypes,
            ImmutableArray<ITypeSymbol> paramTypeSymbols, (MarshalType MarshalType, ITypeSymbol TypeSymbol)? retType)
        {
            Method = method;
            ParamTypes = paramTypes;
            ParamTypeSymbols = paramTypeSymbols;
            RetType = retType;
        }

        public IMethodSymbol Method { get; }
        public ImmutableArray<MarshalType> ParamTypes { get; }
        public ImmutableArray<ITypeSymbol> ParamTypeSymbols { get; }
        public (MarshalType MarshalType, ITypeSymbol TypeSymbol)? RetType { get; }
    }

    public readonly struct scardotSignalDelegateData
    {
        public scardotSignalDelegateData(string name, INamedTypeSymbol delegateSymbol, scardotMethodData invokeMethodData)
        {
            Name = name;
            DelegateSymbol = delegateSymbol;
            InvokeMethodData = invokeMethodData;
        }

        public string Name { get; }
        public INamedTypeSymbol DelegateSymbol { get; }
        public scardotMethodData InvokeMethodData { get; }
    }

    public readonly struct scardotPropertyData
    {
        public scardotPropertyData(IPropertySymbol propertySymbol, MarshalType type)
        {
            PropertySymbol = propertySymbol;
            Type = type;
        }

        public IPropertySymbol PropertySymbol { get; }
        public MarshalType Type { get; }
    }

    public readonly struct scardotFieldData
    {
        public scardotFieldData(IFieldSymbol fieldSymbol, MarshalType type)
        {
            FieldSymbol = fieldSymbol;
            Type = type;
        }

        public IFieldSymbol FieldSymbol { get; }
        public MarshalType Type { get; }
    }

    public struct scardotPropertyOrFieldData
    {
        public scardotPropertyOrFieldData(ISymbol symbol, MarshalType type)
        {
            Symbol = symbol;
            Type = type;
        }

        public scardotPropertyOrFieldData(scardotPropertyData propertyData)
            : this(propertyData.PropertySymbol, propertyData.Type)
        {
        }

        public scardotPropertyOrFieldData(scardotFieldData fieldData)
            : this(fieldData.FieldSymbol, fieldData.Type)
        {
        }

        public ISymbol Symbol { get; }
        public MarshalType Type { get; }
    }
}
