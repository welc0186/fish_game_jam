using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Alf.Utils;
using TMPro;

namespace Alf.UI
{

[System.Serializable]
public class ButtonMessage
{
	public string Text;
	public ButtonMessage(string message)
	{
		Text = message;
	}
}

public class MenuButton : MonoBehaviour
{

	public Action Message;
	// public static readonly CustomEvent onButtonPressed = new CustomEvent();

    void Awake()
	{
		// GetComponent<Button>().onClick.AddListener( () => {onButtonPressed.Invoke(gameObject, Message);});
		GetComponent<Button>().onClick.AddListener( () => {Message?.Invoke();});
	}

}
}