using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FretBoard : MonoBehaviour
{
    [SerializeField] int fretCount; //total number of notes on the neck. 21 frets on 6 strings, plus 6 open strings = 21*6 + 6 = 132. corresponds to 132 fret game objects
    [SerializeField] GameObject[] frets; 
    [SerializeField] GameObject[] inlayMarkers;
    [SerializeField] int fretCountPerString; // number of frets. 21
    [SerializeField] float startingFretDist = 0.035604f; // distance from the open note fret to the first fret
    [SerializeField] Transform[] startingFretPos; // positions of open string note object on each of the 6 strings
    [SerializeField] Transform[] endingFretPos; //positions of 21st fret on each of the 6 strings. This is marked by empty game objects placed on each string where the 21st fret would be
    Vector3[] stringDirections;
    [SerializeField] float AdjustDistLong = 0.03f;
    [SerializeField] float AdjustDistShort = 0.01f;
    [SerializeField] float AdjustFactor = 0.9f;



    // Start is called before the first frame update
    void Start()
    {
        frets = new GameObject[fretCount];
        
        SetupFretPositionsV2();
        //for (int i = 0; i < fretCount; i++)
        //{
        //    frets[i] = this.gameObject.transform.GetChild(i).gameObject;
        //}

        SetNotesOnFrets();
    }

    //deprecated. use V2
    private void SetupFretPositionsV1()
    {
        stringDirections = ComputeStringDirections(startingFretPos, endingFretPos);



        int currStringIdx = -1;
        float fretDist = 0;
        Vector3 target = Vector3.zero;
        Vector3 prevFretPos = Vector3.zero;

        for (int i = 0; i < fretCount; i++)
        {

            var fretGameObject = frets[i].gameObject;
            int j = i % (fretCountPerString + 1);
            if (j == 0)
            {
                currStringIdx++;
                target = startingFretPos[currStringIdx].position;
                fretDist = startingFretDist;
            }
            else
            {
                Vector3 move = fretDist * stringDirections[currStringIdx];
                fretDist *= 0.9437f;
                target = prevFretPos + move;
            }

            fretGameObject.transform.position = target;
            prevFretPos = target;

            Vector3 scale = fretGameObject.transform.localScale;
            scale.x = (float)(scale.x * Math.Pow(0.965, j));
            fretGameObject.transform.localScale = scale;
        }
    }

    public void SetupFretPositionsV2()
    {
        Debug.Log("SetupFretPositionsV2");
        stringDirections = ComputeStringDirections(startingFretPos, endingFretPos);

        frets = new GameObject[fretCount];

        int currStringIdx = -1;
        double scalingFact = 0;

        for (int i = 0; i < fretCount; i++)
        {
            var gameObject = this.gameObject.transform.GetChild(i).gameObject;

            int j = i % (fretCountPerString + 1);
            if (j == 0)
            {
                currStringIdx++;
            }
            //scalingFact = Helper.fretDistsFromNut[j] / stringDirections[currStringIdx].x;
            //Vector3 move = (float)scalingFact * stringDirections[currStringIdx];

            Vector3 move = (float)Helper.fretDistsFromNut[j] * stringDirections[currStringIdx];
            gameObject.transform.position += move;
            
            //prevFretPos = target;

            Vector3 scale = gameObject.transform.localScale;
            scale.x = (float)(scale.x * Math.Pow(0.965, j));
            gameObject.transform.localScale = scale;


            frets[i] = gameObject;
        }

        SetInlayMarkersPosition();

    }

    private void SetInlayMarkersPosition()
    {
        for (int i = 0; i < inlayMarkers.Length; i++)
        {
            var marker = inlayMarkers[i];
            Vector3 currPos = marker.transform.localPosition;
            float newXPos = marker.GetComponent<InlayMarker>().fret.localPosition.x;
            float newYPos = marker.GetComponent<InlayMarker>().fret.localPosition.y - 0.015f;
            if (i == 5)
            {
                newYPos -= 0.01f;
            }
            marker.transform.localPosition = new Vector3(newXPos, newYPos, currPos.z);
        }
    }

    private Vector3[] ComputeStringDirections(Transform[] startingFretPos, Transform[] endingFretPos)
    {
        Vector3[] directions = new Vector3[6]; 
        for(int i = 0; i < startingFretPos.Length; i++)
        {
            directions[i] = (endingFretPos[i].position - startingFretPos[i].position).normalized;
        }
        return directions;
    }

    private double[] ComputeStringAngles(Transform[] startingFretPos, Transform[] endingFretPos)
    {
        double[] angles = new double[6];
        for (int i = 0; i < startingFretPos.Length; i++)
        {
            angles[i] = Math.Atan(endingFretPos[i].position.y - startingFretPos[i].position.x / Helper.fretDistsFromNut[^1]);
        }
        return angles;
    }

    public GameObject[] GetFrets() => frets;
    
    public int GetFretCountPerString() => fretCountPerString;

    public void ResetFretBoard() 
    {

        foreach (var fret in frets)
        {
            fret.GetComponent<Fret>().ResetFret();
        }
    }

    public void ToggleNoteNameOnFrets(bool status)
    {
        foreach (var fret in frets)
        {
            fret.GetComponent<Fret>().ToggleNoteNameOnFret(status);
        }
    }

    public void ToggleHintsForAllowedNotes(bool status)
    {
        foreach (var fret in frets)
        {
            Fret fretScript = fret.GetComponent<Fret>();
            fretScript.ToggleNoteNameOnFret(status);
            fretScript.ToggleNote(status);
        }
    }

    public void SetNotesOnFrets() 
    {
        int stringNum = 0;
        int currNoteIdx = Array.IndexOf(Helper.noteNames, Helper.openStringNoteNames[stringNum]);
        for (int i = 0; i < frets.Length; i++)
        {
            
            frets[i].GetComponent<Fret>().Note = Helper.noteNames[currNoteIdx];
            currNoteIdx = (currNoteIdx+1) % Helper.noteNames.Length;

            if ((i+1) % (fretCountPerString + 1) == 0 && stringNum + 1 < Helper.openStringNoteNames.Length) 
            {
                stringNum++;
                currNoteIdx = Array.IndexOf(Helper.noteNames, Helper.openStringNoteNames[stringNum]);
            }
            
        }
    }

    private Dictionary<GameObject, Vector3> fretPositions;
    public void AdjustFretPositions()
    {
        float adjDist = AdjustDistLong;
        fretPositions = new Dictionary<GameObject, Vector3>();
        //float distAmt = 0;
        //0-8, when 9 -> move to next string
        //update position for frets on all strings below the 9th fret
        Transform[] startPos = {
            frets[0].gameObject.transform,
            frets[22].gameObject.transform,
            frets[44].gameObject.transform,
            frets[66].gameObject.transform,
            frets[88].gameObject.transform,
            frets[110].gameObject.transform
        };
        Transform[] endPos = {
            frets[21].gameObject.transform,
            frets[43].gameObject.transform,
            frets[65].gameObject.transform,
            frets[87].gameObject.transform,
            frets[109].gameObject.transform,
            frets[131].gameObject.transform
        };
        stringDirections = ComputeStringDirections(startPos, endPos);

        int currStringIdx = 0;
        int j = 0;
        for (int i = 0; i < fretCount; i++)
        {
            
            
            if (j > 8)
            {
                //dist = x
                j = 0;
                currStringIdx++;
                i = i + 13;
                adjDist = AdjustDistLong;
            }
            
            if (currStringIdx > 5)
            {
                break;
            }


            var gameObject = frets[i];
            Vector3 move = adjDist * stringDirections[currStringIdx];
            fretPositions.Add(gameObject, gameObject.transform.position);
            gameObject.transform.position -= move;

            adjDist *= AdjustFactor;

            j++;
            
            
            //prevFretPos = target;

        }
    }

    public void ResetFretPositions()
    {
        float adjDist = AdjustDistLong;
        fretPositions = new Dictionary<GameObject, Vector3>();
        //float distAmt = 0;
        //0-8, when 9 -> move to next string
        //update position for frets on all strings below the 9th fret
        Transform[] startPos = {
            frets[0].gameObject.transform,
            frets[22].gameObject.transform,
            frets[44].gameObject.transform,
            frets[66].gameObject.transform,
            frets[88].gameObject.transform,
            frets[110].gameObject.transform
        };
        Transform[] endPos = {
            frets[21].gameObject.transform,
            frets[43].gameObject.transform,
            frets[65].gameObject.transform,
            frets[87].gameObject.transform,
            frets[109].gameObject.transform,
            frets[131].gameObject.transform
        };
        stringDirections = ComputeStringDirections(startPos, endPos);

        int currStringIdx = 0;
        int j = 0;
        for (int i = 0; i < fretCount; i++)
        {


            if (j > 8)
            {
                //dist = x
                j = 0;
                currStringIdx++;
                i = i + 13;
                adjDist = AdjustDistLong;
            }

            if (currStringIdx > 5)
            {
                break;
            }


            var gameObject = frets[i];
            Vector3 move = adjDist * stringDirections[currStringIdx];
            fretPositions.Add(gameObject, gameObject.transform.position);
            gameObject.transform.position += move;

            adjDist *= AdjustFactor;

            j++;


            //prevFretPos = target;

        }
    }
}
