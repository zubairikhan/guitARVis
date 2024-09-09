using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Fret : MonoBehaviour
{
    [SerializeField] Color deactivatedColor = Color.white;
    [SerializeField] Color activatedColor = Color.blue;
    [SerializeField] Color errorColor = Color.red;
    [SerializeField] Color scaleColor = Color.green;
    [SerializeField] TMP_Text noteNameTextBox;
    MeshRenderer objRenderer;
    private bool isActivated;
    private bool isError;

    PlayMode playMode;
    private bool IsInScale => playMode.AllowedNotes.Contains(Note);

    private int playedCount;
    [SerializeField] int maxPlays = 10;

    private ApplicationManager manager;

    [SerializeField] private string note;
    public string Note
    {
        get { return note; }
        set { note = value; }
    }

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        playMode = FindObjectOfType<PlayMode>();
        manager = FindObjectOfType<ApplicationManager>();
        objRenderer = this.gameObject.GetComponent<MeshRenderer>();
        SetColor(deactivatedColor);
        noteNameTextBox.text = Note;
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
        var targetColor = IsInScale ? scaleColor : deactivatedColor;
        SetActivated(false);
        SetError(false);
        SetColor(targetColor);
    }

    public void MarkPlayed(bool error)
    {
        playedCount++;
        SetColor(GetHeatmapColor(playedCount, error));
    }

    public void SwitchOnScale()
    {
        //SetInScale(true);
        Debug.Log("switching on: " + this.gameObject.name);
        SetColor(scaleColor);
    }

    public void ResetFret()
    {
        Deactivate();
        playedCount = 0;
    }

    public void SetActivated(bool status) { isActivated = status; }

    public void SetError(bool status) { isError = status; }

    //public void SetInScale(bool status) { isInScale = status; }

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
