using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Alf.UI
{

public static class UISettings
{
    public const string DEFAULTS_PATH = "UIDefaults";
    
    public static UIDefaults Defaults
    {
        get
        {
            var defaults = Resources.Load<UIDefaults>(DEFAULTS_PATH);
            if(defaults == null) return null;
            return defaults;
        }
    }
}

[CreateAssetMenu(fileName = "UIDefaults", menuName = "ScriptableObjects/UIDefaults")]
public class UIDefaults : ScriptableObject
{

    public Sprite ButtonSprite;

    // Color Scheme
    public Color PanelColor = Color.clear;
    public Color Background = Color.white;
    public Color Primary    = Color.black;
    public Color Secondary  = Color.gray;

    // Text
    public TMP_FontAsset Font;

    // Layout Group
    public LayoutGroupProperties defaultLayoutGroup;

}

}
