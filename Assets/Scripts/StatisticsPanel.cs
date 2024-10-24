using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatisticsPanel : MonoBehaviour
{
    [SerializeField] TMP_Text Text;

    string text;
    float correct = 0;
    float incorrect = 0;
    PlayMode playMode;
    // Start is called before the first frame update
    void Start()
    {
        playMode = FindObjectOfType<PlayMode>();
        text = "Correct Notes Played: {0} \n" +
            "Incorrect Notes Played: {1}\n" +
            "Accuracy: {2}";
    }

    // Update is called once per frame
    void Update()
    {
        (correct, incorrect) = playMode.GetStatistics();
        float accuracy = (correct / (correct + incorrect)) * 100;
        Text.text = String.Format("Correct Notes Played: {0}\nIncorrect Notes Played: {1}\nAccuracy: {2}", correct, incorrect, accuracy);
    }
}
