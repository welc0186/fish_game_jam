#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace Alf.AudioUtils
{
[CustomEditor(typeof(AudioClipSO))]
public class AudioClipSOEditor : Editor
{
    private AudioSource _previewSource;
    
    public override void OnInspectorGUI()
    {
        AudioClipSO audioClipSO = (AudioClipSO) target;

        DrawDefaultInspector();

        GUILayout.Space(10);

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Preview"))
        {
            if (audioClipSO.audioClip != null)
            {
                if(_previewSource == null)
                    _previewSource = EditorUtility.CreateGameObjectWithHideFlags("Audio Preview", HideFlags.HideAndDontSave, typeof(AudioSource)).GetComponent<AudioSource>();
                _previewSource.clip = audioClipSO.audioClip;
                _previewSource.volume = audioClipSO.volume;
                _previewSource.loop = audioClipSO.loop;
                _previewSource.Play();
            }
            else
            {
                Debug.LogWarning("No audio clip assigned for preview.");
            }
        }
        GUILayout.EndHorizontal();

    }
}
}
#endif