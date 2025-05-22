using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteNameToggle : MonoBehaviour
{
    Toggle toggle;
    FretBoard[] fretBoards;
    ApplicationManager applicationManager;

    // Start is called before the first frame update
    void Start()
    {
        applicationManager = FindObjectOfType<ApplicationManager>();
        toggle = GetComponent<Toggle>();
        fretBoards = FindObjectsOfType<FretBoard>();
        toggle.onValueChanged.AddListener(OnToggleChanged);
    }

    void OnToggleChanged(bool isOn)
    {
        Debug.Log("Toggle val: " + isOn);
        applicationManager.SetHintsEnabled(isOn);
        if (fretBoards != null)
        {
            foreach (var fretBoard in fretBoards)
            {
                fretBoard.ToggleHintsForAllowedNotes(isOn);
            }
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
