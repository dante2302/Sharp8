namespace Core;

public struct ErrorMessages
{
    public static string ROMSizeTooBig(int maxRomSize, int romLength) 
    { 
            return @$"
            -----------------------------------
            -   ROM is too big                - 
            -   Max ROM size: {maxRomSize}    -
            -   Your ROM size: {romLength}    -
            -----------------------------------";
    }

    public static string InvalidOpcode(ushort opCode, ushort ProgramCounter)
    {
        return $"error: Invalid OpCode: {opCode:X4} @ PC = 0x{ProgramCounter:X4}";
    }
}