using System.Collections.Generic;
using UnityEngine;
using Melanchall.DryWetMidi.Multimedia;
using System;
using Melanchall.DryWetMidi.Core;
using System.Linq;

public class MidiManager : MonoBehaviour
{
    static int velocityThreshold = 80;
    private static IInputDevice _inputDevice;
    private static DateTime startTime;

    private static LinkedList<Note> notes;

    
    [SerializeField] FretBoard fretBoard;

    //GameObject[,] frets;
    [SerializeField] int stringCount;
    [SerializeField] int fretCountPerString;
    [SerializeField] string[] allowedNotes;

    [SerializeField] Dictionary<string,Fret> frets = new Dictionary<string, Fret>();

    // Start is called before the first frame update
    void Start()
    {
        fretBoard = FindObjectOfType<FretBoard>();
        PopulateFretsArray(fretBoard);
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

    private void PopulateFretsArray(FretBoard fretBoard){
        int stringNum = 1;
        int fretNum = 0;
        foreach (var fret in fretBoard.GetFrets())
        {
            string key = stringNum + "" + fretNum;
            Debug.Log("Adding key: " + key);
            frets.Add(key, fret.GetComponent<Fret>());
            fretNum = (fretNum + 1) % (fretCountPerString + 1);
            if (fretNum == 0) {
                stringNum++;
                fretNum = 0;
            }
        }
        Fret value;
        frets.TryGetValue("10", out value);
        Debug.Log(value.gameObject.name);
    }

    private void OnEventReceived(object sender, MidiEventReceivedEventArgs e)
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
            if (allowedNotes.Contains(noteName))
            {
                ActivateNote(stringNum, fret, false);
            } else
            {
                ActivateNote(stringNum, fret, true);
            }
            
        }

        else if (e.Event.EventType == MidiEventType.NoteOff) {
            note = RemoveNote(midi);
            note.SetEndTime((currentTime - startTime).TotalSeconds);
            DeactivateNote(note.StringNum, note.Fret);
        }

        if (note != null)
        {
            var outputString = $"Event received from '{midiDevice.Name}' at {DateTime.Now}: {e.Event}. " +
            $"Midi: {note.Midi}. Name: {note.Name}. String: {note.StringNum}. Fret: {note.Fret}. Velocity: {note.Velocity} " +
            $"StartTime: {note.StartTime}. EndTime: {note.EndTime}. Note Duration: {note.NoteDuration}";
            Debug.Log(outputString);
        }
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
        string key = stringNum + "" + fretNum;
        var status = frets.TryGetValue(key, out fret);
        Debug.Log("Note found: " + status);
        Debug.Log("Activate key:" + key);
        //Debug.Log(fret);
        
        //Debug.Log("Activating key: " + key);
        //Debug.Log("Fret activated name: " + fret.gamename);
        //Fret fretScript = (Fret)fret.gameObject.GetComponent("Fret");
        //Debug.Log(fretScript.ToString());
        if (fret != null)
        {
            if (!error)
            {
                fret.activated = true;
            }
            else
            {
                fret.error = true;
            }
        }
        else
        {
            //Debug.Log("Cant get fret script");
        }

        
    }

    private void DeactivateNote(int stringNum, int fretNum)
    {
        Fret fret = null;
        string key = stringNum + "" + fretNum;
        Debug.Log("Deactivate key:" + key);
        frets.TryGetValue(key, out fret);
        if (fret != null) {
            //Debug.Log("Deactivating key: " + fret.gameObject.name);
            fret.activated = false;
            fret.error = false;
        } else {
            //Debug.Log("Could not find fret");
        }
    }
}
