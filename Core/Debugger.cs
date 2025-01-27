using System.Text;

namespace Sharp8.Core;

public class Debugger(Chip8 chip8)
{
    private readonly Chip8 chip8 = chip8;
    public void DebugGraphics()
    {
        var output = new StringBuilder();
        var Gfx = chip8.Gfx;

        for (int i = 0; i < Gfx.Length; i += 64)
        {
            output.Append($"0x{i:X3}:");
            output.Append($" 0x{Gfx[i]:X2}");
            output.Append($" 0x{Gfx[i + 1]:X2}");
            output.Append($" 0x{Gfx[i + 2]:X2}");
            output.Append($" 0x{Gfx[i + 3]:X2}");
            output.Append($" 0x{Gfx[i + 4]:X2}");
            output.Append($" 0x{Gfx[i + 5]:X2}");
            output.Append($" 0x{Gfx[i + 6]:X2}");
            output.Append($" 0x{Gfx[i + 7]:X2}");
            output.AppendLine();
        }

        output.AppendLine(" ----------------------------------------------------------------");
        for (int i = 0; i < 32; i++)
        {
            output.Append('|');
            for (int j = 0; j < 64; j++)
            {
                output.Append(Gfx[i * 64 + j] > 0 ? $"{i*64+j}█" : " ");
            }
            output.AppendLine("|");
        }
        output.AppendLine(" ----------------------------------------------------------------");

        Console.WriteLine(output);
    }

    public void DebugMemory()
    {
        var output = new StringBuilder();
        var Memory = chip8.Memory;

        for (int i = 0; i <= 0xfff; i += 8)
        {
            output.Append($"0x{i:X3}:");
            output.Append($" 0x{Memory[i]:X2}");
            output.Append($" 0x{Memory[i + 1]:X2}");
            output.Append($" 0x{Memory[i + 2]:X2}");
            output.Append($" 0x{Memory[i + 3]:X2}");
            output.Append($" 0x{Memory[i + 4]:X2}");
            output.Append($" 0x{Memory[i + 5]:X2}");
            output.Append($" 0x{Memory[i + 6]:X2}");
            output.Append($" 0x{Memory[i + 7]:X2}");
            output.AppendLine();
        }

        Console.WriteLine(output);
    }

    public void DebugRegister(ushort opCode)
    {
        int I = chip8.I;
        int PC = chip8.ProgramCounter;
        int SP = chip8.StackPointer;

            var output = new StringBuilder();
            output.AppendLine($"PC              0x{PC:X4}");
            output.AppendLine($"OpCode          0x{opCode:X4}");
            output.AppendLine($"I               0x{I:X4}");
            output.AppendLine($"sp              0x{SP:X4}");

            foreach(var register in Enumerable.Range(0, 16)) {
                output.AppendLine($"V{register:X}              0x{chip8.V[register]:X2}");
            }


            Console.WriteLine(output);
    }
}
