using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    [SerializeField] Camera centerEyeAnchor;
    [SerializeField] float distanceInfrontOfCamera = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SetPositionRelativeToCamera());
        
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.position = centerEyeAnchor.transform.position + centerEyeAnchor.transform.forward * distanceInfrontOfCamera;
    }

    IEnumerator SetPositionRelativeToCamera()
    {
        yield return new WaitForEndOfFrame();

        this.transform.position = centerEyeAnchor.transform.position + centerEyeAnchor.transform.forward * distanceInfrontOfCamera;
    }
}
