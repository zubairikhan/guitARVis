using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L1Menu : MonoBehaviour
{

    [SerializeField] public GameObject L2MenuModes;
    [SerializeField] public GameObject L2MenuScales;
    private bool isL2MenuModesEnabled;
    private bool isL2MenuScalesEnabled;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleL2MenuModes()
    {
        isL2MenuModesEnabled = !isL2MenuModesEnabled;
        if (isL2MenuModesEnabled && isL2MenuScalesEnabled)
        {
            ToggleL2MenuScales();
        }
        L2MenuModes.SetActive(isL2MenuModesEnabled);
    }
    public void ToggleL2MenuScales()
    {
        isL2MenuScalesEnabled = !isL2MenuScalesEnabled;
        if (isL2MenuScalesEnabled && isL2MenuModesEnabled)
        {
            ToggleL2MenuModes();
        }
        L2MenuScales.SetActive(isL2MenuScalesEnabled);
    }
}
