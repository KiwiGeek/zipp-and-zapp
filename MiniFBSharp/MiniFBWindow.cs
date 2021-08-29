
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace MiniFBSharp
{
	public class MiniFBWindow
	{

		[DllImport(@"minifb.dll", CallingConvention = CallingConvention.Cdecl)] 
		private static extern void mfb_set_active_callback(IntPtr window, ActiveCallbackDelegate callback);

		public delegate void ActiveCallbackDelegate(IntPtr window, bool isActive);

		//public static event EventHandler<bool> ActiveChanged;

		public static void Active(IntPtr window, bool isActive)
		{
			Debug.Write(window.ToString());
			Debug.WriteLine(isActive.ToString());
			//ActiveChanged?.Invoke(window, isActive);
		}

		public MiniFBWindow(IntPtr handle)
		{
			
			Handle = handle;

			// this is broken
			//ActiveCallbackDelegate act = new(Active);
			//mfb_set_active_callback(handle, act);
		}

		public IntPtr Handle { get; init; }

	}
}
