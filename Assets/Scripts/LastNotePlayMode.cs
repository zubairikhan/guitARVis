using Melanchall.DryWetMidi.Core;
using System;
using UnityEngine;

public class LastNotePlayMode : PlayMode
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
            if (note != null)
            {
                Debug.Log("error note: " + midi);
                note.SetEndTime((currentTime - startTime).TotalSeconds);
                DeactivateNote(note.StringNum, note.Fret);
            }
        }

        LogNote(e, note);
    }

    
    private void DeactivateNote(int stringNum, int fretNum)
    {
        string key = GetKey(stringNum, fretNum);
        if (frets.TryGetValue(key, out var fret))
        {
            fret.SetActivated(false);
        }
        else
        {
            Debug.Log("Couldn't find fret to deactivate");
        }
    }
}
