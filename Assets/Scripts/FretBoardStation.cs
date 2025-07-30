using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FretBoardStation : MonoBehaviour
{
    [SerializeField] float moveUnit = 0.005f; 
    ApplicationManager applicationManager;
    MeshRenderer meshRenderer;
    // Start is called before the first frame update
    void Start()
    {
        applicationManager = FindObjectOfType<ApplicationManager>();
        applicationManager.SetFretBoardStation(this.gameObject);
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            transform.position += transform.right * moveUnit;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            transform.position -= transform.right * moveUnit;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.position -= transform.forward * moveUnit;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.position += transform.forward * moveUnit;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            transform.position += transform.up * moveUnit;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            transform.position -= transform.up * moveUnit;
        }
    }



    public void MoveFretBoard(MoveDirection moveDirection)
    {
        switch (moveDirection)
        {
            case MoveDirection.Left:
                transform.position -= transform.forward * moveUnit;
                break;
            case MoveDirection.Right:
                transform.position += transform.forward * moveUnit;
                break;
            case MoveDirection.Up:
                transform.position += transform.right * moveUnit;
                break;
            case MoveDirection.Down:
                transform.position -= transform.right * moveUnit;
                break;
            case MoveDirection.Backwards:
                transform.position -= transform.up * moveUnit;
                break;
            case MoveDirection.Forwards:
                transform.position += transform.up * moveUnit;
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Fretboard")
        {
            meshRenderer.enabled = false;
            //other.gameObject.GetComponent<FretBoard>().AdjustFretPositions();
        }
            
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Fretboard")
        {
            meshRenderer.enabled = true;
            //other.gameObject.GetComponent<FretBoard>().ResetFretPositions();
        }
            
    }
}


