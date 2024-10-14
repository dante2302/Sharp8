namespace Sharp8;

public class OpCodes()
{
    public static void _00E0(Chip8 chip8)
    {
        chip8.Gfx = new byte[64*32];
    }
}