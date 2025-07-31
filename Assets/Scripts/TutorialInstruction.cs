using UnityEngine;

[CreateAssetMenu(menuName = "Tutorial Instruction")]
public class TutorialInstruction : ScriptableObject
{
    [SerializeField] Sprite image;
    [SerializeField] [TextArea(5,10)] string text;


    public Sprite GetImage() => image;

    public string GetText() => text;
}
