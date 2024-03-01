using System;
using System.Reflection;
using UnityEngine.UI;

namespace Alf.UI
{

[System.Serializable]
public class ContentFitterProperties : IComponentProperties
{

    public ContentSizeFitter.FitMode _horizontalFit;
    public ContentSizeFitter.FitMode _verticalFit;

    public ContentSizeFitter.FitMode horizontalFit {get {return _horizontalFit;}}
    public ContentSizeFitter.FitMode verticalFit {get {return _verticalFit;}}

    public Type ComponentType()
    {
        return (typeof(ContentSizeFitter));
    }

    public PropertyInfo[] GetProperties()
    {
        return this.GetType().GetProperties();
    }
}
}