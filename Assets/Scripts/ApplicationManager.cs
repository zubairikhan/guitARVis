using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationManager : MonoBehaviour
{
    PlayMode currentPlayMode;
    FretBoard fretBoard;
    // Start is called before the first frame update
    void Start()
    {
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

    public PlayMode GetCurrentPlayMode() { return currentPlayMode; }

    public void SetCurrentPlayModeAsScales ()
    {
        fretBoard.ResetFretBoard();
        currentPlayMode = PlayMode.Scales;
    }

    public void SetCurrentPlayModeAsHeatmap()
    {
        fretBoard.ResetFretBoard();
        currentPlayMode = PlayMode.Heatmap;
    }


    public enum PlayMode
    { 
        Scales,
        Heatmap
    }
}
