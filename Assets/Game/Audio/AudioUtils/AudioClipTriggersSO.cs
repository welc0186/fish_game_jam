using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alf.AudioUtils
{

[CreateAssetMenu(menuName = "ScriptableObjects/Audio Clip Triggers")]
public class AudioClipTriggersSO : ScriptableObject
{
    
    public AudioClipTrigger[] audioClipTriggers;

}
}
