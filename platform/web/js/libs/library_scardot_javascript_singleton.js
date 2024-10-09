/**************************************************************************/
/*  library_scardot_javascript_singleton.js                                 */
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

const scardotJSWrapper = {

	$scardotJSWrapper__deps: ['$scardotRuntime', '$IDHandler'],
	$scardotJSWrapper__postset: 'scardotJSWrapper.proxies = new Map();',
	$scardotJSWrapper: {
		proxies: null,
		cb_ret: null,

		MyProxy: function (val) {
			const id = IDHandler.add(this);
			scardotJSWrapper.proxies.set(val, id);
			let refs = 1;
			this.ref = function () {
				refs++;
			};
			this.unref = function () {
				refs--;
				if (refs === 0) {
					IDHandler.remove(id);
					scardotJSWrapper.proxies.delete(val);
				}
			};
			this.get_val = function () {
				return val;
			};
			this.get_id = function () {
				return id;
			};
		},

		get_proxied: function (val) {
			const id = scardotJSWrapper.proxies.get(val);
			if (id === undefined) {
				const proxy = new scardotJSWrapper.MyProxy(val);
				return proxy.get_id();
			}
			IDHandler.get(id).ref();
			return id;
		},

		get_proxied_value: function (id) {
			const proxy = IDHandler.get(id);
			if (proxy === undefined) {
				return undefined;
			}
			return proxy.get_val();
		},

		variant2js: function (type, val) {
			switch (type) {
			case 0:
				return null;
			case 1:
				return Boolean(scardotRuntime.getHeapValue(val, 'i64'));
			case 2: {
				// `heap_value` may be a bigint.
				const heap_value = scardotRuntime.getHeapValue(val, 'i64');
				return heap_value >= Number.MIN_SAFE_INTEGER && heap_value <= Number.MAX_SAFE_INTEGER
					? Number(heap_value)
					: heap_value;
			}
			case 3:
				return Number(scardotRuntime.getHeapValue(val, 'double'));
			case 4:
				return scardotRuntime.parseString(scardotRuntime.getHeapValue(val, '*'));
			case 24: // OBJECT
				return scardotJSWrapper.get_proxied_value(scardotRuntime.getHeapValue(val, 'i64'));
			default:
				return undefined;
			}
		},

		js2variant: function (p_val, p_exchange) {
			if (p_val === undefined || p_val === null) {
				return 0; // NIL
			}
			const type = typeof (p_val);
			if (type === 'boolean') {
				scardotRuntime.setHeapValue(p_exchange, p_val, 'i64');
				return 1; // BOOL
			} else if (type === 'number') {
				if (Number.isInteger(p_val)) {
					scardotRuntime.setHeapValue(p_exchange, p_val, 'i64');
					return 2; // INT
				}
				scardotRuntime.setHeapValue(p_exchange, p_val, 'double');
				return 3; // FLOAT
			} else if (type === 'bigint') {
				scardotRuntime.setHeapValue(p_exchange, p_val, 'i64');
				return 2; // INT
			} else if (type === 'string') {
				const c_str = scardotRuntime.allocString(p_val);
				scardotRuntime.setHeapValue(p_exchange, c_str, '*');
				return 4; // STRING
			}
			const id = scardotJSWrapper.get_proxied(p_val);
			scardotRuntime.setHeapValue(p_exchange, id, 'i64');
			return 24; // OBJECT
		},
	},

	scardot_js_wrapper_interface_get__proxy: 'sync',
	scardot_js_wrapper_interface_get__sig: 'ii',
	scardot_js_wrapper_interface_get: function (p_name) {
		const name = scardotRuntime.parseString(p_name);
		if (typeof (window[name]) !== 'undefined') {
			return scardotJSWrapper.get_proxied(window[name]);
		}
		return 0;
	},

	scardot_js_wrapper_object_get__proxy: 'sync',
	scardot_js_wrapper_object_get__sig: 'iiii',
	scardot_js_wrapper_object_get: function (p_id, p_exchange, p_prop) {
		const obj = scardotJSWrapper.get_proxied_value(p_id);
		if (obj === undefined) {
			return 0;
		}
		if (p_prop) {
			const prop = scardotRuntime.parseString(p_prop);
			try {
				return scardotJSWrapper.js2variant(obj[prop], p_exchange);
			} catch (e) {
				scardotRuntime.error(`Error getting variable ${prop} on object`, obj);
				return 0; // NIL
			}
		}
		return scardotJSWrapper.js2variant(obj, p_exchange);
	},

	scardot_js_wrapper_object_set__proxy: 'sync',
	scardot_js_wrapper_object_set__sig: 'viiii',
	scardot_js_wrapper_object_set: function (p_id, p_name, p_type, p_exchange) {
		const obj = scardotJSWrapper.get_proxied_value(p_id);
		if (obj === undefined) {
			return;
		}
		const name = scardotRuntime.parseString(p_name);
		try {
			obj[name] = scardotJSWrapper.variant2js(p_type, p_exchange);
		} catch (e) {
			scardotRuntime.error(`Error setting variable ${name} on object`, obj);
		}
	},

	scardot_js_wrapper_object_call__proxy: 'sync',
	scardot_js_wrapper_object_call__sig: 'iiiiiiiii',
	scardot_js_wrapper_object_call: function (p_id, p_method, p_args, p_argc, p_convert_callback, p_exchange, p_lock, p_free_lock_callback) {
		const obj = scardotJSWrapper.get_proxied_value(p_id);
		if (obj === undefined) {
			return -1;
		}
		const method = scardotRuntime.parseString(p_method);
		const convert = scardotRuntime.get_func(p_convert_callback);
		const freeLock = scardotRuntime.get_func(p_free_lock_callback);
		const args = new Array(p_argc);
		for (let i = 0; i < p_argc; i++) {
			const type = convert(p_args, i, p_exchange, p_lock);
			const lock = scardotRuntime.getHeapValue(p_lock, '*');
			args[i] = scardotJSWrapper.variant2js(type, p_exchange);
			if (lock) {
				freeLock(p_lock, type);
			}
		}
		try {
			const res = obj[method](...args);
			return scardotJSWrapper.js2variant(res, p_exchange);
		} catch (e) {
			scardotRuntime.error(`Error calling method ${method} on:`, obj, 'error:', e);
			return -1;
		}
	},

	scardot_js_wrapper_object_unref__proxy: 'sync',
	scardot_js_wrapper_object_unref__sig: 'vi',
	scardot_js_wrapper_object_unref: function (p_id) {
		const proxy = IDHandler.get(p_id);
		if (proxy !== undefined) {
			proxy.unref();
		}
	},

	scardot_js_wrapper_create_cb__proxy: 'sync',
	scardot_js_wrapper_create_cb__sig: 'iii',
	scardot_js_wrapper_create_cb: function (p_ref, p_func) {
		const func = scardotRuntime.get_func(p_func);
		let id = 0;
		const cb = function () {
			if (!scardotJSWrapper.get_proxied_value(id)) {
				return undefined;
			}
			// The callback will store the returned value in this variable via
			// "scardot_js_wrapper_object_set_cb_ret" upon calling the user function.
			// This is safe! JavaScript is single threaded (and using it in threads is not a good idea anyway).
			scardotJSWrapper.cb_ret = null;
			const args = Array.from(arguments);
			const argsProxy = new scardotJSWrapper.MyProxy(args);
			func(p_ref, argsProxy.get_id(), args.length);
			argsProxy.unref();
			const ret = scardotJSWrapper.cb_ret;
			scardotJSWrapper.cb_ret = null;
			return ret;
		};
		id = scardotJSWrapper.get_proxied(cb);
		return id;
	},

	scardot_js_wrapper_object_set_cb_ret__proxy: 'sync',
	scardot_js_wrapper_object_set_cb_ret__sig: 'vii',
	scardot_js_wrapper_object_set_cb_ret: function (p_val_type, p_val_ex) {
		scardotJSWrapper.cb_ret = scardotJSWrapper.variant2js(p_val_type, p_val_ex);
	},

	scardot_js_wrapper_object_getvar__proxy: 'sync',
	scardot_js_wrapper_object_getvar__sig: 'iiii',
	scardot_js_wrapper_object_getvar: function (p_id, p_type, p_exchange) {
		const obj = scardotJSWrapper.get_proxied_value(p_id);
		if (obj === undefined) {
			return -1;
		}
		const prop = scardotJSWrapper.variant2js(p_type, p_exchange);
		if (prop === undefined || prop === null) {
			return -1;
		}
		try {
			return scardotJSWrapper.js2variant(obj[prop], p_exchange);
		} catch (e) {
			scardotRuntime.error(`Error getting variable ${prop} on object`, obj, e);
			return -1;
		}
	},

	scardot_js_wrapper_object_setvar__proxy: 'sync',
	scardot_js_wrapper_object_setvar__sig: 'iiiiii',
	scardot_js_wrapper_object_setvar: function (p_id, p_key_type, p_key_ex, p_val_type, p_val_ex) {
		const obj = scardotJSWrapper.get_proxied_value(p_id);
		if (obj === undefined) {
			return -1;
		}
		const key = scardotJSWrapper.variant2js(p_key_type, p_key_ex);
		try {
			obj[key] = scardotJSWrapper.variant2js(p_val_type, p_val_ex);
			return 0;
		} catch (e) {
			scardotRuntime.error(`Error setting variable ${key} on object`, obj);
			return -1;
		}
	},

	scardot_js_wrapper_create_object__proxy: 'sync',
	scardot_js_wrapper_create_object__sig: 'iiiiiiii',
	scardot_js_wrapper_create_object: function (p_object, p_args, p_argc, p_convert_callback, p_exchange, p_lock, p_free_lock_callback) {
		const name = scardotRuntime.parseString(p_object);
		if (typeof (window[name]) === 'undefined') {
			return -1;
		}
		const convert = scardotRuntime.get_func(p_convert_callback);
		const freeLock = scardotRuntime.get_func(p_free_lock_callback);
		const args = new Array(p_argc);
		for (let i = 0; i < p_argc; i++) {
			const type = convert(p_args, i, p_exchange, p_lock);
			const lock = scardotRuntime.getHeapValue(p_lock, '*');
			args[i] = scardotJSWrapper.variant2js(type, p_exchange);
			if (lock) {
				freeLock(p_lock, type);
			}
		}
		try {
			const res = new window[name](...args);
			return scardotJSWrapper.js2variant(res, p_exchange);
		} catch (e) {
			scardotRuntime.error(`Error calling constructor ${name} with args:`, args, 'error:', e);
			return -1;
		}
	},
};

autoAddDeps(scardotJSWrapper, '$scardotJSWrapper');
mergeInto(LibraryManager.library, scardotJSWrapper);

const scardotEval = {
	scardot_js_eval__deps: ['$scardotRuntime'],
	scardot_js_eval__sig: 'iiiiiii',
	scardot_js_eval: function (p_js, p_use_global_ctx, p_union_ptr, p_byte_arr, p_byte_arr_write, p_callback) {
		const js_code = scardotRuntime.parseString(p_js);
		let eval_ret = null;
		try {
			if (p_use_global_ctx) {
				// indirect eval call grants global execution context
				const global_eval = eval; // eslint-disable-line no-eval
				eval_ret = global_eval(js_code);
			} else {
				eval_ret = eval(js_code); // eslint-disable-line no-eval
			}
		} catch (e) {
			scardotRuntime.error(e);
		}

		switch (typeof eval_ret) {
		case 'boolean':
			scardotRuntime.setHeapValue(p_union_ptr, eval_ret, 'i32');
			return 1; // BOOL

		case 'number':
			scardotRuntime.setHeapValue(p_union_ptr, eval_ret, 'double');
			return 3; // FLOAT

		case 'string':
			scardotRuntime.setHeapValue(p_union_ptr, scardotRuntime.allocString(eval_ret), '*');
			return 4; // STRING

		case 'object':
			if (eval_ret === null) {
				break;
			}

			if (ArrayBuffer.isView(eval_ret) && !(eval_ret instanceof Uint8Array)) {
				eval_ret = new Uint8Array(eval_ret.buffer);
			} else if (eval_ret instanceof ArrayBuffer) {
				eval_ret = new Uint8Array(eval_ret);
			}
			if (eval_ret instanceof Uint8Array) {
				const func = scardotRuntime.get_func(p_callback);
				const bytes_ptr = func(p_byte_arr, p_byte_arr_write, eval_ret.length);
				HEAPU8.set(eval_ret, bytes_ptr);
				return 29; // PACKED_BYTE_ARRAY
			}
			break;

			// no default
		}
		return 0; // NIL
	},
};

mergeInto(LibraryManager.library, scardotEval);
