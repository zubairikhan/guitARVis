using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class ControllerInput : MonoBehaviour
{
    [SerializeField] GameObject testPoint;
    [SerializeField] Vector3 tipOffset = new Vector3(0, 0, 0.1f);
    [SerializeField] Vector3 boardShift = new Vector3(0, 0, 0.1f);
    [SerializeField] Transform rightController;
    [SerializeField] Transform leftControllerAnchor;
    [SerializeField] float gizmosSize = 0.005f;

    [SerializeField] GameObject boardPlane;
    private List<GameObject> boards = new List<GameObject>();
    [SerializeField] float xStretch = 0.0554f;
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

    //TODO: get controller position by either OVRInput.Controller or controller prefab
    private Vector3 GetControllerTipPosition(OVRInput.Controller ovrController)
    {
        Vector3 ovrPos = OVRInput.GetLocalControllerPosition(ovrController);
        Vector3 pos = rightController.position;

        Debug.Log("PosOVRInput: " + ovrPos + ", PosTransform: " + pos);
        Vector3 tipPos = pos + rightController.TransformDirection(tipOffset);
        Debug.Log("Controller tip at: " + tipOffset);
        Instantiate(testPoint, tipPos, rightController.rotation);
        return tipPos;
    }

    void CreatePlaneBetweenPoints(Vector3 startPoint, Vector3 endPoint)
    {
        Vector3 midpoint = (startPoint + endPoint) / 2;
        Vector3 location = midpoint + boardShift;
        float distance = Vector3.Distance(startPoint, endPoint);
        Vector3 scale = new Vector3(xStretch, yStretch, distance);

        Quaternion rotation1 = Quaternion.LookRotation(endPoint - startPoint);
        
        //Quaternion rot2 = Quaternion.Euler(90f, 0 , 0);

        GameObject plane = Instantiate(boardPlane, location, rotation1, leftControllerAnchor);
        plane.transform.localScale = scale;
        boards.Add(plane);
        Debug.Log("PLane instantiated");

    }

    private void OnDrawGizmos()
    {
        if (rightController) { 
            Vector3 tipPos = rightController.position + rightController.TransformDirection(tipOffset);
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(tipPos, gizmosSize);
        }
    }

    public void DeleteBoards()
    {
        foreach (var board in boards)
        {
            Destroy(board);
        }
    }
}
