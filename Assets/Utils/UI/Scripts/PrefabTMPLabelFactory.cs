using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Alf.UI
{

public class DynamicLabelString
{
    public string LabelString;
}

public class PrefabTMPLabelFactory : IMenuItemFactory
{

    string _text;
    GameObject _labelPrefab;
    DynamicLabelString _dynamicLabelString;
    
    public PrefabTMPLabelFactory(GameObject labelPrefab, string text)
    {
        _text = text;
        _labelPrefab = labelPrefab;
    }

    public PrefabTMPLabelFactory(GameObject labelPrefab, DynamicLabelString labelString)
    {
        _labelPrefab = labelPrefab;
        _dynamicLabelString = labelString;
    }

    public GameObject MakeMenuItem(Transform parent = null)
    {
        GameObject label;
        if(parent != null)
            label = GameObject.Instantiate(_labelPrefab, parent);
        else
            label = GameObject.Instantiate(_labelPrefab);
        var labelTMP = label.GetComponent<TMP_Text>();
        if(labelTMP == null)
        {
            GameObject.Destroy(label);
            return null;
        }

        if(_dynamicLabelString != null)
        {
            labelTMP.text = _dynamicLabelString.LabelString;
            return label;
        }
        labelTMP.text = _text;
        return label;
    }

}
}
