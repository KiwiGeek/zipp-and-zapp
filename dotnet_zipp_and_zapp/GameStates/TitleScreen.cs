using System;
using System.Diagnostics;
using System.Drawing;

namespace dotnet_zipp_and_zapp.GameStates
{
	static class TitleScreen
	{
		private static readonly Random Rnd = new();
		private static int _localFrame;
		private static readonly Font DrawFont = new("Arial", 16);
		private static readonly SolidBrush DrawBrush = new(Color.White);
		private static readonly StringFormat DrawAlignment = new() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
		private static RectangleF _drawRect;
		private static Image _logo;

		static TitleScreen()
		{
			_logo = Image.FromFile($"Assets\\TitleScreens\\TitleScreen0{(Rnd.Next(0, 2) + 1)}.png");
		}

		internal static void Render(Bitmap image, Graphics graphics)
		{

			graphics.Clear(Color.Black);

			if (_drawRect.Width == 0 && _drawRect.Height == 0)
			{
				_drawRect.Width = image.Width;
				_drawRect.Height = image.Height;
			}

			// Frames 0   > 120 "The Penmans"
			// Frames 121 > 240 "Present"
			// Frames 241 > 480 logo screen
			//
			// Fade in for first 40 frames
			// Fare ot for the last 40 frames

			if (_localFrame < 121)
			{
				graphics.DrawString("- The Penmans -", DrawFont, DrawBrush, _drawRect, DrawAlignment);
			}
			else if (_localFrame < 241)
			{
				graphics.DrawString("- Present -", DrawFont, DrawBrush, _drawRect, DrawAlignment);
			}
			else if (_localFrame < 480)
			{
				Debug.Assert(_logo != null, nameof(_logo) + " != null");
				graphics.DrawImage(_logo, new Point(0, 0));
			}

			int alpha = 0;

			if (_localFrame < 40)
			{
				alpha = (int)(255 - 255.0f / 40.0f * _localFrame);
			}
			else if (_localFrame is > 81 and < 121)
			{
				alpha = (int)(255.0f / 40.0f * (_localFrame-81));
			}
			else if (_localFrame is > 120 and < 160)
			{
				alpha = (int)(255 - 255.0f / 40.0f * (_localFrame-121));
			}
			else if (_localFrame is > 201 and < 240)
			{
				alpha = (int)(255.0f / 40.0f * (_localFrame - 201));
			}
			else if (_localFrame is > 240 and < 280)
			{
				alpha = (int)(255 - 255.0f / 40.0f * (_localFrame - 241));
			}
			else if (_localFrame is > 441 and < 480)
			{
				alpha = (int)(255.0f / 40.0f * (_localFrame - 441));
			} 

			using (Brush cloud_brush = new SolidBrush(Color.FromArgb(alpha, Color.Black)))
			{
				graphics.FillRectangle(cloud_brush, _drawRect);
			}

			_localFrame++;


		}

		internal static void ProcessInput()
		{

		}

		internal static void UpdateGameState()
		{
			if (_localFrame >= 481)
			{
				Program.gameState = GameState.MainMenu;
			}
		}
	}
}
