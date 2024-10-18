using Microsoft.VisualBasic;

namespace Sharp8;

public class OpCodes()
{
    public static void _00E0(Chip8 chip8)
    {
        chip8.Gfx = new byte[64*32];
    }

    public static void _00EE(Chip8 chip8)
    {
        chip8.ProgramCounter = chip8.Stack[chip8.StackPointer];
        chip8.StackPointer -= 1;
    }

    // NNN BLOCK
    public static void _1nnn(Chip8 chip8, ushort nnn)
    {
        chip8.ProgramCounter = nnn;
    }

    public static void _2nnn(Chip8 chip8, ushort nnn)
    {
        chip8.StackPointer++;
        chip8.Stack[chip8.StackPointer] = chip8.ProgramCounter;
        chip8.ProgramCounter = nnn;
    }

    public static void _Annn(Chip8 chip8, ushort nnn)
    {
        chip8.I = nnn;
    }

    public static void _Bnnn(Chip8 chip8, ushort nnn)
    {
        chip8.ProgramCounter = (ushort)(nnn + chip8.V[0]);
    }
    // END OF NNN BLOCK

    // KK BLOCK

    public static void _3xkk(Chip8 chip8, byte x, byte kk)
    {
        if(chip8.V[x] == kk) 
            chip8.ProgramCounter += 2;
    }

    public static void _4xkk(Chip8 chip8, byte x, byte kk)
    {
        if(chip8.V[x] != kk)
            chip8.ProgramCounter += 2;
    }

    public static void _6xkk(Chip8 chip8, byte x, byte kk)
    {
        chip8.V[x] = (byte)kk;
    }

    public static void _7xkk(Chip8 chip8, byte x, byte kk)
    {
        chip8.V[x] += kk;
    }

    public static void _Cxkk(Chip8 chip8, byte x, byte kk)
    {
        byte randomNumber = (byte)(new Random().Next(0, 256));
        chip8.V[x] = (byte)(randomNumber & kk);
    }

    // END OF KK BLOCK

    // XY BLOCK

    public static void _5xy0(Chip8 chip8, byte x, byte y)
    {
        if(chip8.V[x] == chip8.V[y]) 
            chip8.ProgramCounter += 2;
    }

    public static void _8xy0(Chip8 chip8, byte x, byte y)
    {
        chip8.V[x] = chip8.V[y];
    }

    public static void _8xy1(Chip8 chip8, byte x, byte y)
    {
        chip8.V[x] = (byte)(chip8.V[x] | chip8.V[y]);
    }

    public static void _8xy2(Chip8 chip8, byte x, byte y)
    {
        chip8.V[x] = (byte)(chip8.V[x] & chip8.V[y]);
    }

    public static void _8xy3(Chip8 chip8, byte x, byte y)
    {
        chip8.V[x] = (byte)(chip8.V[x] ^ chip8.V[y]);
    }

    public static void _8xy4(Chip8 chip8, byte x, byte y)
    {
        ushort sum = (ushort)(chip8.V[x] + chip8.V[y]);
        if(sum > 255)
            chip8.V[0xF] = (byte)(sum - 255);
        chip8.V[x] = (byte)(sum & 0xFF);
    }

    public static void _8xy5(Chip8 chip8, byte x, byte y)
    {
        if(chip8.V[y] > chip8.V[x])
            chip8.V[0xF] = 1;
        else  
            chip8.V[0xF] = 0;
        
        chip8.V[x] = (byte)(chip8.V[y] - chip8.V[x]);
    }

    public static void _8xy6(Chip8 chip8, byte x, byte y)
    {
        chip8.V[0xF] = (byte)(chip8.V[x] & 0x1);
        chip8.V[x] >>= 0x1;
    }

    public static void _8xy7(Chip8 chip8, byte x, byte y)
    {
        if(chip8.V[y] > chip8.V[x])
            chip8.V[0xF] = 1;
        else  
            chip8.V[0xF] = 0;
        
        chip8.V[x] = (byte)(chip8.V[x] - chip8.V[y]);
    }

    public static void _8xyE(Chip8 chip8, byte x, byte y)
    {
        // Mask the most significant bit and shift it to the start
        // If its 1 then VF is 1
        // Else 0
        chip8.V[0xF] = (byte)((Helpers.MsbByteMask & chip8.V[x]) >> 7);
        chip8.V[x] <<= 0x1;
    }

    public static void _9xy0(Chip8 chip8, byte x, byte y)
    {
        if(chip8.V[x] != chip8.V[y])
            chip8.ProgramCounter += 2;
    }
}