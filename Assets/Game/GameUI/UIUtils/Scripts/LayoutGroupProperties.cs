using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using Alf.Utils;

public enum LayoutGroupType
{
    VERTICAL,
    HORIZONTAL
}

[System.Serializable]
public class LayoutGroupProperties : IComponentProperties
{
    public LayoutGroupType _type;
    public RectOffset _padding = new RectOffset();
    public int _spacing; 
    public TextAnchor _childAlignment = new TextAnchor();
    public bool _childControlWidth;
    public bool _childControlHeight;
    public bool _childScaleWidth;
    public bool _childScaleHeight;
    public bool _childForceExpandWidth;
    public bool _childForceExpandHeight;

    public LayoutGroupType type {get {return _type;}}
    public RectOffset padding {get {return _padding;}}
    public int spacing {get {return _spacing;}}
    public TextAnchor childAlignment {get {return _childAlignment;}}
    public bool childControlWidth {get {return _childControlWidth;}}
    public bool childControlHeight {get {return _childControlHeight;}}
    public bool childScaleWidth {get {return _childScaleWidth;}}
    public bool childScaleHeight {get {return _childScaleHeight;}}
    public bool childForceExpandWidth {get {return _childForceExpandWidth;}}
    public bool childForceExpandHeight {get {return _childForceExpandHeight;}}


    public Type ComponentType()
    {
        if(_type == LayoutGroupType.VERTICAL)
            return typeof(VerticalLayoutGroup);
        return typeof(HorizontalLayoutGroup);
    }

    public LayoutGroupProperties Clone()
    {
        return (LayoutGroupProperties) this.MemberwiseClone();
    }

    public PropertyInfo[] GetProperties()
    {
        return this.GetType().GetProperties();
    }
}
