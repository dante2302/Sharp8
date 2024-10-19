namespace Sharp8.Tests;

public class OpCodeTests 
{

    private readonly Chip8 _chip8;
    public OpCodeTests()
    {
        // Initializing chip8 without a window
        // as window is decoupled from opcode
        // so we only need access to the graphics array
        _chip8 = new(null);
    }

    [Fact]
    public void _00E0()
    {
        _chip8.Run([0x00, 0xE0]);

        byte[] expected = new byte[64*32];
        _chip8.EmulateCycle();

        Assert.Equal(expected, _chip8.Gfx);
    }

    [Fact]
    public void _00EE()
    {
        _chip8.Stack[1] = 13;
        _chip8.StackPointer = 1;
        _chip8.Run([0x00, 0xEE]);

        byte expected = 13;
        _chip8.EmulateCycle();

        Assert.Equal(expected, _chip8.ProgramCounter);
    }

    [Fact]
    public void _1nnn()
    {
        _chip8.Run([0x12, 0x34]);

        int expected = 0x234;
        _chip8.EmulateCycle();

        Assert.Equal(expected, _chip8.ProgramCounter);
    }

    [Fact]
    public void _2nnn()
    {
        ushort expectedPCBefore = (ushort)(_chip8.ProgramCounter+2);
        _chip8.Run([0x22, 0x34]);

        int expectedPCAfter = 0x234;
        _chip8.EmulateCycle();

        Assert.Equal(expectedPCBefore, _chip8.Stack[_chip8.StackPointer]);
        Assert.Equal(expectedPCAfter, _chip8.ProgramCounter);
    }

    [Fact]
    public void _Annn()
    {
        _chip8.Run([0xA2, 0x34]);

        _chip8.EmulateCycle();

        Assert.Equal(0x234, _chip8.I);
    }

    [Fact]
    public void _Bnnn()
    {
        _chip8.V[0] = 0x5;
        _chip8.Run([0xB2, 0x34]);

        int expected = 0x239;
        
        _chip8.EmulateCycle();

        Assert.Equal(expected, _chip8.ProgramCounter);
    }

    [Fact]
    public void _3xkk__True()
    {
        ushort ProgramCounterBefore = (ushort)(_chip8.ProgramCounter + 2);
        _chip8.V[0] = 0x22;
        _chip8.Run([0x30, 0x22]);

        _chip8.EmulateCycle();

        Assert.Equal(ProgramCounterBefore+2, _chip8.ProgramCounter);
    }

    [Fact]
    public void _3xkk__False()
    {
        ushort ProgramCounterBefore = (ushort)(_chip8.ProgramCounter + 2);
        _chip8.V[0] = 0x21;
        _chip8.Run([0x30, 0x22]);

        _chip8.EmulateCycle();

        Assert.Equal(ProgramCounterBefore, _chip8.ProgramCounter);
    }

    [Fact]
    public void _4xkk__True()
    {
        ushort ProgramCounterBefore = (ushort)(_chip8.ProgramCounter + 2);
        _chip8.V[0] = 21;
        _chip8.Run([0x40, 0x22]);

        _chip8.EmulateCycle();

        Assert.Equal(ProgramCounterBefore+2, _chip8.ProgramCounter);
    }

    [Fact]
    public void _4xkk__False()
    {
        ushort ProgramCounterBefore = (ushort)(_chip8.ProgramCounter + 2);
        _chip8.V[0] = 0x21;
        _chip8.Run([0x40, 0x21]);

        _chip8.EmulateCycle();

        Assert.Equal(ProgramCounterBefore, _chip8.ProgramCounter);
    }

    [Fact]
    public void _6xkk()
    {
        _chip8.Run([0x60, 0x21]);

        int expected = 0x21;
        _chip8.EmulateCycle();

        Assert.Equal(expected, _chip8.V[0]);
    }

    [Fact]
    public void _7xkk()
    {
        _chip8.V[0] = 0x1;
        int expected = 0x21 + _chip8.V[0];
        _chip8.Run([0x60, 0x21]);

        _chip8.EmulateCycle();

        Assert.Equal(expected, _chip8.V[0]);
    }
}