namespace Sharp8.Core;

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
        chip8.V[x] = kk;
    }

    public static void _7xkk(Chip8 chip8, byte x, byte kk)
    {
        chip8.V[x] += kk;
    }

    public static void _Cxkk(Chip8 chip8, byte x, byte kk)
    {
        byte randomNumber = (byte)new Random().Next(0, 256);
        chip8.V[x] = (byte)(randomNumber & kk);
    }

    // END OF KK BLOCK

    // XY BLOCK

    public static void _5xy0(Chip8 chip8, byte x, byte y)
    {
        if (chip8.V[x] == chip8.V[y])
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
        if (chip8.V[y] > (0xFF - chip8.V[x]))
            chip8.V[0xF] = 1; 
        else 
            chip8.V[0xF] = 0;
        chip8.V[x] += chip8.V[y];
    }

    public static void _8xy5(Chip8 chip8, byte x, byte y)
    {
        if (chip8.V[x] > chip8.V[y])
            chip8.V[0xF] = 1;
        else
            chip8.V[0xF] = 0;

        chip8.V[x] = (byte)(chip8.V[x] - chip8.V[y]);
    }

    public static void _8xy6(Chip8 chip8, byte x, byte y)
    {
        chip8.V[0xF] = (byte)(chip8.V[x] & 0x1);
        chip8.V[x] >>= 0x1;
    }

    public static void _8xy7(Chip8 chip8, byte x, byte y)
    {
        int difference = chip8.V[y] - chip8.V[x];
        chip8.V[0xF] = (byte)(difference > 0 ? 1 : 0);

        chip8.V[x] = (byte)(difference & 0xFF);
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
        if (chip8.V[x] != chip8.V[y])
            chip8.ProgramCounter += 2;
    }

    // Ex and Fx BLOCK

    public static void _Ex9E(Chip8 chip8, byte x)
    {
        bool keyIsPressed = chip8.Keys[chip8.V[x]];
        if(keyIsPressed)
            chip8.ProgramCounter += 2;
    }

    public static void _ExA1(Chip8 chip8, byte x)
    {
        bool keyIsPressed = chip8.Keys[chip8.V[x]];
        if(!keyIsPressed)
            chip8.ProgramCounter += 2;
    }

    public static void _Fx07(Chip8 chip8, byte x)
    {
        chip8.V[x] = chip8.DelayTimer;
    }

    public static void _Fx0A(Chip8 chip8)
    {
    }

    public static void _Fx15(Chip8 chip8, byte x)
    {
        chip8.DelayTimer = chip8.V[x];
    }

    public static void _Fx18(Chip8 chip8, byte x)
    {
        chip8.SoundTimer = chip8.V[x];
    }

    public static void _Fx1E(Chip8 chip8, byte x)
    {
        chip8.I = (ushort)(chip8.V[x] + chip8.I);
    }

    public static void _Fx29(Chip8 chip8, byte x)
    {
        chip8.I = (byte)(chip8.V[x] * 5);
    }

    public static void _Fx33(Chip8 chip8, byte x)
    {
        int value = chip8.V[x];
        byte firstDigit = (byte)(value / 100);
        byte secondDigit = (byte)((value / 10) % 10);
        byte thirdDigit = (byte)(value % 10);
        chip8.Memory[chip8.I] = firstDigit;
        chip8.Memory[chip8.I + 1] = secondDigit;
        chip8.Memory[chip8.I + 2] = thirdDigit;
    }

    public static void _Fx55(Chip8 chip8, byte x)
    {
        for (int i = 0; i <= x; i++)
        {
            chip8.Memory[chip8.I+i] = chip8.V[i];
        }
    }

    public static void _Fx65(Chip8 chip8, byte x)
    {
        for (int i = 0; i <= x; i++)
        {
            chip8.V[i] = chip8.Memory[chip8.I+i];
        }
    }

    public static void _Dxyn(Chip8 chip8, byte x, byte y, byte n)
    {
        chip8.V[0xF] = 0;

        for (int line = 0; line < n; line++)
        {
            var yCord = (chip8.V[y] + line) % 32;

            byte spriteByte = chip8.Memory[chip8.I + line];

            for (int column = 0; column < 8; column++)
            {
                if ((spriteByte & 0x80) != 0)
                {
                    var xCord = (chip8.V[x] + column) % 64;

                    if (chip8.Gfx[yCord * 64 + xCord] == 1)
                    {
                        chip8.V[0xF] = 1;
                    }

                    chip8.Gfx[yCord * 64 + xCord] ^= 1;
                }

                spriteByte <<= 0x1;
            }
        }
    }
}