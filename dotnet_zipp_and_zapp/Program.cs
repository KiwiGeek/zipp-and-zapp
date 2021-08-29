#define HORZ_LINES
#define COLORED_DOTS

using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using MiniFBSharp;
using MiniFBSharp.Enums;

namespace dotnet_zipp_and_zapp
{

	public enum GameState
	{
		TitleScreen,
		MainMenu,
		Options
	}

	class Program
	{

		private const int GAME_RES_WIDTH = 384;
		private const int GAME_RES_HEIGHT = 240;

		internal static GameState gameState = GameState.TitleScreen;
		private static MiniFBWindow _gameWindow;
		private static Bitmap _image;
		private static Graphics _graphics;

		private static void Main()
		{

			_image = new Bitmap(GAME_RES_WIDTH, GAME_RES_HEIGHT);
			_graphics = Graphics.FromImage(_image);
			_graphics.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;

			_gameWindow = MiniFB.OpenWindow("The Adventures of Zipp and Zapp", new Size(800, 450), WindowFlags.Resizable);

			MiniFB.SetTargetFPS(60);

			do
			{

				// Main game loop:
				//
				// 1) Update the game state
				// 2) Process any input
				// 3) Render the screen
				// 4) Wait for Sync.

				UpdateGameState();

				ProcessPlayerGameInput();

				if (RenderScreen() < 0) { break; }

			} while (MiniFB.WaitSync(_gameWindow));

		}

		private static void ProcessPlayerGameInput()
		{
			switch (gameState)
			{
				case GameState.TitleScreen:
				{
					GameStates.TitleScreen.ProcessInput();
					break;
				}
				case GameState.MainMenu:
				{
					break;
				}
				case GameState.Options:
				{
					break;
				}
			}
		}

		private static void UpdateGameState()
		{
			switch (gameState)
			{
				case GameState.TitleScreen:
				{
					GameStates.TitleScreen.UpdateGameState();
					break;
				}
				case GameState.MainMenu:
				{
					break;
				}
				case GameState.Options:
				{
					break;
				}
			}
		}

		private static int RenderScreen()
		{
			switch (gameState)
			{
				case GameState.TitleScreen:
				{
					GameStates.TitleScreen.Render(_image, _graphics);
					break;
				}
				case GameState.MainMenu:
				{
					break;
				}
				case GameState.Options:
				{
					break;
				}
			}

			return MiniFB.Update(_gameWindow, _image, new Size(GAME_RES_WIDTH, GAME_RES_HEIGHT));
		}

	}
}
