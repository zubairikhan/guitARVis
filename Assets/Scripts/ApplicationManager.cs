using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplicationManager : MonoBehaviour
{
    GameMode currentPlayMode;
    FretBoard fretBoard;
    MidiManager midiManager;
    // Start is called before the first frame update
    void Start()
    {
        midiManager = FindObjectOfType<MidiManager>();
        fretBoard = FindObjectOfType<FretBoard>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        { 
            SetCurrentPlayModeAsScales();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetCurrentPlayModeAsHeatmap();
        }
    }

    public GameMode GetCurrentPlayMode() { return currentPlayMode; }

    public void SetCurrentPlayModeAsScales ()
    {
        fretBoard.ResetFretBoard();
        currentPlayMode = GameMode.Scales;
    }

    public void SetCurrentPlayModeAsHeatmap()
    {
        fretBoard.ResetFretBoard();
        currentPlayMode = GameMode.Heatmap;
    }

    public void LoadScene(string sceneName)
    {
        midiManager.ReleaseMidiInput();
        SceneManager.LoadScene(sceneName);
    }
}

public enum GameMode
{
    Scales,
    Heatmap
}
