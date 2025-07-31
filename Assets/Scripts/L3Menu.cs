using UnityEngine;

public class L3Menu : MonoBehaviour
{
    public void ToggleEnable(bool enable)
    {
        this.gameObject.SetActive(enable);
    }

    public void Disable()
    {
        this.gameObject.SetActive(false);
    }
}
