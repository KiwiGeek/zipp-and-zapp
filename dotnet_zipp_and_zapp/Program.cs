using System;
using System.Runtime.InteropServices;

namespace dotnet_zipp_and_zapp
{

	class Program
	{
		private const int GAME_RES_WIDTH = 384;
		private const int GAME_RES_HEIGHT = 240;
		private const int GAME_RES_BPP = 4;

		[Flags]
		private enum MfbWindowFlags
		{
			// ReSharper disable UnusedMember.Local
			WF_RESIZABLE = 0x01,
			WF_FULLSCREEN = 0x02,
			WF_FULLSCREEN_DESKTOP = 0x04,
			WF_BORDERLESS = 0x08,
			WD_ALWAYS_ON_TOP = 0x16
			// ReSharper restore UnusedMember.Local
		}

		[DllImport(@"minifb.dll", BestFitMapping = false, ThrowOnUnmappableChar = true)]
		private static extern IntPtr mfb_open_ex(string title, uint width, uint height, MfbWindowFlags flags);

		[DllImport(@"minifb.dll")]
		private static extern int mfb_update_ex(IntPtr window, IntPtr buffer, uint width, uint height);

		[DllImport(@"minifb.dll")]
		private static extern bool mfb_wait_sync(IntPtr window);

		static readonly IntPtr BackBuffer = Marshal.AllocHGlobal(GAME_RES_WIDTH * GAME_RES_HEIGHT * GAME_RES_BPP);
		private static readonly Random Rnd = new();
		
		static void Main()
		{
			//Console.WriteLine("Hello World!");
			IntPtr windowHandle = mfb_open_ex("The Adventures of Zipp and Zapp", 800, 600, MfbWindowFlags.WF_RESIZABLE);
			if (windowHandle == (IntPtr)0)
			{
				return;
			}

			do
			{
				for (int y = 0; y < 240; y++)
				{
					DrawHorizontalLine(0, 384, y, (byte)y, (byte)(240 - y), 0);
				}

				for (int y = 0; y < GAME_RES_HEIGHT; y++)
				{
					for (int x = 0; x < GAME_RES_WIDTH; x++)
					{
						DrawPixel(x, y, (byte)Rnd.Next(0,255), (byte)Rnd.Next(0,255), (byte)Rnd.Next(0,255));
					}
				}

				int state = mfb_update_ex(windowHandle, BackBuffer, 384, 240);
				

				if (state < 0) {
					break;
				}
			} while (mfb_wait_sync(windowHandle));


		}

		static void DrawHorizontalLine(int startX, int endX, int y, byte red, byte green, byte blue)
		{
			int startingPixel = (y * GAME_RES_WIDTH + startX) * GAME_RES_BPP;
			for (int x = startX; x < endX; x++)
			{
				Marshal.WriteByte(BackBuffer + startingPixel + x * GAME_RES_BPP, blue);     // blue
				Marshal.WriteByte(BackBuffer + startingPixel + x * GAME_RES_BPP + 1, green);    // green
				Marshal.WriteByte(BackBuffer + startingPixel + x * GAME_RES_BPP + 2, red);  // red
			}
		}

		static void DrawPixel(int x, int y, byte red, byte green, byte blue)
		{
			int startingOffset = (y * GAME_RES_WIDTH + x) * GAME_RES_BPP;
			Marshal.WriteByte(BackBuffer + startingOffset , blue);							// blue
			Marshal.WriteByte(BackBuffer + startingOffset + 1, green);						// green
			Marshal.WriteByte(BackBuffer + startingOffset + 2, red);							// red
		}
	}
}
