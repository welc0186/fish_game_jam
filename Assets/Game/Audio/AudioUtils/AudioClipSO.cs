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
}
}