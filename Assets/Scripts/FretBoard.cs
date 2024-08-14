using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FretBoard : MonoBehaviour
{
    [SerializeField] int fretCount;
    [SerializeField] GameObject[] frets;

    void Awake()
    {
        frets = new GameObject[fretCount];
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            frets[i] = this.gameObject.transform.GetChild(i).gameObject;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public GameObject[] GetFrets() => frets;
}
