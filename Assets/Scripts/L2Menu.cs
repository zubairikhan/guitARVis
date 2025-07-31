using UnityEngine;

public class L2Menu : MonoBehaviour
{
    [SerializeField] public GameObject L3Menu;
    public void Toggle(bool toggleValue)
    {
        L3Menu.SetActive(toggleValue);
    }
}
