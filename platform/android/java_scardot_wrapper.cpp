/**************************************************************************/
/*  java_scardot_wrapper.cpp                                                */
/**************************************************************************/
/*                         This file is part of:                          */
/*                             SCARDOT ENGINE                               */
/*                        https://godotengine.org                         */
/**************************************************************************/
/* Copyright (c) 2014-present scardot Engine contributors (see AUTHORS.md). */
/* Copyright (c) 2007-2014 Juan Linietsky, Ariel Manzur.                  */
/*                                                                        */
/* Permission is hereby granted, free of charge, to any person obtaining  */
/* a copy of this software and associated documentation files (the        */
/* "Software"), to deal in the Software without restriction, including    */
/* without limitation the rights to use, copy, modify, merge, publish,    */
/* distribute, sublicense, and/or sell copies of the Software, and to     */
/* permit persons to whom the Software is furnished to do so, subject to  */
/* the following conditions:                                              */
/*                                                                        */
/* The above copyright notice and this permission notice shall be         */
/* included in all copies or substantial portions of the Software.        */
/*                                                                        */
/* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,        */
/* EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF     */
/* MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. */
/* IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY   */
/* CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,   */
/* TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE      */
/* SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.                 */
/**************************************************************************/

#include "java_scardot_wrapper.h"

// JNIEnv is only valid within the thread it belongs to, in a multi threading environment
// we can't cache it.
// For scardot we call most access methods from our thread and we thus get a valid JNIEnv
// from get_jni_env(). For one or two we expect to pass the environment

// TODO we could probably create a base class for this...

scardotJavaWrapper::scardotJavaWrapper(JNIEnv *p_env, jobject p_activity, jobject p_scardot_instance) {
	scardot_instance = p_env->NewGlobalRef(p_scardot_instance);
	activity = p_env->NewGlobalRef(p_activity);

	// get info about our scardot class so we can get pointers and stuff...
	scardot_class = p_env->FindClass("org/scardotengine/scardot/scardot");
	if (scardot_class) {
		scardot_class = (jclass)p_env->NewGlobalRef(scardot_class);
	} else {
		// this is a pretty serious fail.. bail... pointers will stay 0
		return;
	}
	activity_class = p_env->FindClass("android/app/Activity");
	if (activity_class) {
		activity_class = (jclass)p_env->NewGlobalRef(activity_class);
	} else {
		// this is a pretty serious fail.. bail... pointers will stay 0
		return;
	}

	// get some scardot method pointers...
	_restart = p_env->GetMethodID(scardot_class, "restart", "()V");
	_finish = p_env->GetMethodID(scardot_class, "forceQuit", "(I)Z");
	_set_keep_screen_on = p_env->GetMethodID(scardot_class, "setKeepScreenOn", "(Z)V");
	_alert = p_env->GetMethodID(scardot_class, "alert", "(Ljava/lang/String;Ljava/lang/String;)V");
	_is_dark_mode_supported = p_env->GetMethodID(scardot_class, "isDarkModeSupported", "()Z");
	_is_dark_mode = p_env->GetMethodID(scardot_class, "isDarkMode", "()Z");
	_get_clipboard = p_env->GetMethodID(scardot_class, "getClipboard", "()Ljava/lang/String;");
	_set_clipboard = p_env->GetMethodID(scardot_class, "setClipboard", "(Ljava/lang/String;)V");
	_has_clipboard = p_env->GetMethodID(scardot_class, "hasClipboard", "()Z");
	_request_permission = p_env->GetMethodID(scardot_class, "requestPermission", "(Ljava/lang/String;)Z");
	_request_permissions = p_env->GetMethodID(scardot_class, "requestPermissions", "()Z");
	_get_granted_permissions = p_env->GetMethodID(scardot_class, "getGrantedPermissions", "()[Ljava/lang/String;");
	_get_ca_certificates = p_env->GetMethodID(scardot_class, "getCACertificates", "()Ljava/lang/String;");
	_init_input_devices = p_env->GetMethodID(scardot_class, "initInputDevices", "()V");
	_vibrate = p_env->GetMethodID(scardot_class, "vibrate", "(II)V");
	_get_input_fallback_mapping = p_env->GetMethodID(scardot_class, "getInputFallbackMapping", "()Ljava/lang/String;");
	_on_scardot_setup_completed = p_env->GetMethodID(scardot_class, "onscardotSetupCompleted", "()V");
	_on_scardot_main_loop_started = p_env->GetMethodID(scardot_class, "onscardotMainLoopStarted", "()V");
	_on_scardot_terminating = p_env->GetMethodID(scardot_class, "onscardotTerminating", "()V");
	_create_new_scardot_instance = p_env->GetMethodID(scardot_class, "createNewscardotInstance", "([Ljava/lang/String;)I");
	_get_render_view = p_env->GetMethodID(scardot_class, "getRenderView", "()Lorg/scardotengine/scardot/scardotRenderView;");
	_begin_benchmark_measure = p_env->GetMethodID(scardot_class, "nativeBeginBenchmarkMeasure", "(Ljava/lang/String;Ljava/lang/String;)V");
	_end_benchmark_measure = p_env->GetMethodID(scardot_class, "nativeEndBenchmarkMeasure", "(Ljava/lang/String;Ljava/lang/String;)V");
	_dump_benchmark = p_env->GetMethodID(scardot_class, "nativeDumpBenchmark", "(Ljava/lang/String;)V");
	_get_gdextension_list_config_file = p_env->GetMethodID(scardot_class, "getGDExtensionConfigFiles", "()[Ljava/lang/String;");
	_has_feature = p_env->GetMethodID(scardot_class, "hasFeature", "(Ljava/lang/String;)Z");
	_sign_apk = p_env->GetMethodID(scardot_class, "nativeSignApk", "(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I");
	_verify_apk = p_env->GetMethodID(scardot_class, "nativeVerifyApk", "(Ljava/lang/String;)I");
	_enable_immersive_mode = p_env->GetMethodID(scardot_class, "nativeEnableImmersiveMode", "(Z)V");
	_is_in_immersive_mode = p_env->GetMethodID(scardot_class, "isInImmersiveMode", "()Z");
}

scardotJavaWrapper::~scardotJavaWrapper() {
	if (scardot_view) {
		delete scardot_view;
	}

	JNIEnv *env = get_jni_env();
	ERR_FAIL_NULL(env);
	env->DeleteGlobalRef(scardot_instance);
	env->DeleteGlobalRef(scardot_class);
	env->DeleteGlobalRef(activity);
	env->DeleteGlobalRef(activity_class);
}

jobject scardotJavaWrapper::get_activity() {
	return activity;
}

scardotJavaViewWrapper *scardotJavaWrapper::get_scardot_view() {
	if (scardot_view != nullptr) {
		return scardot_view;
	}
	if (_get_render_view) {
		JNIEnv *env = get_jni_env();
		ERR_FAIL_NULL_V(env, nullptr);
		jobject scardot_render_view = env->CallObjectMethod(scardot_instance, _get_render_view);
		if (!env->IsSameObject(scardot_render_view, nullptr)) {
			scardot_view = new scardotJavaViewWrapper(scardot_render_view);
		}
	}
	return scardot_view;
}

void scardotJavaWrapper::on_scardot_setup_completed(JNIEnv *p_env) {
	if (_on_scardot_setup_completed) {
		if (p_env == nullptr) {
			p_env = get_jni_env();
		}
		p_env->CallVoidMethod(scardot_instance, _on_scardot_setup_completed);
	}
}

void scardotJavaWrapper::on_scardot_main_loop_started(JNIEnv *p_env) {
	if (_on_scardot_main_loop_started) {
		if (p_env == nullptr) {
			p_env = get_jni_env();
		}
		ERR_FAIL_NULL(p_env);
		p_env->CallVoidMethod(scardot_instance, _on_scardot_main_loop_started);
	}
}

void scardotJavaWrapper::on_scardot_terminating(JNIEnv *p_env) {
	if (_on_scardot_terminating) {
		if (p_env == nullptr) {
			p_env = get_jni_env();
		}
		ERR_FAIL_NULL(p_env);
		p_env->CallVoidMethod(scardot_instance, _on_scardot_terminating);
	}
}

void scardotJavaWrapper::restart(JNIEnv *p_env) {
	if (_restart) {
		if (p_env == nullptr) {
			p_env = get_jni_env();
		}
		ERR_FAIL_NULL(p_env);
		p_env->CallVoidMethod(scardot_instance, _restart);
	}
}

bool scardotJavaWrapper::force_quit(JNIEnv *p_env, int p_instance_id) {
	if (_finish) {
		if (p_env == nullptr) {
			p_env = get_jni_env();
		}
		ERR_FAIL_NULL_V(p_env, false);
		return p_env->CallBooleanMethod(scardot_instance, _finish, p_instance_id);
	}
	return false;
}

void scardotJavaWrapper::set_keep_screen_on(bool p_enabled) {
	if (_set_keep_screen_on) {
		JNIEnv *env = get_jni_env();
		ERR_FAIL_NULL(env);
		env->CallVoidMethod(scardot_instance, _set_keep_screen_on, p_enabled);
	}
}

void scardotJavaWrapper::alert(const String &p_message, const String &p_title) {
	if (_alert) {
		JNIEnv *env = get_jni_env();
		ERR_FAIL_NULL(env);
		jstring jStrMessage = env->NewStringUTF(p_message.utf8().get_data());
		jstring jStrTitle = env->NewStringUTF(p_title.utf8().get_data());
		env->CallVoidMethod(scardot_instance, _alert, jStrMessage, jStrTitle);
		env->DeleteLocalRef(jStrMessage);
		env->DeleteLocalRef(jStrTitle);
	}
}

bool scardotJavaWrapper::is_dark_mode_supported() {
	if (_is_dark_mode_supported) {
		JNIEnv *env = get_jni_env();
		ERR_FAIL_NULL_V(env, false);
		return env->CallBooleanMethod(scardot_instance, _is_dark_mode_supported);
	} else {
		return false;
	}
}

bool scardotJavaWrapper::is_dark_mode() {
	if (_is_dark_mode) {
		JNIEnv *env = get_jni_env();
		ERR_FAIL_NULL_V(env, false);
		return env->CallBooleanMethod(scardot_instance, _is_dark_mode);
	} else {
		return false;
	}
}

bool scardotJavaWrapper::has_get_clipboard() {
	return _get_clipboard != nullptr;
}

String scardotJavaWrapper::get_clipboard() {
	String clipboard;
	if (_get_clipboard) {
		JNIEnv *env = get_jni_env();
		ERR_FAIL_NULL_V(env, String());
		jstring s = (jstring)env->CallObjectMethod(scardot_instance, _get_clipboard);
		clipboard = jstring_to_string(s, env);
		env->DeleteLocalRef(s);
	}
	return clipboard;
}

String scardotJavaWrapper::get_input_fallback_mapping() {
	String input_fallback_mapping;
	if (_get_input_fallback_mapping) {
		JNIEnv *env = get_jni_env();
		ERR_FAIL_NULL_V(env, String());
		jstring fallback_mapping = (jstring)env->CallObjectMethod(scardot_instance, _get_input_fallback_mapping);
		input_fallback_mapping = jstring_to_string(fallback_mapping, env);
		env->DeleteLocalRef(fallback_mapping);
	}
	return input_fallback_mapping;
}

bool scardotJavaWrapper::has_set_clipboard() {
	return _set_clipboard != nullptr;
}

void scardotJavaWrapper::set_clipboard(const String &p_text) {
	if (_set_clipboard) {
		JNIEnv *env = get_jni_env();
		ERR_FAIL_NULL(env);
		jstring jStr = env->NewStringUTF(p_text.utf8().get_data());
		env->CallVoidMethod(scardot_instance, _set_clipboard, jStr);
		env->DeleteLocalRef(jStr);
	}
}

bool scardotJavaWrapper::has_has_clipboard() {
	return _has_clipboard != nullptr;
}

bool scardotJavaWrapper::has_clipboard() {
	if (_has_clipboard) {
		JNIEnv *env = get_jni_env();
		ERR_FAIL_NULL_V(env, false);
		return env->CallBooleanMethod(scardot_instance, _has_clipboard);
	} else {
		return false;
	}
}

bool scardotJavaWrapper::request_permission(const String &p_name) {
	if (_request_permission) {
		JNIEnv *env = get_jni_env();
		ERR_FAIL_NULL_V(env, false);
		jstring jStrName = env->NewStringUTF(p_name.utf8().get_data());
		bool result = env->CallBooleanMethod(scardot_instance, _request_permission, jStrName);
		env->DeleteLocalRef(jStrName);
		return result;
	} else {
		return false;
	}
}

bool scardotJavaWrapper::request_permissions() {
	if (_request_permissions) {
		JNIEnv *env = get_jni_env();
		ERR_FAIL_NULL_V(env, false);
		return env->CallBooleanMethod(scardot_instance, _request_permissions);
	} else {
		return false;
	}
}

Vector<String> scardotJavaWrapper::get_granted_permissions() const {
	Vector<String> permissions_list;
	if (_get_granted_permissions) {
		JNIEnv *env = get_jni_env();
		ERR_FAIL_NULL_V(env, permissions_list);
		jobject permissions_object = env->CallObjectMethod(scardot_instance, _get_granted_permissions);
		jobjectArray *arr = reinterpret_cast<jobjectArray *>(&permissions_object);

		jsize len = env->GetArrayLength(*arr);
		for (int i = 0; i < len; i++) {
			jstring jstr = (jstring)env->GetObjectArrayElement(*arr, i);
			String str = jstring_to_string(jstr, env);
			permissions_list.push_back(str);
			env->DeleteLocalRef(jstr);
		}
	}
	return permissions_list;
}

Vector<String> scardotJavaWrapper::get_gdextension_list_config_file() const {
	Vector<String> config_file_list;
	if (_get_gdextension_list_config_file) {
		JNIEnv *env = get_jni_env();
		ERR_FAIL_NULL_V(env, config_file_list);
		jobject config_file_list_object = env->CallObjectMethod(scardot_instance, _get_gdextension_list_config_file);
		jobjectArray *arr = reinterpret_cast<jobjectArray *>(&config_file_list_object);

		jsize len = env->GetArrayLength(*arr);
		for (int i = 0; i < len; i++) {
			jstring j_config_file = (jstring)env->GetObjectArrayElement(*arr, i);
			String config_file = jstring_to_string(j_config_file, env);
			config_file_list.push_back(config_file);
			env->DeleteLocalRef(j_config_file);
		}
	}
	return config_file_list;
}

String scardotJavaWrapper::get_ca_certificates() const {
	String ca_certificates;
	if (_get_ca_certificates) {
		JNIEnv *env = get_jni_env();
		ERR_FAIL_NULL_V(env, String());
		jstring s = (jstring)env->CallObjectMethod(scardot_instance, _get_ca_certificates);
		ca_certificates = jstring_to_string(s, env);
		env->DeleteLocalRef(s);
	}
	return ca_certificates;
}

void scardotJavaWrapper::init_input_devices() {
	if (_init_input_devices) {
		JNIEnv *env = get_jni_env();
		ERR_FAIL_NULL(env);
		env->CallVoidMethod(scardot_instance, _init_input_devices);
	}
}

void scardotJavaWrapper::vibrate(int p_duration_ms, float p_amplitude) {
	if (_vibrate) {
		JNIEnv *env = get_jni_env();
		ERR_FAIL_NULL(env);

		int j_amplitude = -1.0;

		if (p_amplitude != -1.0) {
			j_amplitude = CLAMP(int(p_amplitude * 255), 1, 255);
		}

		env->CallVoidMethod(scardot_instance, _vibrate, p_duration_ms, j_amplitude);
	}
}

int scardotJavaWrapper::create_new_scardot_instance(const List<String> &args) {
	if (_create_new_scardot_instance) {
		JNIEnv *env = get_jni_env();
		ERR_FAIL_NULL_V(env, 0);
		jobjectArray jargs = env->NewObjectArray(args.size(), env->FindClass("java/lang/String"), env->NewStringUTF(""));
		int i = 0;
		for (List<String>::ConstIterator itr = args.begin(); itr != args.end(); ++itr, ++i) {
			jstring j_arg = env->NewStringUTF(itr->utf8().get_data());
			env->SetObjectArrayElement(jargs, i, j_arg);
			env->DeleteLocalRef(j_arg);
		}
		return env->CallIntMethod(scardot_instance, _create_new_scardot_instance, jargs);
	} else {
		return 0;
	}
}

void scardotJavaWrapper::begin_benchmark_measure(const String &p_context, const String &p_label) {
	if (_begin_benchmark_measure) {
		JNIEnv *env = get_jni_env();
		ERR_FAIL_NULL(env);
		jstring j_context = env->NewStringUTF(p_context.utf8().get_data());
		jstring j_label = env->NewStringUTF(p_label.utf8().get_data());
		env->CallVoidMethod(scardot_instance, _begin_benchmark_measure, j_context, j_label);
		env->DeleteLocalRef(j_context);
		env->DeleteLocalRef(j_label);
	}
}

void scardotJavaWrapper::end_benchmark_measure(const String &p_context, const String &p_label) {
	if (_end_benchmark_measure) {
		JNIEnv *env = get_jni_env();
		ERR_FAIL_NULL(env);
		jstring j_context = env->NewStringUTF(p_context.utf8().get_data());
		jstring j_label = env->NewStringUTF(p_label.utf8().get_data());
		env->CallVoidMethod(scardot_instance, _end_benchmark_measure, j_context, j_label);
		env->DeleteLocalRef(j_context);
		env->DeleteLocalRef(j_label);
	}
}

void scardotJavaWrapper::dump_benchmark(const String &benchmark_file) {
	if (_dump_benchmark) {
		JNIEnv *env = get_jni_env();
		ERR_FAIL_NULL(env);
		jstring j_benchmark_file = env->NewStringUTF(benchmark_file.utf8().get_data());
		env->CallVoidMethod(scardot_instance, _dump_benchmark, j_benchmark_file);
		env->DeleteLocalRef(j_benchmark_file);
	}
}

bool scardotJavaWrapper::has_feature(const String &p_feature) const {
	if (_has_feature) {
		JNIEnv *env = get_jni_env();
		ERR_FAIL_NULL_V(env, false);

		jstring j_feature = env->NewStringUTF(p_feature.utf8().get_data());
		bool result = env->CallBooleanMethod(scardot_instance, _has_feature, j_feature);
		env->DeleteLocalRef(j_feature);
		return result;
	} else {
		return false;
	}
}

Error scardotJavaWrapper::sign_apk(const String &p_input_path, const String &p_output_path, const String &p_keystore_path, const String &p_keystore_user, const String &p_keystore_password) {
	if (_sign_apk) {
		JNIEnv *env = get_jni_env();
		ERR_FAIL_NULL_V(env, ERR_UNCONFIGURED);

		jstring j_input_path = env->NewStringUTF(p_input_path.utf8().get_data());
		jstring j_output_path = env->NewStringUTF(p_output_path.utf8().get_data());
		jstring j_keystore_path = env->NewStringUTF(p_keystore_path.utf8().get_data());
		jstring j_keystore_user = env->NewStringUTF(p_keystore_user.utf8().get_data());
		jstring j_keystore_password = env->NewStringUTF(p_keystore_password.utf8().get_data());

		int result = env->CallIntMethod(scardot_instance, _sign_apk, j_input_path, j_output_path, j_keystore_path, j_keystore_user, j_keystore_password);

		env->DeleteLocalRef(j_input_path);
		env->DeleteLocalRef(j_output_path);
		env->DeleteLocalRef(j_keystore_path);
		env->DeleteLocalRef(j_keystore_user);
		env->DeleteLocalRef(j_keystore_password);

		return static_cast<Error>(result);
	} else {
		return ERR_UNCONFIGURED;
	}
}

Error scardotJavaWrapper::verify_apk(const String &p_apk_path) {
	if (_verify_apk) {
		JNIEnv *env = get_jni_env();
		ERR_FAIL_NULL_V(env, ERR_UNCONFIGURED);

		jstring j_apk_path = env->NewStringUTF(p_apk_path.utf8().get_data());
		int result = env->CallIntMethod(scardot_instance, _verify_apk, j_apk_path);
		env->DeleteLocalRef(j_apk_path);
		return static_cast<Error>(result);
	} else {
		return ERR_UNCONFIGURED;
	}
}

void scardotJavaWrapper::enable_immersive_mode(bool p_enabled) {
	if (_enable_immersive_mode) {
		JNIEnv *env = get_jni_env();
		ERR_FAIL_NULL(env);
		env->CallVoidMethod(scardot_instance, _enable_immersive_mode, p_enabled);
	}
}

bool scardotJavaWrapper::is_in_immersive_mode() {
	if (_is_in_immersive_mode) {
		JNIEnv *env = get_jni_env();
		ERR_FAIL_NULL_V(env, false);
		return env->CallBooleanMethod(scardot_instance, _is_in_immersive_mode);
	} else {
		return false;
	}
}
