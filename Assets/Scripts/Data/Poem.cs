using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Poem", menuName = "Scriptable Objects/Poem")]
public class Poem : ScriptableObject
{
    [SerializeField] private string title;
    [SerializeField] private string author;
    [SerializeField] private string speaker;
    [SerializeField] [TextArea(3, 10)] private string text;
    [SerializeField] AudioClip audioClip;
    
    public string Title => title;
    public string Author => author;
    public string Text => text;
    public string Speaker => speaker;
    public AudioClip AudioClip => audioClip;
}
