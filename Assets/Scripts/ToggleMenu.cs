using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleMenu : MonoBehaviour
{
    Toggle toggle;
    [SerializeField] GameObject Menu;

    // Start is called before the first frame update
    void Start()
    {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(OnToggleChanged);
    }

    void OnToggleChanged(bool isOn)
    {
        Debug.Log("Toggle: " + isOn);
        Menu.SetActive(isOn);
    }

    void OnDisable()
    {
        if (toggle != null)
        {
            toggle.onValueChanged.RemoveListener(OnToggleChanged);
        }
    }
}
