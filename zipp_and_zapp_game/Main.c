#include "Main.h"

int __stdcall WinMain(_In_ HINSTANCE Instance, _In_opt_ HINSTANCE PreviousInstance,	_In_ LPSTR CommandLine, _In_ int CmdShow)
{
	UNREFERENCED_PARAMETER(Instance);
	UNREFERENCED_PARAMETER(PreviousInstance);
	UNREFERENCED_PARAMETER(CommandLine);
	UNREFERENCED_PARAMETER(CmdShow);

	MessageBox(NULL, L"Hello from Zipp and Zapp", L"Hi there", MB_OK);
}