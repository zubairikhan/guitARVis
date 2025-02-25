using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftControllerMenu : MonoBehaviour
{
    [SerializeField] GameObject alignMenu;
    bool isAlignMenuEnabled;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleAlignMenu()
    {
        isAlignMenuEnabled = !isAlignMenuEnabled;
        alignMenu.SetActive(isAlignMenuEnabled);
    }
}
