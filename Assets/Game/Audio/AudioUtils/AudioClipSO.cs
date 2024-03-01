using UnityEngine;
using Alf.Utils;
using Alf.Utils.SerializableTypes;
using System;

namespace Alf.AudioUtils
{

[CreateAssetMenu(menuName = "ScriptableObjects/Audio Clip")]
public class AudioClipSO : ScriptableObject
{
    public AudioClip audioClip;
    public float volume = 1f;
    public bool loop = false;

    [TypeFilter(typeof(ICustomEvent))]
    [SerializeField] SerializableType[] eventTriggers;

    public void RegisterTriggers()
    {
        var player = new AudioClipPlayer(this);

        for(int i = 0; i < eventTriggers.Length; i++)
        {
            var trigger = (ICustomEvent) Activator.CreateInstance(eventTriggers[i]);
            trigger.Event.Subscribe(() => player.Play());
        }
    }
}
}