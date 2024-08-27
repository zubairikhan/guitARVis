using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Multimedia;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayMode : MonoBehaviour, IProcess
{
    [SerializeField] FretBoard fretBoard;
    
    [SerializeField] protected string[] allowedNotes;
    [SerializeField] int velocityThreshold = 80;
    [SerializeField] protected bool showErrors;

    protected DateTime startTime;
    protected LinkedList<Note> notes;
    protected Dictionary<string, Fret> frets = new Dictionary<string, Fret>();

    // Start is called before the first frame update
    protected void Start()
    {
        Debug.Log("Parent plymode executred");
        startTime = DateTime.Now;
        notes = new LinkedList<Note>();
        fretBoard = FindObjectOfType<FretBoard>();
        PopulateFretsArray(fretBoard);
    }

    private void PopulateFretsArray(FretBoard fretBoard)
    {
        int stringNum = 1;
        int fretNum = 0;
        foreach (var fret in fretBoard.GetFrets())
        {
            string key = GetKey(stringNum, fretNum);
            frets.Add(key, fret.GetComponent<Fret>());
            fretNum = (fretNum + 1) % (fretBoard.GetFretCountPerString() + 1);
            if (fretNum == 0)
            {
                stringNum++;
                fretNum = 0;
            }
        }
    }

    protected bool IsNoteStopped(MidiEventReceivedEventArgs e)
    {
        return e.Event.EventType == MidiEventType.NoteOff;
    }

    protected bool IsNotePlayed(MidiEventReceivedEventArgs e)
    {
        return e.Event.EventType == MidiEventType.NoteOn && ((NoteEvent)e.Event).Velocity > velocityThreshold;
    }
    protected void LogNote(MidiEventReceivedEventArgs e, MidiDevice midiDevice, Note note)
    {
        if (note != null)
        {
            var outputString = $"Event received from '{midiDevice.Name}' at {DateTime.Now}: {e.Event}. " +
            $"Midi: {note.Midi}. Name: {note.NoteName}. String: {note.StringNum}. Fret: {note.Fret}. Velocity: {note.Velocity} " +
            $"StartTime: {note.StartTime}. EndTime: {note.EndTime}. Note Duration: {note.NoteDuration}";
            Debug.Log(outputString);
        }
    }

    protected Note ComputeNoteProperties(MidiEventReceivedEventArgs e, DateTime currentTime)
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

    protected void ActivateNote(int stringNum, int fretNum, bool error)
    {
        string key = GetKey(stringNum, fretNum);

        if (frets.TryGetValue(key, out var fret))
        {
            fret.SetActivated(true);
            fret.SetError(error);
        }
        else
        {
            Debug.Log("Couldn't find fret to activate");
        }
    }

    protected Note RemoveNote(int midi)
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

    protected string GetKey(int stringNum, int fretNum)
    {
        return stringNum + "" + fretNum;
    }

    public void SetAllowedNotes(string[] allowedNotes)
    {
        this.allowedNotes = allowedNotes;
    }

    public virtual void Process(object sender, MidiEventReceivedEventArgs e) { }
}

interface IProcess
{
    void Process(object sender, MidiEventReceivedEventArgs e);
}
