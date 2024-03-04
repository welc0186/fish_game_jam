using UnityEngine;

namespace Alf.AudioUtils
{
public class AudioClipManager : MonoBehaviour
{
    
    [SerializeField] private AudioClipTriggersSO _audioClipTriggers;

    void Start()
    {
        InitAudioClipPlayers();
    }

    private void InitAudioClipPlayers()
    {
        foreach(AudioClipTrigger clipTrigger in _audioClipTriggers.audioClipTriggers)
        {
            clipTrigger.RegisterTrigger();
        }
    }
}
}
