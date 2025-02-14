using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L2Menu : MonoBehaviour
{
    [SerializeField] public GameObject L3Menu;
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
        L3Menu.SetActive(toggleValue);
    }
}
