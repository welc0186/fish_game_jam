using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alf.UI
{
public class GameMenuPanel : MonoBehaviour
{

    private GameMenu _loadedMenu;

    public static GameObject Spawn(GameObject prefab, Transform parent)
    {
        var menuPanel = GameObject.Instantiate(prefab, parent);
        menuPanel.AddComponent<GameMenuPanel>();
        return menuPanel;
    }

    public GameMenu LoadMenu(GameMenu menu)
    {   
        if(_loadedMenu != null)
            _loadedMenu.Unload();

        if(!gameObject.activeSelf)
            gameObject.SetActive(true);

        menu.Load(transform);
        _loadedMenu = menu;
        return _loadedMenu;
    }

    public void Close()
    {
        if(_loadedMenu != null)
            _loadedMenu.Unload();

        gameObject.SetActive(false);
    }

    void OnDestroy()
    {
        if (_loadedMenu != null)
            _loadedMenu.Unload();
    }

}
}
