using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class SelectNotesPanel : MonoBehaviour
{
    [SerializeField] SelectNotesChildPanel selectNotesChildPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Enable()
    {
        this.gameObject.SetActive(true);
    }

    public void Disable()
    {
        this.gameObject.SetActive(false);
    }

    public void EnableChildPanelForSingleNotes()
    {
        selectNotesChildPanel.SetMenuItems(Helper.naturalNotes.ToList());
        selectNotesChildPanel.gameObject.SetActive(true);
        Disable();
    }

    public void EnableChildPanelForScales()
    {
        selectNotesChildPanel.SetMenuItems(Helper.scales.ToList());
        selectNotesChildPanel.gameObject.SetActive(true);
        Disable();
    }
}
