using System;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace Sharp8;

public class Window : GameWindow, IWindow
{
    public Window(GameWindowSettings settings, NativeWindowSettings nativeSettings) : base(settings, nativeSettings)
    {
        FileDrop += OnFileDrop;
    }

    public void OnFileDrop(FileDropEventArgs args)
    {
        string rom = args.FileNames[0];
        // Start Chip8;
    }

    public void Render()
    {
        // To Be Implemented
    }

    public void Beep()
    {
        // To Be Implemented
    }
}
