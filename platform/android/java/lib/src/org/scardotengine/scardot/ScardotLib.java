/**************************************************************************/
/*  scardotLib.java                                                         */
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

package org.scardotengine.scardot;

import org.scardotengine.scardot.gl.scardotRenderer;
import org.scardotengine.scardot.io.directory.DirectoryAccessHandler;
import org.scardotengine.scardot.io.file.FileAccessHandler;
import org.scardotengine.scardot.tts.scardotTTS;
import org.scardotengine.scardot.utils.scardotNetUtils;

import android.app.Activity;
import android.content.res.AssetManager;
import android.hardware.SensorEvent;
import android.view.Surface;

import javax.microedition.khronos.opengles.GL10;

/**
 * Wrapper for native library
 */
public class scardotLib {
	static {
		System.loadLibrary("scardot_android");
	}

	/**
	 * Invoked on the main thread to initialize scardot native layer.
	 */
	public static native boolean initialize(Activity activity,
			scardot p_instance,
			AssetManager p_asset_manager,
			scardotIO scardotIO,
			scardotNetUtils netUtils,
			DirectoryAccessHandler directoryAccessHandler,
			FileAccessHandler fileAccessHandler,
			boolean use_apk_expansion);

	/**
	 * Invoked on the main thread to clean up scardot native layer.
	 * @see androidx.fragment.app.Fragment#onDestroy()
	 */
	public static native void ondestroy();

	/**
	 * Invoked on the GL thread to complete setup for the scardot native layer logic.
	 * @param p_cmdline Command line arguments used to configure scardot native layer components.
	 */
	public static native boolean setup(String[] p_cmdline, scardotTTS tts);

	/**
	 * Invoked on the GL thread when the underlying Android surface has changed size.
	 * @param p_surface
	 * @param p_width
	 * @param p_height
	 * @see org.scardotengine.scardot.gl.GLSurfaceView.Renderer#onSurfaceChanged(GL10, int, int)
	 */
	public static native void resize(Surface p_surface, int p_width, int p_height);

	/**
	 * Invoked on the render thread when the underlying Android surface is created or recreated.
	 * @param p_surface
	 */
	public static native void newcontext(Surface p_surface);

	/**
	 * Forward {@link Activity#onBackPressed()} event.
	 */
	public static native void back();

	/**
	 * Invoked on the GL thread to draw the current frame.
	 * @see org.scardotengine.scardot.gl.GLSurfaceView.Renderer#onDrawFrame(GL10)
	 */
	public static native boolean step();

	/**
	 * TTS callback.
	 */
	public static native void ttsCallback(int event, int id, int pos);

	/**
	 * Forward touch events.
	 */
	public static native void dispatchTouchEvent(int event, int pointer, int pointerCount, float[] positions, boolean doubleTap);

	/**
	 * Dispatch mouse events
	 */
	public static native void dispatchMouseEvent(int event, int buttonMask, float x, float y, float deltaX, float deltaY, boolean doubleClick, boolean sourceMouseRelative, float pressure, float tiltX, float tiltY);

	public static native void magnify(float x, float y, float factor);

	public static native void pan(float x, float y, float deltaX, float deltaY);

	/**
	 * Forward accelerometer sensor events.
	 * @see android.hardware.SensorEventListener#onSensorChanged(SensorEvent)
	 */
	public static native void accelerometer(float x, float y, float z);

	/**
	 * Forward gravity sensor events.
	 * @see android.hardware.SensorEventListener#onSensorChanged(SensorEvent)
	 */
	public static native void gravity(float x, float y, float z);

	/**
	 * Forward magnetometer sensor events.
	 * @see android.hardware.SensorEventListener#onSensorChanged(SensorEvent)
	 */
	public static native void magnetometer(float x, float y, float z);

	/**
	 * Forward gyroscope sensor events.
	 * @see android.hardware.SensorEventListener#onSensorChanged(SensorEvent)
	 */
	public static native void gyroscope(float x, float y, float z);

	/**
	 * Forward regular key events.
	 */
	public static native void key(int p_physical_keycode, int p_unicode, int p_key_label, boolean p_pressed, boolean p_echo);

	/**
	 * Forward game device's key events.
	 */
	public static native void joybutton(int p_device, int p_but, boolean p_pressed);

	/**
	 * Forward joystick devices axis motion events.
	 */
	public static native void joyaxis(int p_device, int p_axis, float p_value);

	/**
	 * Forward joystick devices hat motion events.
	 */
	public static native void joyhat(int p_device, int p_hat_x, int p_hat_y);

	/**
	 * Fires when a joystick device is added or removed.
	 */
	public static native void joyconnectionchanged(int p_device, boolean p_connected, String p_name);

	/**
	 * Invoked when the Android app resumes.
	 * @see androidx.fragment.app.Fragment#onResume()
	 */
	public static native void focusin();

	/**
	 * Invoked when the Android app pauses.
	 * @see androidx.fragment.app.Fragment#onPause()
	 */
	public static native void focusout();

	/**
	 * Used to access scardot global properties.
	 * @param p_key Property key
	 * @return String value of the property
	 */
	public static native String getGlobal(String p_key);

	/**
	 * Used to access scardot's editor settings.
	 * @param settingKey Setting key
	 * @return String value of the setting
	 */
	public static native String getEditorSetting(String settingKey);

	/**
	 * Invoke method |p_method| on the scardot object specified by |p_id|
	 * @param p_id Id of the scardot object to invoke
	 * @param p_method Name of the method to invoke
	 * @param p_params Parameters to use for method invocation
	 */
	public static native void callobject(long p_id, String p_method, Object[] p_params);

	/**
	 * Invoke method |p_method| on the scardot object specified by |p_id| during idle time.
	 * @param p_id Id of the scardot object to invoke
	 * @param p_method Name of the method to invoke
	 * @param p_params Parameters to use for method invocation
	 */
	public static native void calldeferred(long p_id, String p_method, Object[] p_params);

	/**
	 * Forward the results from a permission request.
	 * @see Activity#onRequestPermissionsResult(int, String[], int[])
	 * @param p_permission Request permission
	 * @param p_result True if the permission was granted, false otherwise
	 */
	public static native void requestPermissionResult(String p_permission, boolean p_result);

	/**
	 * Invoked on the theme light/dark mode change.
	 */
	public static native void onNightModeChanged();

	/**
	 * Invoked on the GL thread to configure the height of the virtual keyboard.
	 */
	public static native void setVirtualKeyboardHeight(int p_height);

	/**
	 * Invoked on the GL thread when the {@link scardotRenderer} has been resumed.
	 * @see scardotRenderer#onActivityResumed()
	 */
	public static native void onRendererResumed();

	/**
	 * Invoked on the GL thread when the {@link scardotRenderer} has been paused.
	 * @see scardotRenderer#onActivityPaused()
	 */
	public static native void onRendererPaused();

	/**
	 * @return true if input must be dispatched from the render thread. If false, input is
	 * dispatched from the UI thread.
	 */
	public static native boolean shouldDispatchInputToRenderThread();

	/**
	 * @return the project resource directory
	 */
	public static native String getProjectResourceDir();
}
