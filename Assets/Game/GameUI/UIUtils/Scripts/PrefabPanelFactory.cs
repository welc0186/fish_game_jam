using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Alf.UI
{

public class PrefabPanelFactory : IMenuItemFactory
{
    
    GameObject _prefab;
    IMenuItemFactory[] _childMenuItems;
    
    public PrefabPanelFactory(GameObject prefab, IMenuItemFactory[] childItems = null)
    {
        _prefab = prefab;
        _childMenuItems = childItems;
    }
    
    
    public GameObject MakeMenuItem(Transform parent = null)
    {
        GameObject panel;
        if(parent != null)
            panel = GameObject.Instantiate(_prefab, parent);
        else
            panel = GameObject.Instantiate(_prefab);
        SpawnChildren(panel);
        return panel;
    }

    private void SpawnChildren(GameObject menuPanel)
    {
        if(_childMenuItems == null) return;
        foreach(IMenuItemFactory menuItemFactory in _childMenuItems)
        {
            menuItemFactory.MakeMenuItem(menuPanel.transform);
        }
    }

}

}
