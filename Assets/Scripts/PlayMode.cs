using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.Multimedia;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayMode : MonoBehaviour, IProcess
{
    [SerializeField] FretBoard fretBoard;
    StatisticsPanel statisticsPanel;
    [SerializeField] public string[] AllowedNotes { get; private set;} //notes that are in the scale chosen to play
    [SerializeField] int velocityThreshold = 80;  //notes played with lower velocity are not considered played.
    [SerializeField] protected bool showErrors;
    string selectedScale; //starting default scale
    protected DateTime startTime;
    protected LinkedList<Note> notes;  //list of currently active note events
    protected Dictionary<string, Fret> frets = new Dictionary<string, Fret>();
    int countCorrectNotes = 0;
    int countIncorrectNotes = 0;

    // Start is called before the first frame update
    protected void Start()
    {
        selectedScale = "E Major";
        startTime = DateTime.Now;
        notes = new LinkedList<Note>();
        //fretBoard = GetComponent<FretBoard>();
        statisticsPanel = FindObjectOfType<StatisticsPanel>();
        PopulateFretsArray(fretBoard);
        ChangeNotesToPractice(selectedScale);
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

    public void ActivateScaleOnFretBoard(string[] allowedNotes)
    {
        foreach (var fret in fretBoard.GetFrets())
        {
            Fret fretScript = fret.GetComponent<Fret>();
            if (allowedNotes.Contains(fretScript.Note))
            {
                fretScript.SwitchOnScale();
            }
        }
    }

    public void ChangeNotesToPractice(string itemName)
    {
        if (Helper.allowedNotes.TryGetValue(itemName, out var notes))
        {
            SetAllowedNotes(notes);
            ResetFretBoard();
            ResetStatistics();
            ActivateScaleOnFretBoard(notes);
            selectedScale = itemName;
        }
    }

    protected bool IsNoteStopped(MidiEvent e)
    {
        return e.EventType == MidiEventType.NoteOff;
    }

    protected bool IsNotePlayed(MidiEvent e)
    {
        return e.EventType == MidiEventType.NoteOn && ((NoteEvent)e).Velocity > velocityThreshold;
    }
    protected void LogNote(MidiEvent e, Note note)
    {
        if (note != null)
        {
            var outputString = $"Event received at {DateTime.Now}: {e}. " +
            $"Midi: {note.Midi}. Name: {note.NoteName}. String: {note.StringNum}. Fret: {note.Fret}. Velocity: {note.Velocity} " +
            $"StartTime: {note.StartTime}. EndTime: {note.EndTime}. Note Duration: {note.NoteDuration}";
            Debug.Log(outputString);
        }
    }

    protected Note ComputeNoteProperties(MidiEvent e, DateTime currentTime)
    {
        var midi = ((NoteEvent)e).NoteNumber;
        var channel = ((ChannelEvent)e).Channel;
        var stringNum = channel + 1;
        var noteName = Helper.noteNames[midi % 12];
        var noteStartTime = (currentTime - startTime).TotalSeconds;
        var fret = midi - Helper.tuningPitchEStd[channel];
        var velocity = ((NoteEvent)e).Velocity;

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

    private void SetAllowedNotes(string[] allowedNotes)
    {
        this.AllowedNotes = allowedNotes;
    }

    public void ResetFretBoard()
    {
        fretBoard.ResetFretBoard();
    }

    protected bool IsCorrectNotePlayed(string noteName)
    {
        bool isCorrectNotePlayed = AllowedNotes.Contains(noteName);
        UpdateStatistics(isCorrectNotePlayed);

        return isCorrectNotePlayed;
    }

    private void UpdateStatistics(bool isCorrectNotePlayed)
    {
        if (isCorrectNotePlayed)
        {
            countCorrectNotes++;
        }
        else
        {
            countIncorrectNotes++;
        }
    }

    public void ResetStatistics()
    {
        countCorrectNotes = 0;
        countIncorrectNotes = 0;
    }

    public (float, float) GetStatistics()
    {
        return (countCorrectNotes, countIncorrectNotes);
    }

    public virtual void Process(MidiEvent e) { }
}

interface IProcess
{
    void Process(MidiEvent e);
}
