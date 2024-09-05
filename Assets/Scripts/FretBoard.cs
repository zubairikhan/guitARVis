using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FretBoard : MonoBehaviour
{
    [SerializeField] int fretCount;
    [SerializeField] GameObject[] frets;
    [SerializeField] int fretCountPerString;
    void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        frets = new GameObject[fretCount];
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            frets[i] = this.gameObject.transform.GetChild(i).gameObject;
        }

        SetNotesOnFrets();
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
