using System.Collections.Generic;

public class Helper
{
    public static int[] tuningPitchEStd = {64, 59, 55, 50, 45, 40};
    public static string[] noteNames = {"C", "C#/Db", "D", "D#/Eb", "E", "F", "F#/Gb", "G", "G#/Ab", "A", "A#/Bb", "B"};
    public static string[] openStringNoteNames = {"E", "B", "G", "D", "A", "E"};
    public static string[] noteNamesToDisplay = { "C", "D", "E", "F", "G", "A", "B" };

    public static Dictionary<string, string[]> scales = new Dictionary<string, string[]>
    {
        { "Amajor", new string[]{ "A", "B", "C#", "D", "E", "F#", "G#"} },
        { "Bmajor", new string[]{ "B", "C#/Db", "D#/Eb", "E", "F#/Gb", "G#/Ab", "A#/Bb"} },
        { "Cmajor", new string[]{ "C", "D", "E", "F", "G", "A", "B"} },
        { "Dmajor", new string[]{ "D", "E", "F#/Gb", "G", "A", "B", "C#/Db"} },
        { "Emajor", new string[]{ "E", "F#/Gb", "G#/Ab", "A", "B", "C#/Db", "D#/Eb"} },
        { "Fmajor", new string[]{ "F", "G", "A", "A#/Bb", "C", "D", "E"} },
        { "Gmajor", new string[]{ "G", "A", "B", "C", "D", "E", "F#/Gb"} },
        { "Aminor Pentatonic", new string[]{ "A", "B", "C", "D", "E", "F", "G"} },
        { "Bminor Pentatonic", new string[]{ "B", "C#/Db", "D", "E", "F#/Gb", "G", "A"} },
        { "Cminor Pentatonic", new string[]{ "C", "D", "D#/Eb", "F", "G", "G#/Ab", "A#/Bb"} },
        { "Dminor Pentatonic", new string[]{ "D", "E", "F", "G", "A", "A#/Bb", "C"} },
        { "Eminor Pentatonic", new string[]{ "E", "F#/Gb", "G", "A", "B", "C", "D"} },
        { "Fminor Pentatonic", new string[]{ "F", "G", "G#/Ab", "A#/Bb", "C", "C#/Db", "D#/Eb"} },
        { "Gminor Pentatonic", new string[]{ "G", "A", "A#/Bb", "C", "D", "D#/Eb", "F"} }
    };
}


