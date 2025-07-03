using System;
using System.Collections.Generic;
using Melanchall.DryWetMidi.Multimedia;
using UnityEngine;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class MidiEventRecorder
{
    private IInputDevice _inputDevice;
    private List<TimedMidiEvent> _recordedEvents = new List<TimedMidiEvent>();
    private DateTime _recordingStartTime;
    //private bool _isRecording = false;
    private MidiManager _midiManager;

    public MidiEventRecorder(MidiManager midiManager)
    {
        _midiManager = midiManager;
    }

    private void Start()
    {
        Initialize("TriplePlay Connect");
    }
    public void Initialize(string deviceName)
    {
        _inputDevice = InputDevice.GetByName(deviceName);
        _inputDevice.EventReceived += OnMidiEventReceived;
        _inputDevice.StartEventsListening();
    }

    public void StartRecording()
    {
        _recordedEvents.Clear();
        _recordingStartTime = DateTime.Now;
        //_isRecording = true;
        //_inputDevice.StartEventsListening();
        Debug.Log("MIDI recording started");
    }

    public void StopRecording()
    {
        //_isRecording = false;
        //_inputDevice.StopEventsListening();
        Debug.Log($"MIDI recording stopped. Captured {_recordedEvents.Count} events");
    }

    private void OnMidiEventReceived(object sender, MidiEventReceivedEventArgs e)
    {
        if (!_midiManager.IsPlaying)
        {
            if (_midiManager.IsRecording)
            {
                var eventTime = DateTime.Now;
                var timeOffset = eventTime - _recordingStartTime;
                _recordedEvents.Add(new TimedMidiEvent(e.Event, timeOffset));
            }
            
            _midiManager.ProcessNotes(e.Event);

        }
    }

    public List<TimedMidiEvent> GetRecordedEvents()
    {
        return new List<TimedMidiEvent>(_recordedEvents);
    }

    public void ReleaseMidiInput()
    {
        (_inputDevice as IDisposable)?.Dispose();
    }
}