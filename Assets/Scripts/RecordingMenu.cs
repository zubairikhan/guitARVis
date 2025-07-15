using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordingMenu : MonoBehaviour
{
    [SerializeField] MidiManager midiManager;
    public void StartRecording()
    {
        midiManager.StartRecording();
    }

    public void StopRecording()
    {
        midiManager.StopRecording();
    }

    public void StartPlayback()
    {
        midiManager.PlayBackRecording();
    }

    public void StopPlayback() 
    {
        midiManager.StopPlayBackRecording();
    }
}
