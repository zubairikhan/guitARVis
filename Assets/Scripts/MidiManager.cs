using System.Collections.Generic;
using UnityEngine;
using Melanchall.DryWetMidi.Multimedia;
using System;
using Melanchall.DryWetMidi.Core;
using System.Linq;

public class MidiManager: MonoBehaviour
{
    [SerializeField] PlayMode[] fretBoardPlayModes;
    public bool IsRecording { get; private set; }
    public bool IsPlaying { get; private set; }
    
    //private IInputDevice _inputDevice;

    // Start is called before the first frame update
    //void Start()
    //{
    //    _inputDevice = InputDevice.GetByName("TriplePlay Connect");
    //    _inputDevice.EventReceived += OnEventReceived;
    //    _inputDevice.StartEventsListening();

    //    //(_inputDevice as IDisposable)?.Dispose();
    //}

    // Update is called once per frame
    //void Update()
    //{
    //    if(Input.GetKeyDown(KeyCode.Space))
    //    {
    //        (_inputDevice as IDisposable)?.Dispose();
    //    }
    //}

    //private void OnEventReceived(object sender, MidiEventReceivedEventArgs e)
    //{
    //    //if recording, save events to a list
    //    //if not recording, dont save to list

    //    //if playing, skip this next block
    //    //if not playing, run this block
    //    foreach(var playMode in fretBoardPlayModes)
    //    {
    //        playMode.Process(sender, e);
    //    }

    //}

    [SerializeField] private string _midiDeviceName = "TriplePlay Connect";

    private MidiEventRecorder _recorder;
    private MidiEventPlayer _player;

    void Start()
    {
        _recorder = new MidiEventRecorder(this);
        _player = GetComponent<MidiEventPlayer>();

        try
        {
            _recorder.Initialize(_midiDeviceName);
            Debug.Log($"Connected to MIDI device: {_midiDeviceName}");
        }
        catch (Exception e)
        {
            Debug.LogError($"MIDI initialization failed: {e.Message}");
        }
    }
    
    public void StartRecording()
    {
        if (IsPlaying)
        {
            Debug.Log("Playback in progress. Stop playback to record");
            return;
        }

        IsRecording = true;
        _recorder.StartRecording();
    }

    public void StopRecording()
    {
        IsRecording = false;
        _recorder.StopRecording();
    }

    public void PlayBackRecording()
    {
        if (IsRecording)
        {
            Debug.Log("Recording in progress. Stop recording to play back");
            return;
        }
        

        var recordedEvents = _recorder.GetRecordedEvents();
        if (recordedEvents.Count > 0)
        {
            Debug.Log($"Playing back {recordedEvents.Count} events...");
            IsPlaying = true;
            _player.PlayEvents(recordedEvents);
        }
        else
        {
            Debug.Log("No events to play back");
        }
    }

    public void StopPlayBackRecording()
    {
        Debug.Log("Playback stopped");
        IsPlaying = false;
        _player.StopEvents();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) // Start recording
        {
            StartRecording();
        }

        if (Input.GetKeyDown(KeyCode.S)) // Stop recording
        {
            StopRecording();
        }

        if (Input.GetKeyDown(KeyCode.P)) // Play back recorded events
        {
            PlayBackRecording();
        }

        if (Input.GetKeyDown(KeyCode.O)) // Stop Play back recorded events
        {
            StopPlayBackRecording();
        }
    }

    void OnDestroy()
    {
        ReleaseMidiInput();
    }

    public void ReleaseMidiInput()
    {
        _recorder?.ReleaseMidiInput();
    }

    public void ProcessNotes(MidiEvent e)
    {
        foreach (var playMode in fretBoardPlayModes)
        {
            playMode.Process(e);
        }
    }

}
