using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Alf.UI
{

public class UIImageFactory : IMenuItemFactory
{
    
    private Sprite _image;
    private Vector2? _size;

    public UIImageFactory(Sprite image, Vector2? size = null)
    {
        _image = image;
        _size = size;
    }
    
    public GameObject MakeMenuItem(Transform parent = null)
    {
        var ret = new GameObject("UIImage", typeof(RectTransform), typeof(Image));
        if(parent != null)
            ret.transform.SetParent(parent, false);
        ret.GetComponent<Image>().sprite = _image;
        if(_size != null)
            ret.GetComponent<RectTransform>().sizeDelta = _size.Value;
        return ret;
    }
}
}
