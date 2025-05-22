using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hints : MonoBehaviour
{
    FretBoard[] fretBoards;
    ApplicationManager applicationManager;
    bool isOn;

    // Start is called before the first frame update
    void Start()
    {
        applicationManager = FindObjectOfType<ApplicationManager>();
        fretBoards = FindObjectsOfType<FretBoard>();
    }

    public void ToggleHints()
    {
        isOn = !isOn;
        applicationManager.SetHintsEnabled(isOn);
        if (fretBoards != null)
        {
            foreach(FretBoard fretboard in fretBoards)
            {
                fretboard.ToggleHintsForAllowedNotes(isOn);
            }
        }
    }

}
