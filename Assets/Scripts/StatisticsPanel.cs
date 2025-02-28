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
        text = "Correct Notes Played:\n{0} \n" +
            "Incorrect Notes Played:\n{1}\n" +
            "Accuracy:\n{2}";
    }

    // Update is called once per frame
    void Update()
    {
        (correct, incorrect) = playMode.GetStatistics();
        double accuracy = (correct / (correct + incorrect)) * 100;
        accuracy = double.IsInfinity(accuracy) ? 0 : accuracy;
        Text.text = String.Format("Correct Notes Played:\n{0}\nIncorrect Notes Played:\n{1}\nAccuracy:\n{2}", correct, incorrect, Math.Round(accuracy, 2));
    }
}
