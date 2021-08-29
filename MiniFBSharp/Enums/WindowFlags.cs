using System;
using System.Diagnostics.CodeAnalysis;

namespace MiniFBSharp.Enums
{
	[Flags]
	[SuppressMessage("ReSharper", "UnusedMember.Global")]
	internal enum MfbWindowFlags
	{
		WF_RESIZABLE = 0x01,
		WF_FULLSCREEN = 0x02,
		WF_FULLSCREEN_DESKTOP = 0x04,
		WF_BORDERLESS = 0x08,
		WD_ALWAYS_ON_TOP = 0x16
	}

	[Flags]
	[SuppressMessage("ReSharper", "UnusedMember.Global")]
	public enum WindowFlags
	{
		Resizable = 1,
		Fullscreen = 2,
		FullscreenDesktop = 4,
		Borderless = 8,
		AlwaysOnTop = 16
	}
}
