using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alf.UI
{

public class GameMenu
{
    
    public IMenuItemFactory[] MenuItemFactories { get; private set; }

    private List<GameObject> _menuItems;

    public GameMenu(IMenuItemFactory[] menuItemFactories)
    {
        MenuItemFactories = menuItemFactories;
    }

    public List<GameObject> Load(Transform parent = null)
    {
        if(_menuItems != null)
            Unload();
        
        _menuItems = new List<GameObject>();
        foreach(IMenuItemFactory menuItemFactory in MenuItemFactories)
        {
            _menuItems.Add(menuItemFactory.MakeMenuItem(parent));
        }
        return _menuItems;
    }

    public void Unload()
    {
        if(_menuItems == null)
            return;
        foreach(GameObject menuItem in _menuItems)
        {
            GameObject.Destroy(menuItem);
        }
        _menuItems = null;
    }
}
}
