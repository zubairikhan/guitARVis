using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L1Menu : MonoBehaviour
{

    [SerializeField] public GameObject L2Menu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Toggle(bool toggleValue)
    {
        Debug.Log("Toggle Val: " + toggleValue);
        L2Menu.SetActive(toggleValue);
    }
}
