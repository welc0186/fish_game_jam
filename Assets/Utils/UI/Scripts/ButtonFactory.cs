using UnityEngine;
using UnityEngine.UI;
using System;

namespace Alf.UI
{

public class ButtonFactory : IMenuItemFactory
{
    
	public const string PATH = "MenuButton";
	Sprite _sprite;
	IMenuItemFactory[] _buttonItems;
	Action _message;

	public ButtonFactory(Action message, string text)
	{
		_message = message;
		_buttonItems = new IMenuItemFactory[] {new LabelFactory(text)};
		_sprite = UISettings.Defaults.ButtonSprite;
	}

	public ButtonFactory(Action message, string text, Sprite image)
	{
		_message = message;
		_buttonItems = new IMenuItemFactory[] {new LabelFactory(text)};
		_sprite = image;
	}

	public ButtonFactory(Action message, Sprite image = null, IMenuItemFactory[] children = null)
	{
		_message = message;
		_sprite = (image != null)? image : UISettings.Defaults.ButtonSprite;
		if(children != null)
			_buttonItems = children;
	}
	
	public GameObject MakeMenuItem(Transform parent = null)
    {
		GameObject button;
        if(parent != null)
            button = GameObject.Instantiate(Resources.Load<GameObject>(PATH), parent);
        else
            button = GameObject.Instantiate(Resources.Load<GameObject>(PATH));
		button.GetComponent<MenuButton>().Message = _message;
		if(_sprite != null)
			button.GetComponent<Image>().sprite = _sprite;
		if(_buttonItems == null)
			_buttonItems = new IMenuItemFactory[0];
		foreach(IMenuItemFactory menuItem in _buttonItems)
		{
			var newItem = menuItem.MakeMenuItem();
			newItem.transform.SetParent(button.transform, false);
		}
		return button;
    }

}
}