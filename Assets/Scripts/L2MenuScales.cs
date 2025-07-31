using System.Linq;
using UnityEngine;

public class L2MenuScales : MonoBehaviour
{
    [SerializeField] public L3MenuScalesList scalesList;
    [SerializeField] public GameObject L3Menu;
    private bool isScalesMenuEnabled;
    private bool isNotesMenuEnabled;

    public void EnableChildPanelForSingleNotes()
    {
        isNotesMenuEnabled = !isNotesMenuEnabled;
        bool enable = isNotesMenuEnabled || isScalesMenuEnabled;
        L3Menu.SetActive(enable);
        
        if (enable)
        {
            isScalesMenuEnabled = false;
            scalesList.SetMenuItems(Helper.naturalNotes.ToList());
        }
        
    }

    public void EnableChildPanelForScales()
    {
        isScalesMenuEnabled = !isScalesMenuEnabled;
        bool enable = isNotesMenuEnabled || isScalesMenuEnabled;
        L3Menu.SetActive(enable);

        if (enable)
        {
            isNotesMenuEnabled = false;
            scalesList.SetMenuItems(Helper.scales.ToList());
        }
    }
}
