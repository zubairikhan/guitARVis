using UnityEngine;

public class LeftControllerMenu : MonoBehaviour
{
    [SerializeField] GameObject alignMenu;
    bool isAlignMenuEnabled;
    public void ToggleAlignMenu()
    {
        isAlignMenuEnabled = !isAlignMenuEnabled;
        alignMenu.SetActive(isAlignMenuEnabled);
    }
}
