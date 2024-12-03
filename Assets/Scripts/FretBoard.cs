using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FretBoard : MonoBehaviour
{
    [SerializeField] int fretCount;
    [SerializeField] GameObject[] frets;
    [SerializeField] int fretCountPerString;
    [SerializeField] float startingFretDist = 0.035604f;
    [SerializeField] Transform[] startingFretPos;
    [SerializeField] Transform[] endingFretPos;
    Vector3[] stringDirections;
    
    [SerializeField] static int[] fretDistChanges = { 2, 4, 8, 12, 16, 20 };
    
    HashSet<int> fretDistChangesHashSet = new HashSet<int>(fretDistChanges);
     
    // Start is called before the first frame update
    void Start()
    {
        SetupFretPositionsV2();

        SetNotesOnFrets();
    }

    private void SetupFretPositionsV1()
    {
        stringDirections = ComputeStringDirections(startingFretPos, endingFretPos);

        frets = new GameObject[fretCount];

        int currStringIdx = -1;
        float fretDist = 0;
        Vector3 target = Vector3.zero;
        Vector3 prevFretPos = Vector3.zero;

        for (int i = 0; i < fretCount; i++)
        {
            var gameObject = this.gameObject.transform.GetChild(i).gameObject;

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

            gameObject.transform.position = target;
            prevFretPos = target;

            Vector3 scale = gameObject.transform.localScale;
            scale.x = (float)(scale.x * Math.Pow(0.965, j));
            gameObject.transform.localScale = scale;


            frets[i] = gameObject;
        }
    }

    private void SetupFretPositionsV2()
    {
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
            scalingFact = Helper.fretDistsFromNut[j] / stringDirections[currStringIdx].x;
            Vector3 move = (float)scalingFact * stringDirections[currStringIdx];
            gameObject.transform.position += move;
            
            //prevFretPos = target;

            Vector3 scale = gameObject.transform.localScale;
            scale.x = (float)(scale.x * Math.Pow(0.965, j));
            gameObject.transform.localScale = scale;


            frets[i] = gameObject;
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
}
