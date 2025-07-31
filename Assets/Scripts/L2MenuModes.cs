using UnityEngine;

public class L2MenuModes : MonoBehaviour
{
    ApplicationManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<ApplicationManager>();
    }

    public void LoadHeatmapModeScene()
    {
        manager.LoadScene("Assets/Scenes/HeatmapModeScene.unity");
    }

    public void LoadLastNoteModeScene()
    {
        manager.LoadScene("Assets/Scenes/LastNodeModeScene.unity");
    }
}
