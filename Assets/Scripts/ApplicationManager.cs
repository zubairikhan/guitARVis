using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplicationManager : MonoBehaviour
{
    MidiManager midiManager;
    PlayMode playMode;
    bool hintsEnabled = true;
    GameObject fretboardStation;
    // Start is called before the first frame update
    void Start()
    {
        midiManager = FindObjectOfType<MidiManager>();
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

    public void SetFretBoardStation(GameObject gameObject)
    {
        fretboardStation = gameObject;
    }

    public void MoveFretBoardStation(MoveDirection moveDirection)
    {
        if (fretboardStation != null)
            fretboardStation.GetComponent<FretBoardStation>().MoveFretBoard(moveDirection);
    }
}
