using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fret : MonoBehaviour
{
    [SerializeField] Color deactivatedColor = Color.black;
    [SerializeField] Color activatedColor = Color.white;
    Renderer objRenderer;
    // Start is called before the first frame update
    void Start()
    {
        objRenderer = this.gameObject.GetComponent<Renderer>();
        objRenderer.material.color = deactivatedColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate()
    {
        objRenderer.material.color = activatedColor;
    }

    public void Deactivate()
    {
        objRenderer.material.color = activatedColor;
    }
}
