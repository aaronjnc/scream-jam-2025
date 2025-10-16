using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogObject", menuName = "Scriptable Objects/DialogObject")]
public class DialogObject : ScriptableObject
{
    public enum ESoundTiming
    {
        Before = 0,
        During = 1,
        After = 2,
    }

    [SerializeField]
    private bool bPlayerOption;

    [SerializeField]
    private AudioClip textAudio;

    [SerializeField]
    private ESoundTiming soundTiming;

    [SerializeField]
    private string text;

    [SerializeField]
    private string impairedText;

    [SerializeField]
    private List<DialogObject> nextDialogs = new List<DialogObject>();

    [SerializeField]
    private Page nextPage;

    [SerializeField]
    private bool bImpairs;

    public List<DialogObject> GetNextObjects()
    {
        return nextDialogs;
    }

    public Page GetNextPage()
    {
        return nextPage;
    }

    public bool isPlayerOption()
    {
        return bPlayerOption;
    }

    public string GetText(bool isImpaired)
    {
        return isImpaired ? impairedText : text;
    }

    public bool DoesImpair()
    {
        return bImpairs;
    }

    public AudioClip GetAudioClip()
    {
        return textAudio;
    }

    public ESoundTiming GetSoundTiming()
    {
        return soundTiming;
    }
}
