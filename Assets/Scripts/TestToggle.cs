using UnityEngine;
using UnityEngine.UI;

public class TestToggle : MonoBehaviour
{
    Toggle toggle;
    [SerializeField] GameObject Menu;
    
    // Start is called before the first frame update
    void Start()
    {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(SendMessage);
    }

    private void SendMessage(bool toggleValue)
    {
        Menu.SetActive(toggleValue);
    }

    void OnDisable()
    {
        if (toggle != null)
        {
            toggle.onValueChanged.RemoveListener(SendMessage);
        }
    }
}
