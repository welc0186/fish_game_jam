using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Alf.UI
{

public class PrefabTMPButtonFactory : IMenuItemFactory
{

    Action _clickAction;
    string _text;
    GameObject _buttonPrefab;
    
    public PrefabTMPButtonFactory(GameObject buttonPrefab, Action clickAction, string text)
    {
        _clickAction = clickAction;
        _text = text;
        _buttonPrefab = buttonPrefab;
    }

    public GameObject MakeMenuItem(Transform parent = null)
    {
        GameObject button;
        if(parent != null)
            button = GameObject.Instantiate(_buttonPrefab, parent);
        else
            button = GameObject.Instantiate(_buttonPrefab);
        var buttonComponent = button.GetComponent<Button>();
        var buttonTMP = button.GetComponentInChildren<TMP_Text>();
        if(buttonComponent == null || buttonTMP == null)
        {
            Debug.LogWarning("TMPButton prefab could not be set up correctly");
            GameObject.Destroy(button);
            return null;
        }

        // TO-DO: Add generic button script that invokes different events (e.g. OnPointerEnter)
        buttonComponent.onClick.AddListener(() => _clickAction?.Invoke());
        buttonComponent.onClick.AddListener(() => UICustomEvents.onButtonClick.Invoke());

        buttonTMP.text = _text;
        return button;
    }

}
}
