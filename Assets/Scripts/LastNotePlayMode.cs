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

            if (allowedNotes.Contains(note.NoteName))
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

    private void ActivateNote(int stringNum, int fretNum, bool error)
    {
        Fret fret;
        string key = GetKey(stringNum, fretNum);
        var status = frets.TryGetValue(key, out fret);

        if (fret != null && !error)
        {
            fret.SetActivated(true);
        }
        else if (fret != null && error)
        {
            fret.SetError(true);
        }
        else
        {
            Debug.Log("Couldn't find fret to activate");
        }
    }

    protected void DeactivateNote(int stringNum, int fretNum)
    {
        Fret fret = null;
        string key = GetKey(stringNum, fretNum);
        frets.TryGetValue(key, out fret);

        if (fret != null)
        {
            fret.SetActivated(false);
            fret.SetError(false);
        }
        else
        {
            Debug.Log("Couldn't find fret to deactivate");
        }
    }
}
