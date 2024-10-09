/**************************************************************************/
/*  scardotActivity.kt                                                      */
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

package org.scardotengine.scardot

import android.app.Activity
import android.content.Intent
import android.content.pm.PackageManager
import android.os.Bundle
import android.util.Log
import androidx.annotation.CallSuper
import androidx.annotation.LayoutRes
import androidx.fragment.app.FragmentActivity
import org.scardotengine.scardot.utils.PermissionsUtil
import org.scardotengine.scardot.utils.ProcessPhoenix

/**
 * Base abstract activity for Android apps intending to use scardot as the primary screen.
 *
 * Also a reference implementation for how to setup and use the [scardotFragment] fragment
 * within an Android app.
 */
abstract class scardotActivity : FragmentActivity(), scardotHost {

	companion object {
		private val TAG = scardotActivity::class.java.simpleName

		@JvmStatic
		protected val EXTRA_NEW_LAUNCH = "new_launch_requested"
	}

	/**
	 * Interaction with the [scardot] object is delegated to the [scardotFragment] class.
	 */
	protected var scardotFragment: scardotFragment? = null
		private set

	override fun onCreate(savedInstanceState: Bundle?) {
		super.onCreate(savedInstanceState)
		setContentView(getscardotAppLayout())

		handleStartIntent(intent, true)

		val currentFragment = supportFragmentManager.findFragmentById(R.id.scardot_fragment_container)
		if (currentFragment is scardotFragment) {
			Log.v(TAG, "Reusing existing scardot fragment instance.")
			scardotFragment = currentFragment
		} else {
			Log.v(TAG, "Creating new scardot fragment instance.")
			scardotFragment = initscardotInstance()
			supportFragmentManager.beginTransaction().replace(R.id.scardot_fragment_container, scardotFragment!!).setPrimaryNavigationFragment(scardotFragment).commitNowAllowingStateLoss()
		}
	}

	@LayoutRes
	protected open fun getscardotAppLayout() = R.layout.scardot_app_layout

	override fun onDestroy() {
		Log.v(TAG, "Destroying scardotActivity $this...")
		super.onDestroy()
	}

	override fun onscardotForceQuit(instance: scardot) {
		runOnUiThread { terminatescardotInstance(instance) }
	}

	private fun terminatescardotInstance(instance: scardot) {
		scardotFragment?.let {
			if (instance === it.scardot) {
				Log.v(TAG, "Force quitting scardot instance")
				ProcessPhoenix.forceQuit(this)
			}
		}
	}

	override fun onscardotRestartRequested(instance: scardot) {
		runOnUiThread {
			scardotFragment?.let {
				if (instance === it.scardot) {
					// It's very hard to properly de-initialize scardot on Android to restart the game
					// from scratch. Therefore, we need to kill the whole app process and relaunch it.
					//
					// Restarting only the activity, wouldn't be enough unless it did proper cleanup (including
					// releasing and reloading native libs or resetting their state somehow and clearing static data).
					Log.v(TAG, "Restarting scardot instance...")
					ProcessPhoenix.triggerRebirth(this)
				}
			}
		}
	}

	override fun onNewIntent(newIntent: Intent) {
		super.onNewIntent(newIntent)
		intent = newIntent

		handleStartIntent(newIntent, false)

		scardotFragment?.onNewIntent(newIntent)
	}

	private fun handleStartIntent(intent: Intent, newLaunch: Boolean) {
		if (!newLaunch) {
			val newLaunchRequested = intent.getBooleanExtra(EXTRA_NEW_LAUNCH, false)
			if (newLaunchRequested) {
				Log.d(TAG, "New launch requested, restarting..")
				val restartIntent = Intent(intent).putExtra(EXTRA_NEW_LAUNCH, false)
				ProcessPhoenix.triggerRebirth(this, restartIntent)
				return
			}
		}
	}

	@CallSuper
	override fun onActivityResult(requestCode: Int, resultCode: Int, data: Intent?) {
		super.onActivityResult(requestCode, resultCode, data)
		scardotFragment?.onActivityResult(requestCode, resultCode, data)
	}

	@CallSuper
	override fun onRequestPermissionsResult(requestCode: Int, permissions: Array<String>, grantResults: IntArray) {
		super.onRequestPermissionsResult(requestCode, permissions, grantResults)
		scardotFragment?.onRequestPermissionsResult(requestCode, permissions, grantResults)

		// Logging the result of permission requests
		if (requestCode == PermissionsUtil.REQUEST_ALL_PERMISSION_REQ_CODE || requestCode == PermissionsUtil.REQUEST_SINGLE_PERMISSION_REQ_CODE) {
			Log.d(TAG, "Received permissions request result..")
			for (i in permissions.indices) {
				val permissionGranted = grantResults[i] == PackageManager.PERMISSION_GRANTED
				Log.d(TAG, "Permission ${permissions[i]} ${if (permissionGranted) { "granted"} else { "denied" }}")
			}
		}
	}

	override fun onBackPressed() {
		scardotFragment?.onBackPressed() ?: super.onBackPressed()
	}

	override fun getActivity(): Activity? {
		return this
	}

	override fun getscardot(): scardot? {
		return scardotFragment?.scardot
	}

	/**
	 * Used to initialize the scardot fragment instance in [onCreate].
	 */
	protected open fun initscardotInstance(): scardotFragment {
		return scardotFragment()
	}
}
