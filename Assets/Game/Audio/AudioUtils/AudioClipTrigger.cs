using System;
using Alf.Utils;
using Alf.Utils.SerializableTypes;
using UnityEngine;

namespace Alf.AudioUtils
{
[System.Serializable]
public class AudioClipTrigger
{
    public AudioClipSO audioClip;
    [TypeFilter(typeof(ICustomEvent))]
    [SerializeField] SerializableType clipTrigger;

    public void RegisterTrigger()
    {
        var player = new AudioClipPlayer(audioClip);
        var trigger = (ICustomEvent) Activator.CreateInstance(clipTrigger);
        trigger.Event.Subscribe(() => player.Play());
    }
}
}
