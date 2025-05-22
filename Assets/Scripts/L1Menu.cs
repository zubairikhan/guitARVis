using OVRSimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L1Menu : MonoBehaviour
{

    [SerializeField] public GameObject L2MenuModes;
    [SerializeField] public GameObject L2MenuScales;
    [SerializeField] public Button LetThereBeFretsBtn;
    private bool isL2MenuModesEnabled;
    private bool isL2MenuScalesEnabled;
    FretBoard[] fretBoards;
    ControllerInput controllerInput;
    ApplicationManager applicationManager;
    bool isOn;
    // Start is called before the first frame update
    void Start()
    {
        applicationManager = FindObjectOfType<ApplicationManager>();
        fretBoards = FindObjectsOfType<FretBoard>();
        controllerInput = FindObjectOfType<ControllerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleL2MenuModes()
    {
        isL2MenuModesEnabled = !isL2MenuModesEnabled;
        if (isL2MenuModesEnabled && isL2MenuScalesEnabled)
        {
            ToggleL2MenuScales();
        }
        L2MenuModes.SetActive(isL2MenuModesEnabled);
    }
    public void ToggleL2MenuScales()
    {
        isL2MenuScalesEnabled = !isL2MenuScalesEnabled;
        if (isL2MenuScalesEnabled && isL2MenuModesEnabled)
        {
            ToggleL2MenuModes();
        }
        L2MenuScales.SetActive(isL2MenuScalesEnabled);
    }

    public void ToggleHints()
    {
        isOn = !isOn;
        applicationManager.SetHintsEnabled(isOn);
        if (fretBoards != null)
        {
            foreach (var fretBoard in fretBoards)
            {
                fretBoard.ToggleHintsForAllowedNotes(isOn);
            }
        }
    }

    public void ExpandFretBoard()
    {
        LetThereBeFretsBtn.interactable = false;
        foreach (var fretBoard in fretBoards)
        {
            fretBoard.SetupFretPositionsV2();
        }
    }

    public void ResetFretBoard()
    {
        foreach (var fretBoard in fretBoards)
        {
            fretBoard.ResetFretBoard();
        }
    }

    public void DeleteBoards()
    {
        controllerInput.DeleteBoards();
    }
}
