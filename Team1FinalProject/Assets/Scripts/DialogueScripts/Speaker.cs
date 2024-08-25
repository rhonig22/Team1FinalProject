
using UnityEngine;

[CreateAssetMenuAttribute (fileName = "New Speaker", menuName = "Scriptables/ Dialogue/ New Speaker")]
public class Speaker : ScriptableObject
{
    [SerializeField] private string speakerName;
    [SerializeField] private Sprite speakerSprite;

    public string GetName()
    {
        return speakerName;
    }

    public Sprite getSprite()
    {
        return speakerSprite;
    }
}
