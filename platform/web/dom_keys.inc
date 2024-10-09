/**************************************************************************/
/*  dom_keys.inc                                                          */
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

#include "core/os/keyboard.h"

// See https://w3c.github.io/uievents-code/#code-value-tables
Key dom_code2godot_scancode(EM_UTF8 const p_code[32], EM_UTF8 const p_key[32], bool p_physical) {
#define DOM2SCARDOT(p_str, p_godot_code)                                                                \
	if (memcmp((const void *)p_str, (void *)(p_physical ? p_code : p_key), strlen(p_str) + 1) == 0) { \
		return Key::p_godot_code;                                                                     \
	}

	// Numpad section.
	DOM2SCARDOT("NumLock", NUMLOCK);
	DOM2SCARDOT("Numpad0", KP_0);
	DOM2SCARDOT("Numpad1", KP_1);
	DOM2SCARDOT("Numpad2", KP_2);
	DOM2SCARDOT("Numpad3", KP_3);
	DOM2SCARDOT("Numpad4", KP_4);
	DOM2SCARDOT("Numpad5", KP_5);
	DOM2SCARDOT("Numpad6", KP_6);
	DOM2SCARDOT("Numpad7", KP_7);
	DOM2SCARDOT("Numpad8", KP_8);
	DOM2SCARDOT("Numpad9", KP_9);
	DOM2SCARDOT("NumpadAdd", KP_ADD);
	DOM2SCARDOT("NumpadBackspace", BACKSPACE);
	DOM2SCARDOT("Clear", CLEAR); // NumLock on macOS.
	DOM2SCARDOT("NumpadClear", CLEAR);
	DOM2SCARDOT("NumpadClearEntry", CLEAR);
	//DOM2SCARDOT("NumpadComma", UNKNOWN);
	DOM2SCARDOT("NumpadDecimal", KP_PERIOD);
	DOM2SCARDOT("NumpadDivide", KP_DIVIDE);
	DOM2SCARDOT("NumpadEnter", KP_ENTER);
	DOM2SCARDOT("NumpadEqual", EQUAL);
	//DOM2SCARDOT("NumpadHash", UNKNOWN);
	//DOM2SCARDOT("NumpadMemoryAdd", UNKNOWN);
	//DOM2SCARDOT("NumpadMemoryClear", UNKNOWN);
	//DOM2SCARDOT("NumpadMemoryRecall", UNKNOWN);
	//DOM2SCARDOT("NumpadMemoryStore", UNKNOWN);
	//DOM2SCARDOT("NumpadMemorySubtract", UNKNOWN);
	DOM2SCARDOT("NumpadMultiply", KP_MULTIPLY);
	DOM2SCARDOT("NumpadParenLeft", PARENLEFT);
	DOM2SCARDOT("NumpadParenRight", PARENRIGHT);
	DOM2SCARDOT("NumpadStar", KP_MULTIPLY); // or ASTERISK ?
	DOM2SCARDOT("NumpadSubtract", KP_SUBTRACT);

	// Alphanumeric section.
	DOM2SCARDOT("Backquote", QUOTELEFT);
	DOM2SCARDOT("Backslash", BACKSLASH);
	DOM2SCARDOT("BracketLeft", BRACKETLEFT);
	DOM2SCARDOT("BracketRight", BRACKETRIGHT);
	DOM2SCARDOT("Comma", COMMA);
	DOM2SCARDOT("Digit0", KEY_0);
	DOM2SCARDOT("Digit1", KEY_1);
	DOM2SCARDOT("Digit2", KEY_2);
	DOM2SCARDOT("Digit3", KEY_3);
	DOM2SCARDOT("Digit4", KEY_4);
	DOM2SCARDOT("Digit5", KEY_5);
	DOM2SCARDOT("Digit6", KEY_6);
	DOM2SCARDOT("Digit7", KEY_7);
	DOM2SCARDOT("Digit8", KEY_8);
	DOM2SCARDOT("Digit9", KEY_9);
	DOM2SCARDOT("Equal", EQUAL);
	DOM2SCARDOT("IntlBackslash", BACKSLASH);
	//DOM2SCARDOT("IntlRo", UNKNOWN);
	DOM2SCARDOT("IntlYen", YEN);

	DOM2SCARDOT("KeyA", A);
	DOM2SCARDOT("KeyB", B);
	DOM2SCARDOT("KeyC", C);
	DOM2SCARDOT("KeyD", D);
	DOM2SCARDOT("KeyE", E);
	DOM2SCARDOT("KeyF", F);
	DOM2SCARDOT("KeyG", G);
	DOM2SCARDOT("KeyH", H);
	DOM2SCARDOT("KeyI", I);
	DOM2SCARDOT("KeyJ", J);
	DOM2SCARDOT("KeyK", K);
	DOM2SCARDOT("KeyL", L);
	DOM2SCARDOT("KeyM", M);
	DOM2SCARDOT("KeyN", N);
	DOM2SCARDOT("KeyO", O);
	DOM2SCARDOT("KeyP", P);
	DOM2SCARDOT("KeyQ", Q);
	DOM2SCARDOT("KeyR", R);
	DOM2SCARDOT("KeyS", S);
	DOM2SCARDOT("KeyT", T);
	DOM2SCARDOT("KeyU", U);
	DOM2SCARDOT("KeyV", V);
	DOM2SCARDOT("KeyW", W);
	DOM2SCARDOT("KeyX", X);
	DOM2SCARDOT("KeyY", Y);
	DOM2SCARDOT("KeyZ", Z);

	DOM2SCARDOT("Minus", MINUS);
	DOM2SCARDOT("Period", PERIOD);
	DOM2SCARDOT("Quote", APOSTROPHE);
	DOM2SCARDOT("Semicolon", SEMICOLON);
	DOM2SCARDOT("Slash", SLASH);

	// Functional keys in the Alphanumeric section.
	DOM2SCARDOT("Alt", ALT);
	DOM2SCARDOT("AltLeft", ALT);
	DOM2SCARDOT("AltRight", ALT);
	DOM2SCARDOT("Backspace", BACKSPACE);
	DOM2SCARDOT("CapsLock", CAPSLOCK);
	DOM2SCARDOT("ContextMenu", MENU);
	DOM2SCARDOT("Control", CTRL);
	DOM2SCARDOT("ControlLeft", CTRL);
	DOM2SCARDOT("ControlRight", CTRL);
	DOM2SCARDOT("Enter", ENTER);
	DOM2SCARDOT("Meta", META);
	DOM2SCARDOT("MetaLeft", META);
	DOM2SCARDOT("MetaRight", META);
	DOM2SCARDOT("OSLeft", META); // Command on macOS.
	DOM2SCARDOT("OSRight", META); // Command on macOS.
	DOM2SCARDOT("Shift", SHIFT);
	DOM2SCARDOT("ShiftLeft", SHIFT);
	DOM2SCARDOT("ShiftRight", SHIFT);
	DOM2SCARDOT("Space", SPACE);
	DOM2SCARDOT("Tab", TAB);

	// ControlPad section.
	DOM2SCARDOT("Delete", KEY_DELETE);
	DOM2SCARDOT("End", END);
	DOM2SCARDOT("Help", HELP);
	DOM2SCARDOT("Home", HOME);
	DOM2SCARDOT("Insert", INSERT);
	DOM2SCARDOT("PageDown", PAGEDOWN);
	DOM2SCARDOT("PageUp", PAGEUP);

	// ArrowPad section.
	DOM2SCARDOT("ArrowDown", DOWN);
	DOM2SCARDOT("ArrowLeft", LEFT);
	DOM2SCARDOT("ArrowRight", RIGHT);
	DOM2SCARDOT("ArrowUp", UP);

	// Function section.
	DOM2SCARDOT("Escape", ESCAPE);
	DOM2SCARDOT("F1", F1);
	DOM2SCARDOT("F2", F2);
	DOM2SCARDOT("F3", F3);
	DOM2SCARDOT("F4", F4);
	DOM2SCARDOT("F5", F5);
	DOM2SCARDOT("F6", F6);
	DOM2SCARDOT("F7", F7);
	DOM2SCARDOT("F8", F8);
	DOM2SCARDOT("F9", F9);
	DOM2SCARDOT("F10", F10);
	DOM2SCARDOT("F11", F11);
	DOM2SCARDOT("F12", F12);
	//DOM2SCARDOT("Fn", UNKNOWN); // never actually fired, but included in the standard draft.
	//DOM2SCARDOT("FnLock", UNKNOWN);
	DOM2SCARDOT("PrintScreen", PRINT);
	DOM2SCARDOT("ScrollLock", SCROLLLOCK);
	DOM2SCARDOT("Pause", PAUSE);

	// Media keys section.
	DOM2SCARDOT("BrowserBack", BACK);
	DOM2SCARDOT("BrowserFavorites", FAVORITES);
	DOM2SCARDOT("BrowserForward", FORWARD);
	DOM2SCARDOT("BrowserHome", OPENURL);
	DOM2SCARDOT("BrowserRefresh", REFRESH);
	DOM2SCARDOT("BrowserSearch", SEARCH);
	DOM2SCARDOT("BrowserStop", STOP);
	//DOM2SCARDOT("Eject", UNKNOWN);
	DOM2SCARDOT("LaunchApp1", LAUNCH0);
	DOM2SCARDOT("LaunchApp2", LAUNCH1);
	DOM2SCARDOT("LaunchMail", LAUNCHMAIL);
	DOM2SCARDOT("MediaPlayPause", MEDIAPLAY);
	DOM2SCARDOT("MediaSelect", LAUNCHMEDIA);
	DOM2SCARDOT("MediaStop", MEDIASTOP);
	DOM2SCARDOT("MediaTrackNext", MEDIANEXT);
	DOM2SCARDOT("MediaTrackPrevious", MEDIAPREVIOUS);
	//DOM2SCARDOT("Power", UNKNOWN);
	//DOM2SCARDOT("Sleep", UNKNOWN);
	DOM2SCARDOT("AudioVolumeDown", VOLUMEDOWN);
	DOM2SCARDOT("AudioVolumeMute", VOLUMEMUTE);
	DOM2SCARDOT("AudioVolumeUp", VOLUMEUP);
	//DOM2SCARDOT("WakeUp", UNKNOWN);

	// Printable ASCII.
	uint8_t b0 = (uint8_t)p_key[0];
	uint8_t b1 = (uint8_t)p_key[1];
	if (b0 >= 0x20 && b0 < 0x7F) { // ASCII.
		if (b0 > 0x60 && b0 < 0x7B) { // Lowercase ASCII.
			b0 -= 32;
		}
		return (Key)b0;
	} else if (b0 == 0xC2 && b1 == 0xA5) {
		return Key::YEN;
	} else if (b0 == 0xC2 && b1 == 0xA7) {
		return Key::SECTION;
	}

	return Key::NONE;
#undef DOM2SCARDOT
}

KeyLocation dom_code2godot_key_location(EM_UTF8 const p_code[32]) {
#define DOM2SCARDOT(m_str, m_godot_code)                                         \
	if (memcmp((const void *)m_str, (void *)p_code, strlen(m_str) + 1) == 0) { \
		return KeyLocation::m_godot_code;                                      \
	}

	DOM2SCARDOT("AltLeft", LEFT);
	DOM2SCARDOT("AltRight", RIGHT);
	DOM2SCARDOT("ControlLeft", LEFT);
	DOM2SCARDOT("ControlRight", RIGHT);
	DOM2SCARDOT("MetaLeft", LEFT);
	DOM2SCARDOT("MetaRight", RIGHT);
	DOM2SCARDOT("OSLeft", LEFT);
	DOM2SCARDOT("OSRight", RIGHT);
	DOM2SCARDOT("ShiftLeft", LEFT);
	DOM2SCARDOT("ShiftRight", RIGHT);

	return KeyLocation::UNSPECIFIED;
#undef DOM2SCARDOT
}
