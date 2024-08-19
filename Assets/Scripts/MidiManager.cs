using System.Collections.Generic;
using UnityEngine;
using Melanchall.DryWetMidi.Multimedia;
using System;
using Melanchall.DryWetMidi.Core;
using System.Linq;

public class MidiManager : MonoBehaviour
{
    [SerializeField] PlayMode playMode;
    private IInputDevice _inputDevice;
    
    // Start is called before the first frame update
    void Start()
    {
        playMode = FindObjectOfType<PlayMode>();
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

    private void OnEventReceived(object sender, MidiEventReceivedEventArgs e)
    {
        playMode.ProcessEvent(sender, e);
    }

    

    
}
