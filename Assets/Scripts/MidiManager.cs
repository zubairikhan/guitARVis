using System.Collections.Generic;
using UnityEngine;
using Melanchall.DryWetMidi.Multimedia;
using System;
using Melanchall.DryWetMidi.Core;
using System.Linq;

public class MidiManager : MonoBehaviour
{
    [SerializeField] PlayMode playMode;
    ApplicationManager manager;
    private IInputDevice _inputDevice;
    
    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<ApplicationManager>();
        //playMode = FindObjectOfType<PlayMode>();
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
        if (manager.GetCurrentPlayMode() == GameMode.Scales)
        {
            playMode.Process(sender, e);
        }
        if (manager.GetCurrentPlayMode() == GameMode.Heatmap)
        {
            playMode.Process(sender, e);
        }

    }

    public void ReleaseMidiInput()
    {
        (_inputDevice as IDisposable)?.Dispose();
    }
    

    
}
