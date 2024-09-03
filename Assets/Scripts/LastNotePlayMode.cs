using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.Multimedia;
using Melanchall.DryWetMidi.MusicTheory;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LastNotePlayMode : PlayMode
{
    public override void Process(object sender, MidiEventReceivedEventArgs e)
    {
        var currentTime = DateTime.Now;
        var midiDevice = (MidiDevice)sender;
        Note note = null;

        if (IsNotePlayed(e))
        {
            note = ComputeNoteProperties(e, currentTime);
            notes.AddLast(note);

            if (AllowedNotes.Contains(note.NoteName))
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
            var midi = ((NoteEvent)e.Event).NoteNumber;
            note = RemoveNote(midi);
            note.SetEndTime((currentTime - startTime).TotalSeconds);
            DeactivateNote(note.StringNum, note.Fret);
        }

        LogNote(e, midiDevice, note);
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
