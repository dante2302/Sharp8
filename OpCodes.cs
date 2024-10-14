namespace Sharp8;

public class OpCodes(Chip8 chip8)
{
    private Chip8 _chip8 = chip8;
    public void _00E0()
    {
        _chip8.Gfx = new byte[64*32];
    }
}