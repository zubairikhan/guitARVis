using OVRSimpleJSON;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestToggle : MonoBehaviour
{
    Toggle toggle;
    [SerializeField] GameObject Menu;
    //[SerializeField] private TMP_Text textfield;
    //[SerializeField] private string message;

    // Start is called before the first frame update
    void Start()
    {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(SendMessage);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SendMessage(bool toggleValue)
    {
        Debug.Log("Toggle valu: " + toggleValue);
        Menu.SetActive(toggleValue);
        //if (toggleValue)
        //{
        //    textfield.SetText(message);
        //}
        //else
        //{
        //    textfield.SetText("nothing set");
        //}
    }

    void OnDisable()
    {
        if (toggle != null)
        {
            toggle.onValueChanged.RemoveListener(SendMessage);
        }
    }
}
