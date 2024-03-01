using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Alf.UI
{

public class PrefabTMPLabelFactory : IMenuItemFactory
{

    string _text;
    GameObject _labelPrefab;
    
    public PrefabTMPLabelFactory(GameObject labelPrefab, string text)
    {
        _text = text;
        _labelPrefab = labelPrefab;
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

        labelTMP.text = _text;
        return label;
    }

}
}
