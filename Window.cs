using System;
using System.ComponentModel;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace Sharp8;

public class Window : GameWindow, IWindow
{
    private Chip8 chip8;
    public Window(GameWindowSettings settings, NativeWindowSettings nativeSettings) : base(settings, nativeSettings)
    {
        chip8 = new(this);
        FileDrop += On_FileDrop;
    }

    private void On_FileDrop(FileDropEventArgs args)
    {
        string rom = args.FileNames[0];

        // Start Chip8;
        chip8.Run(rom);
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
