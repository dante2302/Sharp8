using System.Drawing;
using System.ComponentModel;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace Sharp8;

public class Window : GameWindow, IWindow
{
    private readonly Chip8 chip8;
    private bool isRunning;
    public Window(GameWindowSettings settings, NativeWindowSettings nativeSettings) : base(settings, nativeSettings)
    {
        chip8 = new(this);
        FileDrop += On_FileDrop;
    }

    private void On_FileDrop(FileDropEventArgs args)
    {
        string rom = args.FileNames[0];
        chip8.Run(rom);
        isRunning = true;
    }


    protected override void OnResize(ResizeEventArgs e)
    {
        base.OnResize(e);

        GL.Viewport(0, 0, e.Width, e.Height);
    }

    public void Render()
    {
        for(int y = 0; y < 32; y++)
        {
            for(int x = 0; x < 64; x++)
            {
                if(chip8.Gfx[y * 64 + x] > 0)
                    GL.Rect(x, y, x+1, y+1);
            }
        }
        SwapBuffers();
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

    protected override void OnUpdateFrame(FrameEventArgs args)
    {
        base.OnUpdateFrame(args);
        if(isRunning) chip8.EmulateCycle();
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