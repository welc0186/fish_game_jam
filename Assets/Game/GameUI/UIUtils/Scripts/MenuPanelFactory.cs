using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Alf.UI
{

public class MenuPanelFactory : IMenuItemFactory
{
    
    public const string PATH = "MenuPanel";
    Sprite _background;
    Color _color = UISettings.Defaults.PanelColor;
    IComponentProperties[] _componentProperties;
    List<IMenuItemFactory> _childMenuItems;
    
    public MenuPanelFactory(List<IMenuItemFactory> children = null, params IComponentProperties[] componentProperties)
    {
        _childMenuItems = children;
        if(componentProperties != null)
            _componentProperties = componentProperties;
    }
    
    public MenuPanelFactory(Color color, List<IMenuItemFactory> children = null, params IComponentProperties[] componentProperties) : this(children, componentProperties)
    {
        _color = color;
    }

    public MenuPanelFactory(Sprite background, List<IMenuItemFactory> children = null, params IComponentProperties[] componentProperties) : this(children, componentProperties)
    {
        _background = background;
        _color = Color.white;
    }
    
    public GameObject MakeMenuItem(Transform parent = null)
    {
        GameObject menuPanel;
        if(parent != null)
            menuPanel = GameObject.Instantiate(Resources.Load<GameObject>(PATH), parent);
        else
            menuPanel = GameObject.Instantiate(Resources.Load<GameObject>(PATH));
        if(_background != null)
            menuPanel.GetComponent<Image>().sprite = _background;
        menuPanel.GetComponent<Image>().color = _color;
        foreach(IComponentProperties componentProperties in _componentProperties)
        {
            menuPanel.AddComponentProperties(componentProperties);
        }
        SpawnChildren(menuPanel);
        return menuPanel;
    }

    private void SpawnChildren(GameObject menuPanel)
    {
        if(_childMenuItems == null) return;
        foreach(IMenuItemFactory menuItemFactory in _childMenuItems)
        {
            var newItem = menuItemFactory.MakeMenuItem(menuPanel.transform);
        }
    }

}

}
