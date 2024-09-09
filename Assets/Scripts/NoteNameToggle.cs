using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteNameToggle : MonoBehaviour
{
    Toggle toggle;
    FretBoard fretBoard;

    // Start is called before the first frame update
    void Start()
    {
        toggle = GetComponent<Toggle>();
        fretBoard = FindObjectOfType<FretBoard>();
        toggle.onValueChanged.AddListener(OnToggleChanged);
    }

    void OnToggleChanged(bool isOn)
    {
        if (fretBoard != null)
        {
            fretBoard.ToggleNoteNameOnFrets(isOn);
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
