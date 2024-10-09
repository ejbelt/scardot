/**************************************************************************/
/*  library_scardot_fetch.js                                                */
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

const scardotFetch = {
	$scardotFetch__deps: ['$IDHandler', '$scardotRuntime'],
	$scardotFetch: {

		onread: function (id, result) {
			const obj = IDHandler.get(id);
			if (!obj) {
				return;
			}
			if (result.value) {
				obj.chunks.push(result.value);
			}
			obj.reading = false;
			obj.done = result.done;
		},

		onresponse: function (id, response) {
			const obj = IDHandler.get(id);
			if (!obj) {
				return;
			}
			let chunked = false;
			response.headers.forEach(function (value, header) {
				const v = value.toLowerCase().trim();
				const h = header.toLowerCase().trim();
				if (h === 'transfer-encoding' && v === 'chunked') {
					chunked = true;
				}
			});
			obj.status = response.status;
			obj.response = response;
			obj.reader = response.body.getReader();
			obj.chunked = chunked;
		},

		onerror: function (id, err) {
			scardotRuntime.error(err);
			const obj = IDHandler.get(id);
			if (!obj) {
				return;
			}
			obj.error = err;
		},

		create: function (method, url, headers, body) {
			const obj = {
				request: null,
				response: null,
				reader: null,
				error: null,
				done: false,
				reading: false,
				status: 0,
				chunks: [],
			};
			const id = IDHandler.add(obj);
			const init = {
				method: method,
				headers: headers,
				body: body,
			};
			obj.request = fetch(url, init);
			obj.request.then(scardotFetch.onresponse.bind(null, id)).catch(scardotFetch.onerror.bind(null, id));
			return id;
		},

		free: function (id) {
			const obj = IDHandler.get(id);
			if (!obj) {
				return;
			}
			IDHandler.remove(id);
			if (!obj.request) {
				return;
			}
			// Try to abort
			obj.request.then(function (response) {
				response.abort();
			}).catch(function (e) { /* nothing to do */ });
		},

		read: function (id) {
			const obj = IDHandler.get(id);
			if (!obj) {
				return;
			}
			if (obj.reader && !obj.reading) {
				if (obj.done) {
					obj.reader = null;
					return;
				}
				obj.reading = true;
				obj.reader.read().then(scardotFetch.onread.bind(null, id)).catch(scardotFetch.onerror.bind(null, id));
			}
		},
	},

	scardot_js_fetch_create__proxy: 'sync',
	scardot_js_fetch_create__sig: 'iiiiiii',
	scardot_js_fetch_create: function (p_method, p_url, p_headers, p_headers_size, p_body, p_body_size) {
		const method = scardotRuntime.parseString(p_method);
		const url = scardotRuntime.parseString(p_url);
		const headers = scardotRuntime.parseStringArray(p_headers, p_headers_size);
		const body = p_body_size ? scardotRuntime.heapSlice(HEAP8, p_body, p_body_size) : null;
		return scardotFetch.create(method, url, headers.map(function (hv) {
			const idx = hv.indexOf(':');
			if (idx <= 0) {
				return [];
			}
			return [
				hv.slice(0, idx).trim(),
				hv.slice(idx + 1).trim(),
			];
		}).filter(function (v) {
			return v.length === 2;
		}), body);
	},

	scardot_js_fetch_state_get__proxy: 'sync',
	scardot_js_fetch_state_get__sig: 'ii',
	scardot_js_fetch_state_get: function (p_id) {
		const obj = IDHandler.get(p_id);
		if (!obj) {
			return -1;
		}
		if (obj.error) {
			return -1;
		}
		if (!obj.response) {
			return 0;
		}
		if (obj.reader) {
			return 1;
		}
		if (obj.done) {
			return 2;
		}
		return -1;
	},

	scardot_js_fetch_http_status_get__proxy: 'sync',
	scardot_js_fetch_http_status_get__sig: 'ii',
	scardot_js_fetch_http_status_get: function (p_id) {
		const obj = IDHandler.get(p_id);
		if (!obj || !obj.response) {
			return 0;
		}
		return obj.status;
	},

	scardot_js_fetch_read_headers__proxy: 'sync',
	scardot_js_fetch_read_headers__sig: 'iiii',
	scardot_js_fetch_read_headers: function (p_id, p_parse_cb, p_ref) {
		const obj = IDHandler.get(p_id);
		if (!obj || !obj.response) {
			return 1;
		}
		const cb = scardotRuntime.get_func(p_parse_cb);
		const arr = [];
		obj.response.headers.forEach(function (v, h) {
			arr.push(`${h}:${v}`);
		});
		const c_ptr = scardotRuntime.allocStringArray(arr);
		cb(arr.length, c_ptr, p_ref);
		scardotRuntime.freeStringArray(c_ptr, arr.length);
		return 0;
	},

	scardot_js_fetch_read_chunk__proxy: 'sync',
	scardot_js_fetch_read_chunk__sig: 'iiii',
	scardot_js_fetch_read_chunk: function (p_id, p_buf, p_buf_size) {
		const obj = IDHandler.get(p_id);
		if (!obj || !obj.response) {
			return 0;
		}
		let to_read = p_buf_size;
		const chunks = obj.chunks;
		while (to_read && chunks.length) {
			const chunk = obj.chunks[0];
			if (chunk.length > to_read) {
				scardotRuntime.heapCopy(HEAP8, chunk.slice(0, to_read), p_buf);
				chunks[0] = chunk.slice(to_read);
				to_read = 0;
			} else {
				scardotRuntime.heapCopy(HEAP8, chunk, p_buf);
				to_read -= chunk.length;
				chunks.pop();
			}
		}
		if (!chunks.length) {
			scardotFetch.read(p_id);
		}
		return p_buf_size - to_read;
	},

	scardot_js_fetch_is_chunked__proxy: 'sync',
	scardot_js_fetch_is_chunked__sig: 'ii',
	scardot_js_fetch_is_chunked: function (p_id) {
		const obj = IDHandler.get(p_id);
		if (!obj || !obj.response) {
			return -1;
		}
		return obj.chunked ? 1 : 0;
	},

	scardot_js_fetch_free__proxy: 'sync',
	scardot_js_fetch_free__sig: 'vi',
	scardot_js_fetch_free: function (id) {
		scardotFetch.free(id);
	},
};

autoAddDeps(scardotFetch, '$scardotFetch');
mergeInto(LibraryManager.library, scardotFetch);
