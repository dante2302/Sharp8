namespace Sharp8;

public class Chip8
{
    // Memory
    private ushort RomStart { get; }
    public byte[] Memory { get; set; }

    // A 64x32 pixel screen
    public byte[] Gfx { get; set;}

    // Registers
    public ushort I { get; set;}  // Special Index Register
    public byte[] V { get; set; } // 16 Vx Registers x(0 - F);
                                   // VF - Special Flag Register
    public byte DelayTimer { get; set; }
    public byte SoundTimer { get; set; }

    // Pseudo-registers
    public byte StackPointer { get; set; }
    public ushort ProgramCounter { get; set; }

    public ushort[] Stack { get; set; }

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
        Array.Copy(rom,0, Memory, RomStart, rom.Length);
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
        // FirstByte leftshift by 8 - Makes space for the 2nd byte
        // Bitwise OR combines the bytes
        ushort opCode = (ushort)(Memory[ProgramCounter] << 8 | Memory[ProgramCounter + 1]);

        ushort nnn = (ushort)(opCode & 0x0FFF);
        byte kk = (byte)(opCode & 0x00FF);
        byte x = (byte)(opCode & 0x0F00);
        byte y = (byte)(opCode & 0x00F0);
        ProgramCounter += 2;

        switch(opCode & 0xF000)
        {
            case 0x0000 when opCode == 0x00E0:
                OpCodes._00E0(this);
                break;

            case 0x0000 when opCode == 0x00EE:
                OpCodes._00EE(this);
                break;

            case 0x1000:
                OpCodes._1nnn(this, nnn);
                break;

            case 0x2000:
                OpCodes._2nnn(this, nnn);
                break;

            case 0x3000:
                OpCodes._3xkk(this, x, kk);
                break;
            default:
                Console.WriteLine("Unknown Opcode");
                break;
        }
    }
}