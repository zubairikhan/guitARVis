using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L3Menu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleEnable(bool enable)
    {
        this.gameObject.SetActive(enable);
    }

    public void Disable()
    {
        this.gameObject.SetActive(false);
    }
}
