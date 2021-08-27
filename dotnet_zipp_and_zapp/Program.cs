using System;
using System.Diagnostics;
using System.Runtime.InteropServices;


namespace dotnet_zipp_and_zapp
{

	class Program
	{

		private const int GAME_RES_WIDTH = 384;
		private const int GAME_RES_HEIGHT = 240;
		private const int GAME_RES_BPP = 4;
		
		static ulong _backBuffer;
		private static readonly Random Rnd = new();
		private static byte[] buffer;

		public static void Active(IntPtr window, bool isActive)
		{
			Debug.Write(window.ToString());
			Debug.WriteLine(isActive.ToString());
		}

		public static void Resize(IntPtr window, int width, int height)
		{
			Debug.WriteLine($"Width: {width}   Height: {height}");
		}

		static void Main()
		{

			_backBuffer = MiniFB.CreateBackBuffer(GAME_RES_WIDTH, GAME_RES_HEIGHT);
			buffer = new byte[GAME_RES_BPP * GAME_RES_HEIGHT * GAME_RES_WIDTH];

			MiniFB.ActiveCallbackDelegate act = new(Active);
			MiniFB.ResizeCallbackDelegate resize = new(Resize);

			ulong windowHandle = MiniFB.Open("The Adventures of Zipp and Zapp", 1920, 1080);
			MiniFB.SetTargetFPS(200);
			Debug.WriteLine(MiniFB.GetTargetFPS());

			MiniFB.mfb_set_active_callback((IntPtr)windowHandle, act);
			MiniFB.mfb_set_resize_callback((IntPtr)windowHandle, resize);


			if (windowHandle == 0)
			{
				return;
			}

			do
			{
				for (int y = 0; y < 240; y++)
				{
					DrawHorizontalLine(0, 384, y, (byte)y, (byte)(240 - y), 0);
				}

			/*	for (int y = 0; y < GAME_RES_HEIGHT; y++)
				{
					for (int x = 0; x < GAME_RES_WIDTH; x++)
					{
						DrawPixel(x, y, (byte)Rnd.Next(0, 255), (byte)Rnd.Next(0, 255), (byte)Rnd.Next(0, 255));
					}
				}*/

				int state = MiniFB.UpdateEx(windowHandle, ref buffer, 384, 200);


				if (state < 0)
				{
					break;
				}
			} while (MiniFB.WaitSync(windowHandle));


		}

		static void DrawHorizontalLine(int startX, int endX, int y, byte red, byte green, byte blue)
		{
			int startingOffset = (y * GAME_RES_WIDTH + startX);
			for (int x = startX; x < endX; x++)
			{
				Span<int> pixels = MemoryMarshal.Cast<byte,int>(buffer);
				pixels[startingOffset + x] = /*(red << 16) + (green << 8)*/ + blue;
				//buffer[startingOffset + x * GAME_RES_BPP] = blue;
				//buffer[(startingOffset + x * GAME_RES_BPP) + 1] = green;
				//buffer[(startingOffset + x * GAME_RES_BPP) + 2] = red;
			}
		}

		static void DrawPixel(int x, int y, byte red, byte green, byte blue)
		{
			int startingOffset = (y * GAME_RES_WIDTH + x) * GAME_RES_BPP;
			buffer[startingOffset] = blue;
			buffer[startingOffset + 1] = green;
			buffer[startingOffset + 2] = red;
		}
	}
}
