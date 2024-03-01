using System.Collections;
using System.Collections.Generic;
using Alf.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Alf.UI
{

public class PopupPanel : MonoBehaviour, IPointerDownHandler
{

    public const string PAUSE_PANEL = "PausePanel";
    public GameObject MenuPanel;

    public static GameObject Spawn(Vector2 worldPosition, IMenuItemFactory menuPanelFactory)
    {
        var popup = GameObject.Instantiate(Resources.Load<GameObject>(PAUSE_PANEL), GameObject.Find("Canvas_UI").transform);
        popup.AddComponent<PopupPanel>();
        var menuPanel = menuPanelFactory.MakeMenuItem(popup.transform);
        menuPanel.transform.position = Camera.main.WorldToScreenPoint(worldPosition);
        popup.GetComponent<PopupPanel>().MenuPanel = menuPanel;
        return popup;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Destroy(gameObject);
    }
}
}
