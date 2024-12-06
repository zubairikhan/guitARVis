using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class ControllerInput : MonoBehaviour
{
    [SerializeField] GameObject testPoint;
    [SerializeField] Vector3 tipOffset = new Vector3(0, 0, 0.1f);
    [SerializeField] Transform controller;
    [SerializeField] float gizmosSize = 0.005f;

    [SerializeField] GameObject boardPlane;
    [SerializeField] float zStretch = 0.0554f;
    [SerializeField] float yStretch = 0.005f;
    private Vector3 startPoint;
    private Vector3 endPoint;
    private bool isStartPointSet = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // A-button right controller
        if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch))
        {
            startPoint = GetControllerTipPosition(OVRInput.Controller.RTouch);
            isStartPointSet = true;
            Debug.Log("Start point set: " + startPoint);
        }

        // B-button left controller
        if (OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.RTouch) && isStartPointSet)
        {
            endPoint = GetControllerTipPosition(OVRInput .Controller.RTouch);
            Debug.Log("End point set: " + endPoint);
            CreatePlaneBetweenPoints(startPoint, endPoint);
        }
    }

    private Vector3 GetControllerTipPosition(OVRInput.Controller ovrController)
    {
        Vector3 ovrPos = OVRInput.GetLocalControllerPosition(ovrController);
        Vector3 pos = controller.position;

        Debug.Log("PosOVRInput: " + ovrPos + ", PosTransform: " + pos);
        Vector3 tipPos = pos + controller.TransformDirection(tipOffset);
        Debug.Log("Controller tip at: " + tipOffset);
        Instantiate(testPoint, tipPos, controller.rotation);
        return tipPos;
    }

    void CreatePlaneBetweenPoints(Vector3 startPoint, Vector3 endPoint)
    {
        Vector3 midpoint = (startPoint + endPoint) / 2;
        float distance = Vector3.Distance(startPoint, endPoint);
        Vector3 scale = new Vector3(distance, yStretch, zStretch);

        Quaternion rotation1 = Quaternion.LookRotation(endPoint - startPoint);
        
        //Quaternion rot2 = Quaternion.Euler(90f, 0 , 0);

        GameObject plane = Instantiate(boardPlane, midpoint, rotation1);
        plane.transform.localScale = scale;
        
        Debug.Log("PLane instantiated");

    }

    private void OnDrawGizmos()
    {
        if (controller) { 
            Vector3 tipPos = controller.position + controller.TransformDirection(tipOffset);
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(tipPos, gizmosSize);
        }
    }
}
