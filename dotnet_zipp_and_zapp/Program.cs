#define HORZ_LINES
#define COLORED_DOTS

using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using MiniFBSharp;
using MiniFBSharp.Enums;

namespace dotnet_zipp_and_zapp
{

	class Program
	{

		private const int GAME_RES_WIDTH = 384;
		private const int GAME_RES_HEIGHT = 240;
		private const int GAME_RES_BPP = 4;

		private static readonly Random Rnd = new();

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

			Bitmap image = new Bitmap(GAME_RES_WIDTH, GAME_RES_HEIGHT);
			Graphics g = Graphics.FromImage(image);
			//g.FillRectangle(new SolidBrush(Color.Wheat), 10, 10, 100, 100);


			//	MiniFB.ActiveCallbackDelegate act = new(Active);
			//	MiniFB.ResizeCallbackDelegate resize = new(Resize);

			MiniFBWindow gameWindow = MiniFB.OpenWindow("The Adventures of Zipp and Zapp", new Size(800, 600), WindowFlags.Resizable);
			MiniFB.SetTargetFPS(200);
			Debug.WriteLine(MiniFB.GetTargetFPS());

			gameWindow.ActiveChanged += GameWindow_ActiveChanged;

			(float scaleX, float scaleY) = MiniFB.GetMonitorScale(gameWindow);

			//MiniFB.mfb_set_active_callback((IntPtr)windowHandle, act);
			//MiniFB.mfb_set_resize_callback((IntPtr)windowHandle, resize);

			do
			{
#if HORZ_LINES
				for (int y = 0; y < 240; y++)
				{
					g.DrawLine(new Pen(Color.FromArgb(y, 240 - y, 0)), new Point(0, y), new Point(GAME_RES_WIDTH, y));
				}
#endif

#if COLORED_DOTS

				BitmapData bd = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.WriteOnly,
					PixelFormat.Format32bppArgb);

				for (int y = 0; y < GAME_RES_HEIGHT; y++)
				{
					for (int x = 0; x < GAME_RES_WIDTH; x++)
					{

						PlotPixel(bd, x, y, Color.FromArgb(Rnd.Next(0, 255), Rnd.Next(0, 255), Rnd.Next(0, 255)));

						unsafe
						{
							byte* startingMemoryOffset = (byte*)bd.Scan0;
							int rowOffset = bd.Stride * y;
							startingMemoryOffset[rowOffset + (x * 4) + 0] = (byte)Rnd.Next(0, 255);			// blue
							startingMemoryOffset[rowOffset + (x * 4) + 1] = (byte)Rnd.Next(0, 255);			// green
							startingMemoryOffset[rowOffset + (x * 4) + 2] = (byte)Rnd.Next(0, 255);			// red
						}
					}
				}

				image.UnlockBits(bd);
#endif


				int state = MiniFB.Update(gameWindow, image, new Size(GAME_RES_WIDTH, GAME_RES_HEIGHT));

				if (state < 0)
				{
					break;
				}
			} while (MiniFB.WaitSync(gameWindow));


		}

		private static void GameWindow_ActiveChanged(object sender, bool e)
		{
			Debug.Print(e.ToString());
		}

		private static void PlotPixel(BitmapData bd, int x, int y, Color c)
		{
			
		}
	}
}
