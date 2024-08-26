using System.Collections.Generic;

public class Helper
{
    public static int[] tuningPitchEStd = {64, 59, 55, 50, 45, 40};

    public static string[] noteNames = {"C", "C#/Db", "D", "D#/Eb", "E", "F", "F#/Gb", "G", "G#/Ab", "A", "A#/Bb", "B"};

    public static Dictionary<string, string[]> notesInScale = new Dictionary<string, string[]>
    {
        { "Cmajor", new string[]{ "C", "D", "E", "F#/Gb", "G", "A", "B"} },
        { "Dmajor", new string[]{ "D", "E", "F#/Gb", "G#/Ab", "A", "B", "C#/Db"} },
        { "Emajor", new string[]{ "E", "F#/Gb", "G#/Ab", "A#/Bb", "B", "C#/Db", "D#/Eb"} },
        { "Fmajor", new string[]{ "F", "G", "A", "B", "C", "D", "E"} },
    };
}


