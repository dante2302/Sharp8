using System.Drawing;
using System.ComponentModel;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace Sharp8;

public class Window : GameWindow, IWindow
{
    private readonly Chip8 chip8;
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

    protected override void OnLoad()
    {
        base.OnLoad();
        GL.ClearColor(Color.Black);
        GL.Color3(Color.White);
        GL.Ortho(0, 64, 32, 0, -1, 1);
        GL.Clear(ClearBufferMask.ColorBufferBit);

        SwapBuffers();
    }

    protected override void OnKeyDown(KeyboardKeyEventArgs e)
    {
        base.OnKeyDown(e);
        if(Keyboard.Map.TryGetValue(e.Key, out byte byteValue))
        {
            // input key into chip8
        }
    }

    protected override void OnKeyUp(KeyboardKeyEventArgs e)
    {
        base.OnKeyUp(e);
        if(Keyboard.Map.TryGetValue(e.Key, out byte byteValue))
        {
            // input key into chip8
        }
    }

    protected override void OnClosing(CancelEventArgs e)
    {
        base.OnClosing(e);
        Environment.Exit(0);
    }
}