using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplicationManager : MonoBehaviour
{
    FretBoard fretBoard;
    MidiManager midiManager;
    PlayMode playMode;
    bool hintsEnabled = true;
    // Start is called before the first frame update
    void Start()
    {
        midiManager = FindObjectOfType<MidiManager>();
        fretBoard = FindObjectOfType<FretBoard>();
        playMode = FindObjectOfType<PlayMode>();
    }

    public Type GetCurrentPlayMode() { return playMode.GetType(); }

    public void LoadScene(string sceneName)
    {
        midiManager.ReleaseMidiInput();
        SceneManager.LoadScene(sceneName);
    }

    void OnApplicationQuit()
    {
        midiManager.ReleaseMidiInput();
    }

    public bool IsHintsEnabled() { return hintsEnabled; }

    public void SetHintsEnabled(bool status) { hintsEnabled = status; }
}
