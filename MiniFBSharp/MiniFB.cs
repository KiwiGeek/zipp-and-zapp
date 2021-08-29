using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using MiniFBSharp;
using MiniFBSharp.Enums;

// ReSharper disable once CheckNamespace
public static class MiniFB
{

	#region Create a Window

	[DllImport(@"minifb.dll", BestFitMapping = false, ThrowOnUnmappableChar = true)] private static extern IntPtr mfb_open_ex(string title, uint width, uint height, MfbWindowFlags flags);
	public static MiniFBWindow OpenWindow(string title, Size dimensions, WindowFlags flags)
	{
		// todo: validate arguments
		IntPtr handle = mfb_open_ex(title, (uint)dimensions.Width, (uint)dimensions.Height, (MfbWindowFlags)flags);
		MiniFBWindow result = new MiniFBWindow(handle);

		return result;
	}

	[DllImport(@"minifb.dll", BestFitMapping = false, ThrowOnUnmappableChar = true)] private static extern IntPtr mfb_open(string title, uint width, uint height);
	public static MiniFBWindow OpenWindow(string title, Size dimensions)
	{
		// todo: validate arguments
		IntPtr handle = mfb_open(title, (uint)dimensions.Width, (uint)dimensions.Height);
		MiniFBWindow result = new MiniFBWindow(handle);

		return result;
	}

	#endregion

	#region Update the display

	[DllImport(@"minifb.dll")] private static extern int mfb_update(IntPtr window, IntPtr buffer);
	public static int Update(MiniFBWindow window, Bitmap image)
	{
		BitmapData bitmapData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadOnly,
			PixelFormat.Format32bppRgb);

		int result = mfb_update(window.Handle, bitmapData.Scan0);

		image.UnlockBits(bitmapData);
		return result;
	}

	[DllImport(@"minifb.dll")] private static extern int mfb_update_ex(IntPtr window, IntPtr buffer, uint width, uint height);
	public static int Update(MiniFBWindow window, Bitmap image, Size dimensions)
	{
		BitmapData bitmapData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height),
				ImageLockMode.ReadOnly,
				PixelFormat.Format32bppRgb);

		int result = mfb_update_ex(window.Handle, bitmapData.Scan0, (uint)image.Width, (uint)image.Height);

		image.UnlockBits(bitmapData);
		return result;
	}

	#endregion

	#region Only update window events (unfinished)

	//	// Only updates the window events
	//	__declspec(dllexport) mfb_update_state mfb_update_events(struct mfb_window *window);

	#endregion

	#region Close the window

	[DllImport(@"minifb.dll")] private static extern void mfb_close(IntPtr window);
	public static void CloseWindow(MiniFBWindow window)
	{
		mfb_close(window.Handle);
	}

	#endregion

	#region Set User Data (unfinished)
	//// Set user data
	//__declspec(dllexport) void mfb_set_user_data(struct mfb_window *window, void* user_data);
	//	__declspec(dllexport) void* mfb_get_user_data(struct mfb_window *window);

	#endregion

	#region Set viewport (unfinished)
	//// Set viewport (useful when resize)
	//__declspec(dllexport) bool mfb_set_viewport(struct mfb_window *window, unsigned offset_x, unsigned offset_y, unsigned width, unsigned height);

	#endregion

	#region DPI

	[DllImport(@"minifb.dll")] private static extern unsafe void mfb_get_monitor_scale(IntPtr window, float* scale_x, float* scale_y);
	public static unsafe (float ScaleX, float ScaleY) GetMonitorScale(MiniFBWindow windowHandle)
	{
		float scaleX;
		float scaleY;
		mfb_get_monitor_scale(windowHandle.Handle, &scaleX, &scaleY);
		return (scaleX, scaleY);
	}

	#endregion

	#region Callbacks (unfinished)
	//	// Callbacks
	//	__declspec(dllexport) void mfb_set_active_callback(struct mfb_window *window, mfb_active_func callback);
	//	__declspec(dllexport) void mfb_set_resize_callback(struct mfb_window *window, mfb_resize_func callback);
	//	__declspec(dllexport) void mfb_set_keyboard_callback(struct mfb_window *window, mfb_keyboard_func callback);
	//	__declspec(dllexport) void mfb_set_char_input_callback(struct mfb_window *window, mfb_char_input_func callback);
	//	__declspec(dllexport) void mfb_set_mouse_button_callback(struct mfb_window *window, mfb_mouse_button_func callback);
	//	__declspec(dllexport) void mfb_set_mouse_move_callback(struct mfb_window *window, mfb_mouse_move_func callback);
	//	__declspec(dllexport) void mfb_set_mouse_scroll_callback(struct mfb_window *window, mfb_mouse_scroll_func callback);


	[DllImport("minifb.dll", CallingConvention = CallingConvention.Cdecl)] private static extern void mfb_set_resize_callback(IntPtr window, ResizeCallbackDelegate callback);

	//public delegate void ActiveChanged<bool>

	private delegate void ResizeCallbackDelegate(IntPtr window, int width, int height);


	#endregion

	#region Frames Per Second

	[DllImport(@"minifb.dll")] private static extern bool mfb_wait_sync(IntPtr window);

	public static bool WaitSync(MiniFBWindow window)
	{
		return mfb_wait_sync(window.Handle);
	}

	[DllImport(@"minifb.dll")] private static extern void mfb_set_target_fps(int fps);

	public static void SetTargetFPS(int fps)
	{
		mfb_set_target_fps(fps);
	}

	[DllImport(@"minifb.dll")] private static extern uint mfb_get_target_fps();

	public static uint GetTargetFPS()
	{
		return mfb_get_target_fps();
	}


	#endregion

	#region Getters (unfinished)
	//// Getters
	//__declspec(dllexport) const char *        mfb_get_key_name(mfb_key key);
	//__declspec(dllexport) bool                mfb_is_window_active(struct mfb_window *window);
	//__declspec(dllexport) unsigned            mfb_get_window_width(struct mfb_window *window);
	//__declspec(dllexport) unsigned            mfb_get_window_height(struct mfb_window *window);
	//__declspec(dllexport) int                 mfb_get_mouse_x(struct mfb_window *window);             // Last mouse pos X
	//__declspec(dllexport) int                 mfb_get_mouse_y(struct mfb_window *window);             // Last mouse pos Y
	//__declspec(dllexport) float               mfb_get_mouse_scroll_x(struct mfb_window *window);      // Mouse wheel X as a sum. When you call this function it resets.
	//__declspec(dllexport) float               mfb_get_mouse_scroll_y(struct mfb_window *window);      // Mouse wheel Y as a sum. When you call this function it resets.
	//__declspec(dllexport) const uint8_t *     mfb_get_mouse_button_buffer(struct mfb_window *window); // One byte for every button. Press (1), Release 0. (up to 8 buttons)
	//__declspec(dllexport) const uint8_t *     mfb_get_key_buffer(struct mfb_window *window);          // One byte for every key. Press (1), Release 0.
	#endregion

	#region Timer (unfinished)

	//// Timer
	//__declspec(dllexport) struct mfb_timer *  mfb_timer_create(void);
	//__declspec(dllexport) void                mfb_timer_destroy(struct mfb_timer *tmr);
	//__declspec(dllexport) void                mfb_timer_reset(struct mfb_timer *tmr);
	//__declspec(dllexport) double              mfb_timer_now(struct mfb_timer *tmr);
	//__declspec(dllexport) double              mfb_timer_delta(struct mfb_timer *tmr);
	//__declspec(dllexport) double              mfb_timer_get_frequency(void);
	//__declspec(dllexport) double              mfb_timer_get_resolution(void);

	#endregion

}
