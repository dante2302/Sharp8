namespace Sharp8.Tests;

public class OpCodeTests 
{

    // Initializing chip8 without a window
    // as window is decoupled from opcode
    // so we only need access to the graphics array
    private readonly Chip8 _chip8 = new(null);

    // Arrangement of tests is done byt the following pattern:
    // call chip8.Run(opcode)
    // opcode - an array of bytes for the opcode(typically just 2)

    [Fact]
    public void _00E0()
    {
        _chip8.Run([0x00, 0xE0]);

        byte[] expected = new byte[64*32];
        _chip8.EmulateCycle();

        Assert.Equal(expected, _chip8.Gfx);
    }
}