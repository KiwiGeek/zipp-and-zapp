using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
internal static class MiniFB
{


	#region .NET specific methods

	public static ulong CreateBackBuffer(uint width, uint height)
	{
		return (ulong)Marshal.AllocHGlobal((int)(width * height * 4));		// 32bit pixels
	}

	#endregion


	#region Create a Window

	[Flags] private enum MfbWindowFlags
	{
		// ReSharper disable UnusedMember.Local
		WF_RESIZABLE = 0x01,
		WF_FULLSCREEN = 0x02,
		WF_FULLSCREEN_DESKTOP = 0x04,
		WF_BORDERLESS = 0x08,
		WD_ALWAYS_ON_TOP = 0x16
		// ReSharper restore UnusedMember.Local
	}

	[Flags] public enum WindowFlags
	{
		Resizable = 1,
		Fullscreen = 2,
		FullscreenDesktop = 4,
		Borderless = 8,
		AlwaysOnTop = 16
	}

	[DllImport(@"minifb.dll", BestFitMapping = false, ThrowOnUnmappableChar = true)] private static extern IntPtr mfb_open_ex(string title, uint width, uint height, MfbWindowFlags flags);

	public static ulong OpenEx(string title, uint width, uint height, WindowFlags flags)
	{
		return (ulong)mfb_open_ex(title, width, height, (MfbWindowFlags)flags);
	}

	[DllImport(@"minifb.dll", BestFitMapping = false, ThrowOnUnmappableChar = true)] private static extern IntPtr mfb_open(string title, uint width, uint height);

	public static ulong Open(string title, uint width, uint height)
	{
		return (ulong)mfb_open(title, width, height);
	}

	#endregion

	#region Update the display

	//	// Update the display
	//	// Input buffer is assumed to be a 32-bit buffer of the size given in the open call
	//	// Will return a negative status if something went wrong or the user want to exit
	//	// Also updates the window events

	[DllImport(@"minifb.dll")] private static extern int mfb_update(IntPtr window, IntPtr buffer);

	public static int Update(ulong windowHandle, ulong bufferHandle)
	{
		return mfb_update((IntPtr)windowHandle, (IntPtr)bufferHandle);
	}
	public static int Update(ulong windowHandle, IntPtr bufferHandle)
	{
		return mfb_update((IntPtr)windowHandle, bufferHandle);
	}
	public static int Update(IntPtr windowHandle, IntPtr bufferHandle)
	{
		return mfb_update(windowHandle, bufferHandle);
	}
	public static int Update(IntPtr windowHandle, ulong bufferHandle)
	{
		return mfb_update(windowHandle, (IntPtr)bufferHandle);
	}


	[DllImport(@"minifb.dll")] private static extern int mfb_update_ex(IntPtr window, IntPtr buffer, uint width, uint height);

	public static int UpdateEx(ulong windowHandle, ref byte[] buffer, uint width, uint height)
	{
		IntPtr nativeBuffer = Marshal.AllocHGlobal(buffer.Length);
		Marshal.Copy(buffer, 0, nativeBuffer, buffer.Length);
		int result =  mfb_update_ex((IntPtr)windowHandle, nativeBuffer, width, height);
		Marshal.FreeHGlobal(nativeBuffer);
		return result;
	}
	#endregion

	#region Only update window events (unfinished)

	//	// Only updates the window events
	//	__declspec(dllexport) mfb_update_state mfb_update_events(struct mfb_window *window);

	#endregion

	#region Close the window (unfinished)
	//// Close the window
	//__declspec(dllexport) void mfb_close(struct mfb_window *window);

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

	#region DPI (unfinished)
	//// DPI
	//// [Deprecated]: Probably a better name will be mfb_get_monitor_scale
	//__declspec(dllexport) void mfb_get_monitor_dpi(struct mfb_window *window, float* dpi_x, float* dpi_y);
	//	// Use this instead
	//	__declspec(dllexport) void mfb_get_monitor_scale(struct mfb_window *window, float* scale_x, float* scale_y);
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

	[DllImport(@"minifb.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern void mfb_set_active_callback(IntPtr window, ActiveCallbackDelegate callback);

	[DllImport("minifb.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern void mfb_set_resize_callback(IntPtr window, ResizeCallbackDelegate callback);

	public delegate void ActiveCallbackDelegate(IntPtr window, bool isActive);

	public delegate void ResizeCallbackDelegate(IntPtr window, int width, int height);
	#endregion

	#region Frames Per Second

	[DllImport(@"minifb.dll")] private static extern bool mfb_wait_sync(IntPtr window);

	public static bool WaitSync(ulong windowHandle)
	{
		return mfb_wait_sync((IntPtr)windowHandle);
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
