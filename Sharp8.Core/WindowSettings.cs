using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;

namespace Sharp8;

public readonly struct WindowSettings
{
    public static GameWindowSettings GameSettings { get; } = new()
    {
        UpdateFrequency = 60 
    };

    public static NativeWindowSettings NativeSettings { get; } = new()
    {
        ClientSize = new Vector2i(1024, 512),
        Profile = ContextProfile.Compatability,
        Title = "Chip8 Emulator"
    };
}