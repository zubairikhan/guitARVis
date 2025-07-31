using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tutorial : MonoBehaviour
{
    [SerializeField] TutorialInstruction[] hints;
    [SerializeField] TMP_Text hintTextField;
    [SerializeField] Image hintImage;
    [SerializeField] GameObject nextBtn;
    [SerializeField] GameObject prevBtn;
    [SerializeField] GameObject showBtn;
    int curr;
    
    
    // Start is called before the first frame update
    void Start()
    {
        UpdateHint(0);
        prevBtn.SetActive(false);
    }

    public void NextHint()
    {
        prevBtn.SetActive(true);
        curr++;
        if (curr >= hints.Length)
        {
            curr = hints.Length - 1;
            return;
        }
        UpdateHint(curr);
        if (curr + 1 >= hints.Length)
        {
            nextBtn.SetActive(false);
        }
    }

    public void PreviousHint()
    {
        nextBtn.SetActive(true);
        curr--;
        if (curr < 0)
        {
            curr = 0;
            return;
        }
        UpdateHint(curr);
        if (curr - 1 <= 0)
        {
            prevBtn.SetActive(false);
        }
    }

    private void UpdateHint(int hintIndex)
    {
        hintTextField.text = hints[hintIndex].GetText();
        hintImage.sprite = hints[hintIndex].GetImage();
    }
}
