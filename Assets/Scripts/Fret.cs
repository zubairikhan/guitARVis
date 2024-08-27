using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fret : MonoBehaviour
{
    [SerializeField] Color deactivatedColor = Color.white;
    [SerializeField] Color activatedColor = Color.blue;
    [SerializeField] Color errorColor = Color.red;
    MeshRenderer objRenderer;
    private bool isActivated;
    private bool isError;

    private int playedCount;
    [SerializeField] int maxPlays = 10;

    private ApplicationManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<ApplicationManager>();
        objRenderer = this.gameObject.GetComponent<MeshRenderer>();
        SetColor(deactivatedColor);
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.GetCurrentPlayMode() == typeof(LastNotePlayMode))
        {
            if (isActivated)
            {
                Activate(isError);
            }

            else
            {
                Deactivate();
            }
        }

        if (manager.GetCurrentPlayMode() == typeof(HeatmapPlayMode))
        {
            if (isActivated)
            {
                MarkPlayed(isError);
                SetActivated(false);
                SetError(false);
            }
        }

    }

    private void Activate(bool error)
    {
        var targetColor = error ? errorColor : activatedColor;
        SetColor(targetColor);
    }

    private void Deactivate()
    {
        SetColor(deactivatedColor);
    }

    public void MarkPlayed(bool error)
    {
        playedCount++;
        SetColor(GetHeatmapColor(playedCount, error));
    }

    public void ResetFret()
    {
        Deactivate();
        playedCount = 0;
    }

    public void SetActivated(bool status) { isActivated = status; }

    public void SetError(bool status) { isError = status; }

    private void SetColor(Color color)
    {
        objRenderer.material.color = color;
    }

    private Color GetHeatmapColor(int playedCount, bool error)
    {
        float normalizedVal = Mathf.Clamp01((float)playedCount / maxPlays);
        var targetColor = error ? errorColor : activatedColor;

        return Color.Lerp(deactivatedColor, targetColor, normalizedVal);
    }
}
