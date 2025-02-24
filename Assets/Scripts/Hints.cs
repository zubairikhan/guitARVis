using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hints : MonoBehaviour
{
    FretBoard fretBoard;
    ApplicationManager applicationManager;
    bool isOn;

    // Start is called before the first frame update
    void Start()
    {
        applicationManager = FindObjectOfType<ApplicationManager>();
        fretBoard = FindObjectOfType<FretBoard>();
    }

    public void ToggleHints()
    {
        isOn = !isOn;
        applicationManager.SetHintsEnabled(isOn);
        if (fretBoard != null)
        {
            fretBoard.ToggleHintsForAllowedNotes(isOn);
        }
    }

}
