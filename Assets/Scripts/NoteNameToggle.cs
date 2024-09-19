using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteNameToggle : MonoBehaviour
{
    Toggle toggle;
    FretBoard fretBoard;
    ApplicationManager applicationManager;

    // Start is called before the first frame update
    void Start()
    {
        applicationManager = FindObjectOfType<ApplicationManager>();
        toggle = GetComponent<Toggle>();
        fretBoard = FindObjectOfType<FretBoard>();
        toggle.onValueChanged.AddListener(OnToggleChanged);
    }

    void OnToggleChanged(bool isOn)
    {
        applicationManager.SetHintsEnabled(isOn);
        if (fretBoard != null)
        {
            fretBoard.ToggleHintsForAllowedNotes(isOn);
        }
    }

    void OnDestroy()
    {
        if (toggle != null)
        {
            toggle.onValueChanged.RemoveListener(OnToggleChanged);
        }
    }
}
