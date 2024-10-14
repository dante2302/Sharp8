namespace Sharp8;

public class Chip8
{
    // Memory
    private ushort RomStart { get; }
    internal byte[] Memory { get; set; }

    // A 64x32 pixel screen
    internal byte[] Gfx { get; set;}

    // Registers
    internal ushort I { get; set;}  // Special Index Register
    internal byte[] V { get; set; } // 16 Vx Registers x(0 - F);
                                   // VF - Special Flag Register
    internal byte DelayTimer { get; set; }
    internal byte SoundTimer { get; set; }

    // Pseudo-registers
    internal byte StackPointer { get; set; }
    internal ushort ProgramCounter { get; set; }

    internal ushort[] Stack { get; set; }

    public Chip8(IWindow window)
    {
        RomStart = 0x200;
        Memory = new byte[4096];

        Gfx = new byte[64*32];

        I = 0;
        V = new byte[16];
        DelayTimer = 0;
        SoundTimer = 0;

        Stack = new ushort[16];
        StackPointer = 0;
        ProgramCounter = RomStart;

        //Setup Fonts
        Array.Copy(DefaultSprites.FontSet, Memory, 0x0);
    }

    public void Run(byte[] rom)
    {
        if(rom.Length > Memory.Length - RomStart)
        {
            // Print 
            // Rom is too big
            return;
        }
        // Setup ROM
        Array.Copy(rom, Memory, RomStart);
    }

    public void Run(string romPath)
    {
        byte[] romBytes = File.ReadAllBytes(romPath);
        if(romBytes.Length > Memory.Length - RomStart)
        {
            // Print 
            // Rom is too big
            return;
        }
        Run(romBytes);
    }

    public void EmulateCycle()
    {
        // Opcodes are 2 bytes
        // FirstByte << 8 - Makes space for the 2nd byte
        // Bitwise OR combines the bytes
        ushort opCode = (ushort)(Memory[ProgramCounter] << 8 | Memory[ProgramCounter + 1]);

        switch(opCode & 0xF000)
        {
            case 0x0000 when opCode == 0x00E0:
                break;
            default:
                Console.WriteLine("Unkonw Opcode");
                break;
        }
    }
}