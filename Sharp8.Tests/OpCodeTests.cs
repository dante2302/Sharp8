namespace Sharp8.Tests;

public class OpCodeTests 
{
    private readonly Chip8 _chip8;
    public OpCodeTests()
    {
        IWindow window = new Window(WindowSettings.GameSettings, WindowSettings.NativeSettings);
        _chip8 = new(window);
    }
    [Fact]
    public void _00E0()
    {

    }
}