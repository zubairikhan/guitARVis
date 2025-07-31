using System;
using TMPro;
using UnityEngine;

public class StatisticsPanel : MonoBehaviour
{
    [SerializeField] TMP_Text Text;

    string text;
    float correct = 0;
    float incorrect = 0;
    PlayMode[] playModes;
    // Start is called before the first frame update
    void Start()
    {
        playModes = FindObjectsOfType<PlayMode>();
        text = "Correct Notes Played:\n{0} \n" +
            "Incorrect Notes Played:\n{1}\n" +
            "Accuracy:\n{2}";
    }

    // Update is called once per frame
    void Update()
    {
        (correct, incorrect) = playModes[0].GetStatistics();
        double accuracy = (correct + incorrect) == 0 ? 0.0 : (correct / (correct + incorrect)) * 100;
        Text.text = String.Format("Correct Notes Played:\n{0}\nIncorrect Notes Played:\n{1}\nAccuracy:\n{2}", correct, incorrect, Math.Round(accuracy, 2));
    }

    public void ResetStatistics()
    {
        foreach (PlayMode mode in playModes)
        {
            mode.ResetStatistics();
        }
    }
}
