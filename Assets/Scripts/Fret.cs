using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Fret : MonoBehaviour
{
    [SerializeField] Color deactivatedColor = Color.white; //when note is not active
    [SerializeField] Color activatedColor = Color.blue; //when note is played
    [SerializeField] Color errorColor = Color.red; //when note is played but its an error
    [SerializeField] Color scaleColor = Color.green; //highlighting notes in selected scale
    [SerializeField] TMP_Text noteNameTextBox;
    MeshRenderer objRenderer;
    private bool isActivated;
    private bool isError;

    PlayMode playMode;
    private bool IsInScale => playMode.AllowedNotes.Contains(Note);

    private int playedCount;
    [SerializeField] int maxPlays = 10;

    private ApplicationManager applicationManager;

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
        applicationManager = FindObjectOfType<ApplicationManager>();
        objRenderer = this.gameObject.GetComponent<MeshRenderer>();
        SetColor(deactivatedColor);
        SetNoteName();
    }

    // Update is called once per frame
    void Update()
    {
        if (applicationManager.GetCurrentPlayMode() == typeof(LastNotePlayMode))
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

        if (applicationManager.GetCurrentPlayMode() == typeof(HeatmapPlayMode))
        {
            if (isActivated)
            {
                MarkPlayed(isError);
                SetActivated(false);
                SetError(false);
            }
        }

    }

    private void SetNoteName()
    {
        if (Helper.naturalNotes.Contains(Note))
        {
            noteNameTextBox.text = Note;
        }
    }

    private void Activate(bool error)
    {
        var targetColor = error ? errorColor : activatedColor;
        SetColor(targetColor);
    }

    private void Deactivate()
    {
        var targetColor = applicationManager.IsHintsEnabled() && IsInScale ? scaleColor : deactivatedColor;
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
        SetColor(scaleColor);
    }

    public void ToggleNote(bool status)
    {
        if (playedCount != 0 ) { return; }

        var targetColor = status && IsInScale ? scaleColor : deactivatedColor;
        SetColor(targetColor);
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

    public void ToggleNoteNameOnFret(bool status)
    {
        noteNameTextBox.gameObject.SetActive(status);
    }
}
