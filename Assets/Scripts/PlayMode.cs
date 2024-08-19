using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Multimedia;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayMode : MonoBehaviour
{
    [SerializeField] FretBoard fretBoard;
    [SerializeField] int stringCount;
    [SerializeField] int fretCountPerString;
    [SerializeField] string[] allowedNotes;
    [SerializeField] int velocityThreshold = 80;

    private DateTime startTime;
    private LinkedList<Note> notes;
    private Dictionary<string, Fret> frets = new Dictionary<string, Fret>();

    // Start is called before the first frame update
    void Start()
    {
        startTime = DateTime.Now;
        notes = new LinkedList<Note>();
        fretBoard = FindObjectOfType<FretBoard>();
        PopulateFretsArray(fretBoard);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PopulateFretsArray(FretBoard fretBoard)
    {
        int stringNum = 1;
        int fretNum = 0;
        foreach (var fret in fretBoard.GetFrets())
        {
            string key = GetKey(stringNum, fretNum);
            frets.Add(key, fret.GetComponent<Fret>());
            fretNum = (fretNum + 1) % (fretCountPerString + 1);
            if (fretNum == 0)
            {
                stringNum++;
                fretNum = 0;
            }
        }
    }

    public void ProcessEvent(object sender, MidiEventReceivedEventArgs e)
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

    private bool IsNoteStopped(MidiEventReceivedEventArgs e)
    {
        return e.Event.EventType == MidiEventType.NoteOff;
    }

    private bool IsNotePlayed(MidiEventReceivedEventArgs e)
    {
        return e.Event.EventType == MidiEventType.NoteOn && ((NoteEvent)e.Event).Velocity > velocityThreshold;
    }
    private void LogNote(MidiEventReceivedEventArgs e, MidiDevice midiDevice, Note note)
    {
        if (note != null)
        {
            var outputString = $"Event received from '{midiDevice.Name}' at {DateTime.Now}: {e.Event}. " +
            $"Midi: {note.Midi}. Name: {note.NoteName}. String: {note.StringNum}. Fret: {note.Fret}. Velocity: {note.Velocity} " +
            $"StartTime: {note.StartTime}. EndTime: {note.EndTime}. Note Duration: {note.NoteDuration}";
            Debug.Log(outputString);
        }
    }

    private Note ComputeNoteProperties(MidiEventReceivedEventArgs e, DateTime currentTime)
    {
        var midi = ((NoteEvent)e.Event).NoteNumber;
        var channel = ((ChannelEvent)e.Event).Channel;
        var stringNum = channel + 1;
        var noteName = Helper.noteNames[midi % 12];
        var noteStartTime = (currentTime - startTime).TotalSeconds;
        var fret = midi - Helper.tuningPitchEStd[channel];
        var velocity = ((NoteEvent)e.Event).Velocity;

        return new Note(midi, noteName, stringNum, fret, velocity, noteStartTime, currentTime);
    }



    private Note RemoveNote(int midi)
    {
        var current = notes.First;
        while (current != null)
        {
            var next = current.Next;
            if (current.Value.Midi == midi)
            {
                notes.Remove(current);
                return current.Value;
            }
            current = next;
        }
        return null;
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

    private void DeactivateNote(int stringNum, int fretNum)
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

    private string GetKey(int stringNum, int fretNum)
    {
        return stringNum + "" + fretNum;
    }
}
