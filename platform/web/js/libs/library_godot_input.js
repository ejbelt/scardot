/**************************************************************************/
/*  library_godot_input.js                                                */
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

/*
 * IME API helper.
 */

const scardotIME = {
	$scardotIME__deps: ['$scardotRuntime', '$scardotEventListeners'],
	$scardotIME__postset: 'scardotOS.atexit(function(resolve, reject) { scardotIME.clear(); resolve(); });',
	$scardotIME: {
		ime: null,
		active: false,

		getModifiers: function (evt) {
			return (evt.shiftKey + 0) + ((evt.altKey + 0) << 1) + ((evt.ctrlKey + 0) << 2) + ((evt.metaKey + 0) << 3);
		},

		ime_active: function (active) {
			function focus_timer() {
				scardotIME.active = true;
				scardotIME.ime.focus();
			}

			if (scardotIME.ime) {
				if (active) {
					scardotIME.ime.style.display = 'block';
					setInterval(focus_timer, 100);
				} else {
					scardotIME.ime.style.display = 'none';
					scardotConfig.canvas.focus();
					scardotIME.active = false;
				}
			}
		},

		ime_position: function (x, y) {
			if (scardotIME.ime) {
				const canvas = scardotConfig.canvas;
				const rect = canvas.getBoundingClientRect();
				const rw = canvas.width / rect.width;
				const rh = canvas.height / rect.height;
				const clx = (x / rw) + rect.x;
				const cly = (y / rh) + rect.y;

				scardotIME.ime.style.left = `${clx}px`;
				scardotIME.ime.style.top = `${cly}px`;
			}
		},

		init: function (ime_cb, key_cb, code, key) {
			function key_event_cb(pressed, evt) {
				const modifiers = scardotIME.getModifiers(evt);
				scardotRuntime.stringToHeap(evt.code, code, 32);
				scardotRuntime.stringToHeap(evt.key, key, 32);
				key_cb(pressed, evt.repeat, modifiers);
				evt.preventDefault();
			}
			function ime_event_cb(event) {
				if (scardotIME.ime) {
					if (event.type === 'compositionstart') {
						ime_cb(0, null);
						scardotIME.ime.innerHTML = '';
					} else if (event.type === 'compositionupdate') {
						const ptr = scardotRuntime.allocString(event.data);
						ime_cb(1, ptr);
						scardotRuntime.free(ptr);
					} else if (event.type === 'compositionend') {
						const ptr = scardotRuntime.allocString(event.data);
						ime_cb(2, ptr);
						scardotRuntime.free(ptr);
						scardotIME.ime.innerHTML = '';
					}
				}
			}

			const ime = document.createElement('div');
			ime.className = 'ime';
			ime.style.background = 'none';
			ime.style.opacity = 0.0;
			ime.style.position = 'fixed';
			ime.style.textAlign = 'left';
			ime.style.fontSize = '1px';
			ime.style.left = '0px';
			ime.style.top = '0px';
			ime.style.width = '100%';
			ime.style.height = '40px';
			ime.style.pointerEvents = 'none';
			ime.style.display = 'none';
			ime.contentEditable = 'true';

			scardotEventListeners.add(ime, 'compositionstart', ime_event_cb, false);
			scardotEventListeners.add(ime, 'compositionupdate', ime_event_cb, false);
			scardotEventListeners.add(ime, 'compositionend', ime_event_cb, false);
			scardotEventListeners.add(ime, 'keydown', key_event_cb.bind(null, 1), false);
			scardotEventListeners.add(ime, 'keyup', key_event_cb.bind(null, 0), false);

			ime.onblur = function () {
				this.style.display = 'none';
				scardotConfig.canvas.focus();
				scardotIME.active = false;
			};

			scardotConfig.canvas.parentElement.appendChild(ime);
			scardotIME.ime = ime;
		},

		clear: function () {
			if (scardotIME.ime) {
				scardotIME.ime.remove();
				scardotIME.ime = null;
			}
		},
	},
};
mergeInto(LibraryManager.library, scardotIME);

/*
 * Gamepad API helper.
 */
const scardotInputGamepads = {
	$scardotInputGamepads__deps: ['$scardotRuntime', '$scardotEventListeners'],
	$scardotInputGamepads: {
		samples: [],

		get_pads: function () {
			try {
				// Will throw in iframe when permission is denied.
				// Will throw/warn in the future for insecure contexts.
				// See https://github.com/w3c/gamepad/pull/120
				const pads = navigator.getGamepads();
				if (pads) {
					return pads;
				}
				return [];
			} catch (e) {
				return [];
			}
		},

		get_samples: function () {
			return scardotInputGamepads.samples;
		},

		get_sample: function (index) {
			const samples = scardotInputGamepads.samples;
			return index < samples.length ? samples[index] : null;
		},

		sample: function () {
			const pads = scardotInputGamepads.get_pads();
			const samples = [];
			for (let i = 0; i < pads.length; i++) {
				const pad = pads[i];
				if (!pad) {
					samples.push(null);
					continue;
				}
				const s = {
					standard: pad.mapping === 'standard',
					buttons: [],
					axes: [],
					connected: pad.connected,
				};
				for (let b = 0; b < pad.buttons.length; b++) {
					s.buttons.push(pad.buttons[b].value);
				}
				for (let a = 0; a < pad.axes.length; a++) {
					s.axes.push(pad.axes[a]);
				}
				samples.push(s);
			}
			scardotInputGamepads.samples = samples;
		},

		init: function (onchange) {
			scardotInputGamepads.samples = [];
			function add(pad) {
				const guid = scardotInputGamepads.get_guid(pad);
				const c_id = scardotRuntime.allocString(pad.id);
				const c_guid = scardotRuntime.allocString(guid);
				onchange(pad.index, 1, c_id, c_guid);
				scardotRuntime.free(c_id);
				scardotRuntime.free(c_guid);
			}
			const pads = scardotInputGamepads.get_pads();
			for (let i = 0; i < pads.length; i++) {
				// Might be reserved space.
				if (pads[i]) {
					add(pads[i]);
				}
			}
			scardotEventListeners.add(window, 'gamepadconnected', function (evt) {
				if (evt.gamepad) {
					add(evt.gamepad);
				}
			}, false);
			scardotEventListeners.add(window, 'gamepaddisconnected', function (evt) {
				if (evt.gamepad) {
					onchange(evt.gamepad.index, 0);
				}
			}, false);
		},

		get_guid: function (pad) {
			if (pad.mapping) {
				return pad.mapping;
			}
			const ua = navigator.userAgent;
			let os = 'Unknown';
			if (ua.indexOf('Android') >= 0) {
				os = 'Android';
			} else if (ua.indexOf('Linux') >= 0) {
				os = 'Linux';
			} else if (ua.indexOf('iPhone') >= 0) {
				os = 'iOS';
			} else if (ua.indexOf('Macintosh') >= 0) {
				// Updated iPads will fall into this category.
				os = 'MacOSX';
			} else if (ua.indexOf('Windows') >= 0) {
				os = 'Windows';
			}

			const id = pad.id;
			// Chrom* style: NAME (Vendor: xxxx Product: xxxx).
			const exp1 = /vendor: ([0-9a-f]{4}) product: ([0-9a-f]{4})/i;
			// Firefox/Safari style (Safari may remove leading zeroes).
			const exp2 = /^([0-9a-f]+)-([0-9a-f]+)-/i;
			let vendor = '';
			let product = '';
			if (exp1.test(id)) {
				const match = exp1.exec(id);
				vendor = match[1].padStart(4, '0');
				product = match[2].padStart(4, '0');
			} else if (exp2.test(id)) {
				const match = exp2.exec(id);
				vendor = match[1].padStart(4, '0');
				product = match[2].padStart(4, '0');
			}
			if (!vendor || !product) {
				return `${os}Unknown`;
			}
			return os + vendor + product;
		},
	},
};
mergeInto(LibraryManager.library, scardotInputGamepads);

/*
 * Drag and drop helper.
 * This is pretty big, but basically detect dropped files on scardotConfig.canvas,
 * process them one by one (recursively for directories), and copies them to
 * the temporary FS path '/tmp/drop-[random]/' so it can be emitted as a godot
 * event (that requires a string array of paths).
 *
 * NOTE: The temporary files are removed after the callback. This means that
 * deferred callbacks won't be able to access the files.
 */
const scardotInputDragDrop = {
	$scardotInputDragDrop__deps: ['$FS', '$scardotFS'],
	$scardotInputDragDrop: {
		promises: [],
		pending_files: [],

		add_entry: function (entry) {
			if (entry.isDirectory) {
				scardotInputDragDrop.add_dir(entry);
			} else if (entry.isFile) {
				scardotInputDragDrop.add_file(entry);
			} else {
				scardotRuntime.error('Unrecognized entry...', entry);
			}
		},

		add_dir: function (entry) {
			scardotInputDragDrop.promises.push(new Promise(function (resolve, reject) {
				const reader = entry.createReader();
				reader.readEntries(function (entries) {
					for (let i = 0; i < entries.length; i++) {
						scardotInputDragDrop.add_entry(entries[i]);
					}
					resolve();
				});
			}));
		},

		add_file: function (entry) {
			scardotInputDragDrop.promises.push(new Promise(function (resolve, reject) {
				entry.file(function (file) {
					const reader = new FileReader();
					reader.onload = function () {
						const f = {
							'path': file.relativePath || file.webkitRelativePath,
							'name': file.name,
							'type': file.type,
							'size': file.size,
							'data': reader.result,
						};
						if (!f['path']) {
							f['path'] = f['name'];
						}
						scardotInputDragDrop.pending_files.push(f);
						resolve();
					};
					reader.onerror = function () {
						scardotRuntime.print('Error reading file');
						reject();
					};
					reader.readAsArrayBuffer(file);
				}, function (err) {
					scardotRuntime.print('Error!');
					reject();
				});
			}));
		},

		process: function (resolve, reject) {
			if (scardotInputDragDrop.promises.length === 0) {
				resolve();
				return;
			}
			scardotInputDragDrop.promises.pop().then(function () {
				setTimeout(function () {
					scardotInputDragDrop.process(resolve, reject);
				}, 0);
			});
		},

		_process_event: function (ev, callback) {
			ev.preventDefault();
			if (ev.dataTransfer.items) {
				// Use DataTransferItemList interface to access the file(s)
				for (let i = 0; i < ev.dataTransfer.items.length; i++) {
					const item = ev.dataTransfer.items[i];
					let entry = null;
					if ('getAsEntry' in item) {
						entry = item.getAsEntry();
					} else if ('webkitGetAsEntry' in item) {
						entry = item.webkitGetAsEntry();
					}
					if (entry) {
						scardotInputDragDrop.add_entry(entry);
					}
				}
			} else {
				scardotRuntime.error('File upload not supported');
			}
			new Promise(scardotInputDragDrop.process).then(function () {
				const DROP = `/tmp/drop-${parseInt(Math.random() * (1 << 30), 10)}/`;
				const drops = [];
				const files = [];
				FS.mkdir(DROP.slice(0, -1)); // Without trailing slash
				scardotInputDragDrop.pending_files.forEach((elem) => {
					const path = elem['path'];
					scardotFS.copy_to_fs(DROP + path, elem['data']);
					let idx = path.indexOf('/');
					if (idx === -1) {
						// Root file
						drops.push(DROP + path);
					} else {
						// Subdir
						const sub = path.substr(0, idx);
						idx = sub.indexOf('/');
						if (idx < 0 && drops.indexOf(DROP + sub) === -1) {
							drops.push(DROP + sub);
						}
					}
					files.push(DROP + path);
				});
				scardotInputDragDrop.promises = [];
				scardotInputDragDrop.pending_files = [];
				callback(drops);
				if (scardotConfig.persistent_drops) {
					// Delay removal at exit.
					scardotOS.atexit(function (resolve, reject) {
						scardotInputDragDrop.remove_drop(files, DROP);
						resolve();
					});
				} else {
					scardotInputDragDrop.remove_drop(files, DROP);
				}
			});
		},

		remove_drop: function (files, drop_path) {
			const dirs = [drop_path.substr(0, drop_path.length - 1)];
			// Remove temporary files
			files.forEach(function (file) {
				FS.unlink(file);
				let dir = file.replace(drop_path, '');
				let idx = dir.lastIndexOf('/');
				while (idx > 0) {
					dir = dir.substr(0, idx);
					if (dirs.indexOf(drop_path + dir) === -1) {
						dirs.push(drop_path + dir);
					}
					idx = dir.lastIndexOf('/');
				}
			});
			// Remove dirs.
			dirs.sort(function (a, b) {
				const al = (a.match(/\//g) || []).length;
				const bl = (b.match(/\//g) || []).length;
				if (al > bl) {
					return -1;
				} else if (al < bl) {
					return 1;
				}
				return 0;
			}).forEach(function (dir) {
				FS.rmdir(dir);
			});
		},

		handler: function (callback) {
			return function (ev) {
				scardotInputDragDrop._process_event(ev, callback);
			};
		},
	},
};
mergeInto(LibraryManager.library, scardotInputDragDrop);

/*
 * scardot exposed input functions.
 */
const scardotInput = {
	$scardotInput__deps: ['$scardotRuntime', '$scardotConfig', '$scardotEventListeners', '$scardotInputGamepads', '$scardotInputDragDrop', '$scardotIME'],
	$scardotInput: {
		getModifiers: function (evt) {
			return (evt.shiftKey + 0) + ((evt.altKey + 0) << 1) + ((evt.ctrlKey + 0) << 2) + ((evt.metaKey + 0) << 3);
		},
		computePosition: function (evt, rect) {
			const canvas = scardotConfig.canvas;
			const rw = canvas.width / rect.width;
			const rh = canvas.height / rect.height;
			const x = (evt.clientX - rect.x) * rw;
			const y = (evt.clientY - rect.y) * rh;
			return [x, y];
		},
	},

	/*
	 * Mouse API
	 */
	godot_js_input_mouse_move_cb__proxy: 'sync',
	godot_js_input_mouse_move_cb__sig: 'vi',
	godot_js_input_mouse_move_cb: function (callback) {
		const func = scardotRuntime.get_func(callback);
		const canvas = scardotConfig.canvas;
		function move_cb(evt) {
			const rect = canvas.getBoundingClientRect();
			const pos = scardotInput.computePosition(evt, rect);
			// Scale movement
			const rw = canvas.width / rect.width;
			const rh = canvas.height / rect.height;
			const rel_pos_x = evt.movementX * rw;
			const rel_pos_y = evt.movementY * rh;
			const modifiers = scardotInput.getModifiers(evt);
			func(pos[0], pos[1], rel_pos_x, rel_pos_y, modifiers);
		}
		scardotEventListeners.add(window, 'mousemove', move_cb, false);
	},

	godot_js_input_mouse_wheel_cb__proxy: 'sync',
	godot_js_input_mouse_wheel_cb__sig: 'vi',
	godot_js_input_mouse_wheel_cb: function (callback) {
		const func = scardotRuntime.get_func(callback);
		function wheel_cb(evt) {
			if (func(evt['deltaX'] || 0, evt['deltaY'] || 0)) {
				evt.preventDefault();
			}
		}
		scardotEventListeners.add(scardotConfig.canvas, 'wheel', wheel_cb, false);
	},

	godot_js_input_mouse_button_cb__proxy: 'sync',
	godot_js_input_mouse_button_cb__sig: 'vi',
	godot_js_input_mouse_button_cb: function (callback) {
		const func = scardotRuntime.get_func(callback);
		const canvas = scardotConfig.canvas;
		function button_cb(p_pressed, evt) {
			const rect = canvas.getBoundingClientRect();
			const pos = scardotInput.computePosition(evt, rect);
			const modifiers = scardotInput.getModifiers(evt);
			// Since the event is consumed, focus manually.
			// NOTE: The iframe container may not have focus yet, so focus even when already active.
			if (p_pressed) {
				scardotConfig.canvas.focus();
			}
			if (func(p_pressed, evt.button, pos[0], pos[1], modifiers)) {
				evt.preventDefault();
			}
		}
		scardotEventListeners.add(canvas, 'mousedown', button_cb.bind(null, 1), false);
		scardotEventListeners.add(window, 'mouseup', button_cb.bind(null, 0), false);
	},

	/*
	 * Touch API
	 */
	godot_js_input_touch_cb__proxy: 'sync',
	godot_js_input_touch_cb__sig: 'viii',
	godot_js_input_touch_cb: function (callback, ids, coords) {
		const func = scardotRuntime.get_func(callback);
		const canvas = scardotConfig.canvas;
		function touch_cb(type, evt) {
			// Since the event is consumed, focus manually.
			// NOTE: The iframe container may not have focus yet, so focus even when already active.
			if (type === 0) {
				scardotConfig.canvas.focus();
			}
			const rect = canvas.getBoundingClientRect();
			const touches = evt.changedTouches;
			for (let i = 0; i < touches.length; i++) {
				const touch = touches[i];
				const pos = scardotInput.computePosition(touch, rect);
				scardotRuntime.setHeapValue(coords + (i * 2) * 8, pos[0], 'double');
				scardotRuntime.setHeapValue(coords + (i * 2 + 1) * 8, pos[1], 'double');
				scardotRuntime.setHeapValue(ids + i * 4, touch.identifier, 'i32');
			}
			func(type, touches.length);
			if (evt.cancelable) {
				evt.preventDefault();
			}
		}
		scardotEventListeners.add(canvas, 'touchstart', touch_cb.bind(null, 0), false);
		scardotEventListeners.add(canvas, 'touchend', touch_cb.bind(null, 1), false);
		scardotEventListeners.add(canvas, 'touchcancel', touch_cb.bind(null, 1), false);
		scardotEventListeners.add(canvas, 'touchmove', touch_cb.bind(null, 2), false);
	},

	/*
	 * Key API
	 */
	godot_js_input_key_cb__proxy: 'sync',
	godot_js_input_key_cb__sig: 'viii',
	godot_js_input_key_cb: function (callback, code, key) {
		const func = scardotRuntime.get_func(callback);
		function key_cb(pressed, evt) {
			const modifiers = scardotInput.getModifiers(evt);
			scardotRuntime.stringToHeap(evt.code, code, 32);
			scardotRuntime.stringToHeap(evt.key, key, 32);
			func(pressed, evt.repeat, modifiers);
			evt.preventDefault();
		}
		scardotEventListeners.add(scardotConfig.canvas, 'keydown', key_cb.bind(null, 1), false);
		scardotEventListeners.add(scardotConfig.canvas, 'keyup', key_cb.bind(null, 0), false);
	},

	/*
	 * IME API
	 */
	godot_js_set_ime_active__proxy: 'sync',
	godot_js_set_ime_active__sig: 'vi',
	godot_js_set_ime_active: function (p_active) {
		scardotIME.ime_active(p_active);
	},

	godot_js_set_ime_position__proxy: 'sync',
	godot_js_set_ime_position__sig: 'vii',
	godot_js_set_ime_position: function (p_x, p_y) {
		scardotIME.ime_position(p_x, p_y);
	},

	godot_js_set_ime_cb__proxy: 'sync',
	godot_js_set_ime_cb__sig: 'viiii',
	godot_js_set_ime_cb: function (p_ime_cb, p_key_cb, code, key) {
		const ime_cb = scardotRuntime.get_func(p_ime_cb);
		const key_cb = scardotRuntime.get_func(p_key_cb);
		scardotIME.init(ime_cb, key_cb, code, key);
	},

	godot_js_is_ime_focused__proxy: 'sync',
	godot_js_is_ime_focused__sig: 'i',
	godot_js_is_ime_focused: function () {
		return scardotIME.active;
	},

	/*
	 * Gamepad API
	 */
	godot_js_input_gamepad_cb__proxy: 'sync',
	godot_js_input_gamepad_cb__sig: 'vi',
	godot_js_input_gamepad_cb: function (change_cb) {
		const onchange = scardotRuntime.get_func(change_cb);
		scardotInputGamepads.init(onchange);
	},

	godot_js_input_gamepad_sample_count__proxy: 'sync',
	godot_js_input_gamepad_sample_count__sig: 'i',
	godot_js_input_gamepad_sample_count: function () {
		return scardotInputGamepads.get_samples().length;
	},

	godot_js_input_gamepad_sample__proxy: 'sync',
	godot_js_input_gamepad_sample__sig: 'i',
	godot_js_input_gamepad_sample: function () {
		scardotInputGamepads.sample();
		return 0;
	},

	godot_js_input_gamepad_sample_get__proxy: 'sync',
	godot_js_input_gamepad_sample_get__sig: 'iiiiiii',
	godot_js_input_gamepad_sample_get: function (p_index, r_btns, r_btns_num, r_axes, r_axes_num, r_standard) {
		const sample = scardotInputGamepads.get_sample(p_index);
		if (!sample || !sample.connected) {
			return 1;
		}
		const btns = sample.buttons;
		const btns_len = btns.length < 16 ? btns.length : 16;
		for (let i = 0; i < btns_len; i++) {
			scardotRuntime.setHeapValue(r_btns + (i << 2), btns[i], 'float');
		}
		scardotRuntime.setHeapValue(r_btns_num, btns_len, 'i32');
		const axes = sample.axes;
		const axes_len = axes.length < 10 ? axes.length : 10;
		for (let i = 0; i < axes_len; i++) {
			scardotRuntime.setHeapValue(r_axes + (i << 2), axes[i], 'float');
		}
		scardotRuntime.setHeapValue(r_axes_num, axes_len, 'i32');
		const is_standard = sample.standard ? 1 : 0;
		scardotRuntime.setHeapValue(r_standard, is_standard, 'i32');
		return 0;
	},

	/*
	 * Drag/Drop API
	 */
	godot_js_input_drop_files_cb__proxy: 'sync',
	godot_js_input_drop_files_cb__sig: 'vi',
	godot_js_input_drop_files_cb: function (callback) {
		const func = scardotRuntime.get_func(callback);
		const dropFiles = function (files) {
			const args = files || [];
			if (!args.length) {
				return;
			}
			const argc = args.length;
			const argv = scardotRuntime.allocStringArray(args);
			func(argv, argc);
			scardotRuntime.freeStringArray(argv, argc);
		};
		const canvas = scardotConfig.canvas;
		scardotEventListeners.add(canvas, 'dragover', function (ev) {
			// Prevent default behavior (which would try to open the file(s))
			ev.preventDefault();
		}, false);
		scardotEventListeners.add(canvas, 'drop', scardotInputDragDrop.handler(dropFiles));
	},

	/* Paste API */
	godot_js_input_paste_cb__proxy: 'sync',
	godot_js_input_paste_cb__sig: 'vi',
	godot_js_input_paste_cb: function (callback) {
		const func = scardotRuntime.get_func(callback);
		scardotEventListeners.add(window, 'paste', function (evt) {
			const text = evt.clipboardData.getData('text');
			const ptr = scardotRuntime.allocString(text);
			func(ptr);
			scardotRuntime.free(ptr);
		}, false);
	},

	godot_js_input_vibrate_handheld__proxy: 'sync',
	godot_js_input_vibrate_handheld__sig: 'vi',
	godot_js_input_vibrate_handheld: function (p_duration_ms) {
		if (typeof navigator.vibrate !== 'function') {
			scardotRuntime.print('This browser does not support vibration.');
		} else {
			navigator.vibrate(p_duration_ms);
		}
	},
};

autoAddDeps(scardotInput, '$scardotInput');
mergeInto(LibraryManager.library, scardotInput);
