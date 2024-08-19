using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fret : MonoBehaviour
{
    [SerializeField] Color deactivatedColor = Color.black;
    [SerializeField] Color activatedColor = Color.white;
    [SerializeField] Color errorColor = Color.red;
    MeshRenderer objRenderer;
    private bool isActivated;
    private bool isError;
    private bool isPlayed;

    private int playedCount;
    private int maxPlays = 10;

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
        if (Input.GetKeyDown(KeyCode.S))
        {
            SetColor(activatedColor);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            SetColor(deactivatedColor);
        }
        if (manager.GetCurrentPlayMode() == GameMode.Scales)
        {
            if (isActivated)
            {
                Activate();
            }
            else if (isError)
            {
                ActivateError();
            }

            else
            {
                Deactivate();
            }
        }

        if (manager.GetCurrentPlayMode() == GameMode.Heatmap)
        {
            if (isPlayed)
            {
                MarkPlayed();
                SetIsPlayed(false);
            }
        }

    }

    private void Activate()
    {
        SetColor(activatedColor);
    }

    private void Deactivate()
    {
        SetColor(deactivatedColor);
    }

    private void ActivateError()
    {
        SetColor(errorColor);
    }

    public void MarkPlayed()
    {
        playedCount++;
        SetColor(GetHeatmapColor(playedCount));
    }

    public void ResetFret()
    {
        Deactivate();
        playedCount = 0;
    }

    public void SetActivated(bool status) { isActivated = status; }

    public void SetError(bool status) { isError = status; }

    public void SetIsPlayed(bool status) { isPlayed = status; }

    private void SetColor(Color color)
    {
        objRenderer.material.color = color;
    }

    private Color GetHeatmapColor(int playedCount)
    {
        float normalizedVal = Mathf.Clamp01((float)playedCount / maxPlays);

        return Color.Lerp(Color.black, Color.white, normalizedVal);
    }

}
