using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Melanchall.DryWetMidi.Multimedia;
using System.Linq;
using System;
using Melanchall.DryWetMidi.Core;

public class MidiManager : MonoBehaviour
{
    private static IInputDevice _inputDevice;
    // Start is called before the first frame update
    void Start()
    {
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
        var midiDevice = (MidiDevice)sender;
        Debug.Log($"Event received from '{midiDevice.Name}' at {DateTime.Now}: {e.Event}. Channel {((ChannelEvent)e.Event).Channel}. Note {((NoteEvent)e.Event).NoteNumber}");
        
    }
}
