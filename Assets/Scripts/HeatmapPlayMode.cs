using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Multimedia;
using System;
using System.Linq;
using UnityEngine;

public class HeatmapPlayMode : PlayMode
{
    public override void Process(MidiEvent e)
    {
        var currentTime = DateTime.Now;
        Note note = null;

        if (IsNotePlayed(e))
        {
            note = ComputeNoteProperties(e, currentTime);
            notes.AddLast(note);

            if (IsCorrectNotePlayed(note.NoteName))
            {
                ActivateNote(note.StringNum, note.Fret, false);
            }
            else
            {
                ActivateNote(note.StringNum, note.Fret, true);
            }
        }

        else if (IsNoteStopped(e))
        {
            var midi = ((NoteEvent)e).NoteNumber;
            note = RemoveNote(midi);
            note.SetEndTime((currentTime - startTime).TotalSeconds);
        }

        LogNote(e, note);
    }
}
