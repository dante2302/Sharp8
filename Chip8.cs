namespace Sharp8;

public class Chip8
{
    // Memory
    private ushort RomStart { get; }
    private byte[] Memory { get; set; }

    // Registers
    private ushort I { get; set;}
    private byte[] V { get; set; }
    private byte DelayTimer { get; set; }
    private byte SoundTimer { get; set; }

    // Pseudo-registers
    private byte StackPointer { get; set; }
    private ushort ProgramCounter { get; set; }

    private ushort[] Stack { get; set; } = new ushort[16];

    public Chip8()
    {
        RomStart = 0x200;
        Memory = new byte[4096];

        I = 0;
        V = new byte[16];
        DelayTimer = 0;
        SoundTimer = 0;

        StackPointer = 0;
        ProgramCounter = RomStart;
    }

    public void Run(byte[] rom)
    {
        //Setup Fonts
        Array.Copy(DefaultSprites.fontSet, Memory, 0x0);

        // Setup ROM
        Array.Copy(rom, Memory, RomStart);
    }
}
