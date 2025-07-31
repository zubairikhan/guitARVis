using UnityEngine;
using System.Linq;

public class SelectNotesPanel : MonoBehaviour
{
    [SerializeField] SelectNotesChildPanel selectNotesChildPanel;
    
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
