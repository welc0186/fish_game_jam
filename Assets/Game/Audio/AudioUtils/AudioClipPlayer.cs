using UnityEngine;

namespace Alf.AudioUtils
{
public class AudioClipPlayer
{
    
    AudioClipSO _audioClipSO;
    AudioSource _audioSource;

    public AudioClipPlayer(AudioClipSO audioClipSO)
    {
        _audioClipSO = audioClipSO;
    }

    public void Play()
    {
        if(_audioSource == null)
        {
            _audioSource = new GameObject("Audio Source " + _audioClipSO.name, typeof(AudioSource)).GetComponent<AudioSource>();
            Object.DontDestroyOnLoad(_audioSource.gameObject);
        }
        _audioSource.clip = _audioClipSO.audioClip;
        _audioSource.volume = _audioClipSO.volume;
        _audioSource.loop = _audioClipSO.loop;
        _audioSource.Play();
    }

}
}
