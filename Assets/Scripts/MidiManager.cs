using System.Collections.Generic;
using UnityEngine;
using Melanchall.DryWetMidi.Multimedia;
using System;
using Melanchall.DryWetMidi.Core;

public class MidiManager : MonoBehaviour
{
    static int velocityThreshold = 80;
    private static IInputDevice _inputDevice;
    private static DateTime startTime;

    private static LinkedList<Note> notes;

    // Start is called before the first frame update
    void Start()
    {
        notes = new LinkedList<Note>();
        startTime = DateTime.Now;
        _inputDevice = InputDevice.GetByName("TriplePlay Connect");
        _inputDevice.EventReceived += OnEventReceived;
        _inputDevice.StartEventsListening();

        //(_inputDevice as IDisposable)?.Dispose();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            (_inputDevice as IDisposable)?.Dispose();
        }
    }

    private static void OnEventReceived(object sender, MidiEventReceivedEventArgs e)
    {
        var velocity = ((NoteEvent)e.Event).Velocity;
        var midiDevice = (MidiDevice)sender;
        var midi = ((NoteEvent)e.Event).NoteNumber;
        var currentTime = DateTime.Now;
        Note note = null;

        if (e.Event.EventType == MidiEventType.NoteOn && velocity > velocityThreshold) {
            
            var channel = ((ChannelEvent)e.Event).Channel;
            var stringNum = channel + 1;
            var noteName  = Helper.noteNames[midi % 12];
            var noteStartTime = (currentTime - startTime).TotalSeconds;
            var fret = midi - Helper.tuningPitchEStd[channel];
            
            note = new Note(midi, noteName, stringNum, fret, velocity, noteStartTime, currentTime);
            notes.AddLast(note);
        }

        else if (e.Event.EventType == MidiEventType.NoteOff) {
            note = RemoveNote(midi);
            note.SetEndTime((currentTime - startTime).TotalSeconds);
        }

        if (note != null)
        {
            var outputString = $"Event received from '{midiDevice.Name}' at {DateTime.Now}: {e.Event}. " +
            $"Midi: {note.Midi}. Name: {note.Name}. String: {note.StringNum}. Fret: {note.Fret}. Velocity: {note.Velocity} " +
            $"StartTime: {note.StartTime}. EndTime: {note.EndTime}. Note Duration: {note.NoteDuration}";
            Debug.Log(outputString);
        }
    }

    static Note RemoveNote(int midi)
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
}
