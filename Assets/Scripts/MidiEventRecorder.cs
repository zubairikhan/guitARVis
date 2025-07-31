using System;
using System.Collections.Generic;
using Melanchall.DryWetMidi.Multimedia;
using UnityEngine;

public class MidiEventRecorder
{
    private IInputDevice _inputDevice;
    private List<TimedMidiEvent> _recordedEvents = new List<TimedMidiEvent>();
    private DateTime _recordingStartTime;
    private MidiManager _midiManager;
    private MeshRenderer _recordingLightMeshRenderer;

    public MidiEventRecorder(MidiManager midiManager, GameObject recordingLight)
    {
        _midiManager = midiManager;
        _recordingLightMeshRenderer = recordingLight.GetComponent<MeshRenderer>();
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
        Debug.Log("MIDI recording started");
        ChangeRecordingLight(Color.red);
    }

    public void StopRecording()
    {
        Debug.Log($"MIDI recording stopped. Captured {_recordedEvents.Count} events");
        ChangeRecordingLight(Color.white);
    }

    private void ChangeRecordingLight(Color color)
    {
        _recordingLightMeshRenderer.material.color = color;
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