using System.Diagnostics;

namespace UI;

public class SoundManager
{
    public static void PlayLinuxSound()
    {

        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "mpv",
                Arguments = "./beep.mp3",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            }
        };
        process.Start();
        process.WaitForExit();
    }

    public static void PlayWindowsSound()
    {
        Console.Beep();
    }
}