namespace Sharp8;

public class Chip8
{
    // Memory
    private ushort RomStart { get; }
    private byte[] Memory { get; set; }

    // Registers
    private ushort I { get; set;}  // Special Index Register
    private byte[] V { get; set; } // 16 Vx Registers x(0 - F);
                                   // VF - Special Flag Register
    private byte DelayTimer { get; set; }
    private byte SoundTimer { get; set; }

    // Pseudo-registers
    private byte StackPointer { get; set; }
    private ushort ProgramCounter { get; set; }

    private ushort[] Stack { get; set; }

    public Chip8()
    {
        RomStart = 0x200;
        Memory = new byte[4096];

        I = 0;
        V = new byte[16];
        DelayTimer = 0;
        SoundTimer = 0;

        Stack = new ushort[16];
        StackPointer = 0;
        ProgramCounter = RomStart;
    }

    public void Run(byte[] rom)
    {
        if(rom.Length > Memory.Length - RomStart)
        {
            // Print 
            // Rom is too big
            return;
        }
        //Setup Fonts
        Array.Copy(DefaultSprites.FontSet, Memory, 0x0);

        // Setup ROM
        Array.Copy(rom, Memory, RomStart);
    }
}