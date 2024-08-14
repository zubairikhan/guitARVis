using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fret : MonoBehaviour
{
    [SerializeField] Color deactivatedColor = Color.black;
    [SerializeField] Color activatedColor = Color.white;
    [SerializeField] Color errorColor = Color.red;
    MeshRenderer objRenderer;
    public bool activated;
    public bool error;
    // Start is called before the first frame update
    void Start()
    {
        objRenderer = this.gameObject.GetComponent<MeshRenderer>();
        objRenderer.material.color = deactivatedColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            objRenderer.material.color = activatedColor;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            objRenderer.material.color = deactivatedColor;
        }

        if (activated)
        {
            Activate();
            //Invoke("Deactivate", 1f);
        }
        else if (error)
        {
            ActivateError();
        }
        else
        {
            Deactivate();
        }
    }

    public void Activate()
    {
        //Debug.Log("Activating inside fret");
        objRenderer.material.color = activatedColor;
    }

    public void Deactivate()
    {
        //Debug.Log("Dectivating inside fret");
        //Debug.Log("Deativating" + this.gameObject.name);
        objRenderer.material.color = deactivatedColor;
    }

    private void ActivateError()
    {
        objRenderer.material.color = errorColor;
    }

    
}
