using System.Drawing;
using System.ComponentModel;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using Sharp8.Core;
using System.Runtime.InteropServices;
using System.Diagnostics;
using UI;

namespace Sharp8.UI;

public class Window : GameWindow, IWindow
{
    private Chip8 chip8;
    private bool isRunning;
    public Window(GameWindowSettings settings, NativeWindowSettings nativeSettings) : base(settings, nativeSettings)
    {
        chip8 = new(this);
        FileDrop += On_FileDrop;
    }

    private void On_FileDrop(FileDropEventArgs args)
    {
        string rom = args.FileNames[0];
        Beep();
        Console.WriteLine("beep");
        chip8 = new(this);
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
        GL.Clear(ClearBufferMask.ColorBufferBit);
        for (int y = 0; y < 32; y++)
        {
            for(int x = 0; x < 64; x++)
            {
                if(chip8.Gfx[y * 64 + x] > 0)
                    GL.Rect(x, y, x+1, y+1);
            }
        }
        SwapBuffers();
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

    protected override void OnKeyUp(KeyboardKeyEventArgs e)
    {
        base.OnKeyUp(e);
        if(Keyboard.Chip8.TryGetValue(e.Key, out byte byteValue))
        {
            chip8.OnKeyUp(byteValue);
        }
    }

    protected override void OnKeyDown(KeyboardKeyEventArgs e)
    {
        base.OnKeyDown(e);
        if(Keyboard.Chip8.TryGetValue(e.Key, out byte byteValue))
        {
            chip8.OnKeyDown(byteValue);
        }
    }

    public void Beep()
    {
        var linux = OSPlatform.Linux;
        var windows = OSPlatform.Windows;
        var IsOS = RuntimeInformation.IsOSPlatform;

        if(IsOS(linux))
            SoundManager.PlayLinuxSound();

        else if(IsOS(windows))
            SoundManager.PlayWindowsSound();
    }

    protected override void OnClosing(CancelEventArgs e)
    {
        base.OnClosing(e);
        Environment.Exit(0);
    }
}