using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplicationManager : MonoBehaviour
{
    FretBoard fretBoard;
    MidiManager midiManager;
    PlayMode playMode;
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
}
