using UnityEngine;

public class SelectModePanel : MonoBehaviour
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

    public void LoadLastNodeModeScene()
    {
        manager.LoadScene("Assets/Scenes/LastNodeModeScene.unity");
    }

    public void Enable()
    {
        this.gameObject.SetActive(true);
    }

    public void Disable()
    {
        this.gameObject.SetActive(false);
    }
}
