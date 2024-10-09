/**************************************************************************/
/*  library_godot_os.js                                                   */
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

const IDHandler = {
	$IDHandler: {
		_last_id: 0,
		_references: {},

		get: function (p_id) {
			return IDHandler._references[p_id];
		},

		add: function (p_data) {
			const id = ++IDHandler._last_id;
			IDHandler._references[id] = p_data;
			return id;
		},

		remove: function (p_id) {
			delete IDHandler._references[p_id];
		},
	},
};

autoAddDeps(IDHandler, '$IDHandler');
mergeInto(LibraryManager.library, IDHandler);

const scardotConfig = {
	$scardotConfig__postset: 'Module["initConfig"] = scardotConfig.init_config;',
	$scardotConfig__deps: ['$scardotRuntime'],
	$scardotConfig: {
		canvas: null,
		locale: 'en',
		canvas_resize_policy: 2, // Adaptive
		virtual_keyboard: false,
		persistent_drops: false,
		on_execute: null,
		on_exit: null,

		init_config: function (p_opts) {
			scardotConfig.canvas_resize_policy = p_opts['canvasResizePolicy'];
			scardotConfig.canvas = p_opts['canvas'];
			scardotConfig.locale = p_opts['locale'] || scardotConfig.locale;
			scardotConfig.virtual_keyboard = p_opts['virtualKeyboard'];
			scardotConfig.persistent_drops = !!p_opts['persistentDrops'];
			scardotConfig.on_execute = p_opts['onExecute'];
			scardotConfig.on_exit = p_opts['onExit'];
			if (p_opts['focusCanvas']) {
				scardotConfig.canvas.focus();
			}
		},

		locate_file: function (file) {
			return Module['locateFile'](file);
		},
		clear: function () {
			scardotConfig.canvas = null;
			scardotConfig.locale = 'en';
			scardotConfig.canvas_resize_policy = 2;
			scardotConfig.virtual_keyboard = false;
			scardotConfig.persistent_drops = false;
			scardotConfig.on_execute = null;
			scardotConfig.on_exit = null;
		},
	},

	godot_js_config_canvas_id_get__proxy: 'sync',
	godot_js_config_canvas_id_get__sig: 'vii',
	godot_js_config_canvas_id_get: function (p_ptr, p_ptr_max) {
		scardotRuntime.stringToHeap(`#${scardotConfig.canvas.id}`, p_ptr, p_ptr_max);
	},

	godot_js_config_locale_get__proxy: 'sync',
	godot_js_config_locale_get__sig: 'vii',
	godot_js_config_locale_get: function (p_ptr, p_ptr_max) {
		scardotRuntime.stringToHeap(scardotConfig.locale, p_ptr, p_ptr_max);
	},
};

autoAddDeps(scardotConfig, '$scardotConfig');
mergeInto(LibraryManager.library, scardotConfig);

const scardotFS = {
	$scardotFS__deps: ['$FS', '$IDBFS', '$scardotRuntime'],
	$scardotFS__postset: [
		'Module["initFS"] = scardotFS.init;',
		'Module["copyToFS"] = scardotFS.copy_to_fs;',
	].join(''),
	$scardotFS: {
		// ERRNO_CODES works every odd version of emscripten, but this will break too eventually.
		ENOENT: 44,
		_idbfs: false,
		_syncing: false,
		_mount_points: [],

		is_persistent: function () {
			return scardotFS._idbfs ? 1 : 0;
		},

		// Initialize godot file system, setting up persistent paths.
		// Returns a promise that resolves when the FS is ready.
		// We keep track of mount_points, so that we can properly close the IDBFS
		// since emscripten is not doing it by itself. (emscripten GH#12516).
		init: function (persistentPaths) {
			scardotFS._idbfs = false;
			if (!Array.isArray(persistentPaths)) {
				return Promise.reject(new Error('Persistent paths must be an array'));
			}
			if (!persistentPaths.length) {
				return Promise.resolve();
			}
			scardotFS._mount_points = persistentPaths.slice();

			function createRecursive(dir) {
				try {
					FS.stat(dir);
				} catch (e) {
					if (e.errno !== scardotFS.ENOENT) {
						// Let mkdirTree throw in case, we cannot trust the above check.
						scardotRuntime.error(e);
					}
					FS.mkdirTree(dir);
				}
			}

			scardotFS._mount_points.forEach(function (path) {
				createRecursive(path);
				FS.mount(IDBFS, {}, path);
			});
			return new Promise(function (resolve, reject) {
				FS.syncfs(true, function (err) {
					if (err) {
						scardotFS._mount_points = [];
						scardotFS._idbfs = false;
						scardotRuntime.print(`IndexedDB not available: ${err.message}`);
					} else {
						scardotFS._idbfs = true;
					}
					resolve(err);
				});
			});
		},

		// Deinit godot file system, making sure to unmount file systems, and close IDBFS(s).
		deinit: function () {
			scardotFS._mount_points.forEach(function (path) {
				try {
					FS.unmount(path);
				} catch (e) {
					scardotRuntime.print('Already unmounted', e);
				}
				if (scardotFS._idbfs && IDBFS.dbs[path]) {
					IDBFS.dbs[path].close();
					delete IDBFS.dbs[path];
				}
			});
			scardotFS._mount_points = [];
			scardotFS._idbfs = false;
			scardotFS._syncing = false;
		},

		sync: function () {
			if (scardotFS._syncing) {
				scardotRuntime.error('Already syncing!');
				return Promise.resolve();
			}
			scardotFS._syncing = true;
			return new Promise(function (resolve, reject) {
				FS.syncfs(false, function (error) {
					if (error) {
						scardotRuntime.error(`Failed to save IDB file system: ${error.message}`);
					}
					scardotFS._syncing = false;
					resolve(error);
				});
			});
		},

		// Copies a buffer to the internal file system. Creating directories recursively.
		copy_to_fs: function (path, buffer) {
			const idx = path.lastIndexOf('/');
			let dir = '/';
			if (idx > 0) {
				dir = path.slice(0, idx);
			}
			try {
				FS.stat(dir);
			} catch (e) {
				if (e.errno !== scardotFS.ENOENT) {
					// Let mkdirTree throw in case, we cannot trust the above check.
					scardotRuntime.error(e);
				}
				FS.mkdirTree(dir);
			}
			FS.writeFile(path, new Uint8Array(buffer));
		},
	},
};
mergeInto(LibraryManager.library, scardotFS);

const scardotOS = {
	$scardotOS__deps: ['$scardotRuntime', '$scardotConfig', '$scardotFS'],
	$scardotOS__postset: [
		'Module["request_quit"] = function() { scardotOS.request_quit() };',
		'Module["onExit"] = scardotOS.cleanup;',
		'scardotOS._fs_sync_promise = Promise.resolve();',
	].join(''),
	$scardotOS: {
		request_quit: function () {},
		_async_cbs: [],
		_fs_sync_promise: null,

		atexit: function (p_promise_cb) {
			scardotOS._async_cbs.push(p_promise_cb);
		},

		cleanup: function (exit_code) {
			const cb = scardotConfig.on_exit;
			scardotFS.deinit();
			scardotConfig.clear();
			if (cb) {
				cb(exit_code);
			}
		},

		finish_async: function (callback) {
			scardotOS._fs_sync_promise.then(function (err) {
				const promises = [];
				scardotOS._async_cbs.forEach(function (cb) {
					promises.push(new Promise(cb));
				});
				return Promise.all(promises);
			}).then(function () {
				return scardotFS.sync(); // Final FS sync.
			}).then(function (err) {
				// Always deferred.
				setTimeout(function () {
					callback();
				}, 0);
			});
		},
	},

	godot_js_os_finish_async__proxy: 'sync',
	godot_js_os_finish_async__sig: 'vi',
	godot_js_os_finish_async: function (p_callback) {
		const func = scardotRuntime.get_func(p_callback);
		scardotOS.finish_async(func);
	},

	godot_js_os_request_quit_cb__proxy: 'sync',
	godot_js_os_request_quit_cb__sig: 'vi',
	godot_js_os_request_quit_cb: function (p_callback) {
		scardotOS.request_quit = scardotRuntime.get_func(p_callback);
	},

	godot_js_os_fs_is_persistent__proxy: 'sync',
	godot_js_os_fs_is_persistent__sig: 'i',
	godot_js_os_fs_is_persistent: function () {
		return scardotFS.is_persistent();
	},

	godot_js_os_fs_sync__proxy: 'sync',
	godot_js_os_fs_sync__sig: 'vi',
	godot_js_os_fs_sync: function (callback) {
		const func = scardotRuntime.get_func(callback);
		scardotOS._fs_sync_promise = scardotFS.sync();
		scardotOS._fs_sync_promise.then(function (err) {
			func();
		});
	},

	godot_js_os_has_feature__proxy: 'sync',
	godot_js_os_has_feature__sig: 'ii',
	godot_js_os_has_feature: function (p_ftr) {
		const ftr = scardotRuntime.parseString(p_ftr);
		const ua = navigator.userAgent;
		if (ftr === 'web_macos') {
			return (ua.indexOf('Mac') !== -1) ? 1 : 0;
		}
		if (ftr === 'web_windows') {
			return (ua.indexOf('Windows') !== -1) ? 1 : 0;
		}
		if (ftr === 'web_android') {
			return (ua.indexOf('Android') !== -1) ? 1 : 0;
		}
		if (ftr === 'web_ios') {
			return ((ua.indexOf('iPhone') !== -1) || (ua.indexOf('iPad') !== -1) || (ua.indexOf('iPod') !== -1)) ? 1 : 0;
		}
		if (ftr === 'web_linuxbsd') {
			return ((ua.indexOf('CrOS') !== -1) || (ua.indexOf('BSD') !== -1) || (ua.indexOf('Linux') !== -1) || (ua.indexOf('X11') !== -1)) ? 1 : 0;
		}
		return 0;
	},

	godot_js_os_execute__proxy: 'sync',
	godot_js_os_execute__sig: 'ii',
	godot_js_os_execute: function (p_json) {
		const json_args = scardotRuntime.parseString(p_json);
		const args = JSON.parse(json_args);
		if (scardotConfig.on_execute) {
			scardotConfig.on_execute(args);
			return 0;
		}
		return 1;
	},

	godot_js_os_shell_open__proxy: 'sync',
	godot_js_os_shell_open__sig: 'vi',
	godot_js_os_shell_open: function (p_uri) {
		window.open(scardotRuntime.parseString(p_uri), '_blank');
	},

	godot_js_os_hw_concurrency_get__proxy: 'sync',
	godot_js_os_hw_concurrency_get__sig: 'i',
	godot_js_os_hw_concurrency_get: function () {
		// TODO scardot core needs fixing to avoid spawning too many threads (> 24).
		const concurrency = navigator.hardwareConcurrency || 1;
		return concurrency < 2 ? concurrency : 2;
	},

	godot_js_os_download_buffer__proxy: 'sync',
	godot_js_os_download_buffer__sig: 'viiii',
	godot_js_os_download_buffer: function (p_ptr, p_size, p_name, p_mime) {
		const buf = scardotRuntime.heapSlice(HEAP8, p_ptr, p_size);
		const name = scardotRuntime.parseString(p_name);
		const mime = scardotRuntime.parseString(p_mime);
		const blob = new Blob([buf], { type: mime });
		const url = window.URL.createObjectURL(blob);
		const a = document.createElement('a');
		a.href = url;
		a.download = name;
		a.style.display = 'none';
		document.body.appendChild(a);
		a.click();
		a.remove();
		window.URL.revokeObjectURL(url);
	},
};

autoAddDeps(scardotOS, '$scardotOS');
mergeInto(LibraryManager.library, scardotOS);

/*
 * scardot event listeners.
 * Keeps track of registered event listeners so it can remove them on shutdown.
 */
const scardotEventListeners = {
	$scardotEventListeners__deps: ['$scardotOS'],
	$scardotEventListeners__postset: 'scardotOS.atexit(function(resolve, reject) { scardotEventListeners.clear(); resolve(); });',
	$scardotEventListeners: {
		handlers: [],

		has: function (target, event, method, capture) {
			return scardotEventListeners.handlers.findIndex(function (e) {
				return e.target === target && e.event === event && e.method === method && e.capture === capture;
			}) !== -1;
		},

		add: function (target, event, method, capture) {
			if (scardotEventListeners.has(target, event, method, capture)) {
				return;
			}
			function Handler(p_target, p_event, p_method, p_capture) {
				this.target = p_target;
				this.event = p_event;
				this.method = p_method;
				this.capture = p_capture;
			}
			scardotEventListeners.handlers.push(new Handler(target, event, method, capture));
			target.addEventListener(event, method, capture);
		},

		clear: function () {
			scardotEventListeners.handlers.forEach(function (h) {
				h.target.removeEventListener(h.event, h.method, h.capture);
			});
			scardotEventListeners.handlers.length = 0;
		},
	},
};
mergeInto(LibraryManager.library, scardotEventListeners);

const scardotPWA = {

	$scardotPWA__deps: ['$scardotRuntime', '$scardotEventListeners'],
	$scardotPWA: {
		hasUpdate: false,

		updateState: function (cb, reg) {
			if (!reg) {
				return;
			}
			if (!reg.active) {
				return;
			}
			if (reg.waiting) {
				scardotPWA.hasUpdate = true;
				cb();
			}
			scardotEventListeners.add(reg, 'updatefound', function () {
				const installing = reg.installing;
				scardotEventListeners.add(installing, 'statechange', function () {
					if (installing.state === 'installed') {
						scardotPWA.hasUpdate = true;
						cb();
					}
				});
			});
		},
	},

	godot_js_pwa_cb__proxy: 'sync',
	godot_js_pwa_cb__sig: 'vi',
	godot_js_pwa_cb: function (p_update_cb) {
		if ('serviceWorker' in navigator) {
			const cb = scardotRuntime.get_func(p_update_cb);
			navigator.serviceWorker.getRegistration().then(scardotPWA.updateState.bind(null, cb));
		}
	},

	godot_js_pwa_update__proxy: 'sync',
	godot_js_pwa_update__sig: 'i',
	godot_js_pwa_update: function () {
		if ('serviceWorker' in navigator && scardotPWA.hasUpdate) {
			navigator.serviceWorker.getRegistration().then(function (reg) {
				if (!reg || !reg.waiting) {
					return;
				}
				reg.waiting.postMessage('update');
			});
			return 0;
		}
		return 1;
	},
};

autoAddDeps(scardotPWA, '$scardotPWA');
mergeInto(LibraryManager.library, scardotPWA);
