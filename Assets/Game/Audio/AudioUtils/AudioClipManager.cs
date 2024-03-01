using UnityEngine;

namespace Alf.AudioUtils
{
public class AudioClipManager : MonoBehaviour
{
    
    [SerializeField] private AudioClipSO[] _audioClipSOs;

    void Start()
    {
        InitAudioClipPlayers();
    }

    private void InitAudioClipPlayers()
    {
        foreach(AudioClipSO audioClipSO in _audioClipSOs)
        {
            audioClipSO.RegisterTriggers();
        }
    }
}
}
