using System.Collections.Generic;
using UnityEngine;

public class Helper
{
    public static int[] tuningPitchEStd = {64, 59, 55, 50, 45, 40};
    public static string[] noteNames = {"C", "C#/Db", "D", "D#/Eb", "E", "F", "F#/Gb", "G", "G#/Ab", "A", "A#/Bb", "B"};
    public static string[] openStringNoteNames = {"E", "B", "G", "D", "A", "E"};
    public static string[] naturalNotes = { "C", "D", "E", "F", "G", "A", "B" }; // menu list items for choosing individual notes to play

    /* 
     * menu items for choosing scale to play
     * Whatever values you have here, should also be present in the allowedNotes dictionary
     * with the appropriate notes. The values should match the keys in the allowedNotes dictionary
    */
    public static string[] scales = 
    { 
        "A Major", "B Major", "C Major", "D Major", "E Major", "F Major", "G Major",
        "A Minor", "E Minor", "B Minor", "F# Minor",
        "A Minor Pentatonic", "C Major Pentatonic", "E Blues",
        "D Dorian", "G Mixolydian", "A Phrygian", "C Harmonic Minor", "G#/Ab Diminished", "D Whole Tone"
    };

    public static Dictionary<string, string[]> allowedNotes = new Dictionary<string, string[]>
    {
        // Notes
        { "A", new string[]{ "A" }},
        { "B", new string[]{ "B" }},
        { "C", new string[]{ "C" }},
        { "D", new string[]{ "D" }},
        { "E", new string[]{ "E" }},
        { "F", new string[]{ "F" }},
        { "G", new string[]{ "G" }},

        //Major Scales
        { "C Major", new string[]{ "C", "D", "E", "F", "G", "A", "B" } },
        { "G Major", new string[]{ "G", "A", "B", "C", "D", "E", "F#/Gb" } },
        { "D Major", new string[]{ "D", "E", "F#/Gb", "G", "A", "B", "C#/Db" } },
        { "A Major", new string[]{ "A", "B", "C#/Db", "D", "E", "F#/Gb", "G#/Ab" } },
        { "E Major", new string[]{ "E", "F#/Gb", "G#/Ab", "A", "B", "C#/Db", "D#/Eb" } },
        { "F Major", new string[]{ "F", "G", "A", "A#/Bb", "C", "D", "E" } },
        { "B Major", new string[]{ "B", "C#/Db", "D#/Eb", "E", "F#/Gb", "G#/Ab", "A#/Bb" } },

        // Minor Scales (Natural)
        { "A Minor", new string[]{ "A", "B", "C", "D", "E", "F", "G" } },
        { "E Minor", new string[]{ "E", "F#/Gb", "G", "A", "B", "C", "D" } },
        { "B Minor", new string[]{ "B", "C#/Db", "D", "E", "F#/Gb", "G", "A" } },
        { "F# Minor", new string[]{ "F#/Gb", "G#/Ab", "A", "B", "C#/Db", "D", "E" } },

        // Pentatonic Scales
        { "A Minor Pentatonic", new string[]{ "A", "C", "D", "E", "G" } },
        { "C Major Pentatonic", new string[]{ "C", "D", "E", "G", "A" } },
        { "E Blues", new string[]{ "E", "G", "A", "A#/Bb", "B", "D" } },

        // Modal Scales
        { "D Dorian", new string[]{ "D", "E", "F", "G", "A", "B", "C" } },
        { "G Mixolydian", new string[]{ "G", "A", "B", "C", "D", "E", "F" } },
        { "A Phrygian", new string[]{ "A", "A#/Bb", "C", "D", "E", "F", "G" } },

        // Other Common Scales
        { "C Harmonic Minor", new string[]{ "C", "D", "D#/Eb", "F", "G", "G#/Ab", "B" } },
        { "G#/Ab Diminished", new string[]{ "G#/Ab", "B", "D", "F" } },
        { "D Whole Tone", new string[]{ "D", "E", "F#/Gb", "G#/Ab", "A#/Bb", "C" } },
    };

    public static double[] fretDistsFromNut =
    {
        0,
        0.036316,
        0.070596,
        0.102954,
        0.133498,
        0.162329,
        0.189543,
        0.215232,
        0.23948,
        0.262369,
        0.283974,
        0.304368,
        0.323619,
        0.34179,
        0.358942,
        0.375133,
        0.390415,
        0.404841,
        0.418458,
        0.431312,
        0.443444,
        0.454897 
    };
}

public enum MoveDirection
{
    Left,
    Right,
    Up,
    Down,
    Backwards,
    Forwards
}


