using UnityEngine;
using TMPro;

namespace Alf.UI
{

public class LabelFactory : IMenuItemFactory
{
    public const string PATH = "MenuLabel";
	GameObject _labelPrefab;
	string _text;
	Vector2? _size;
	TMP_FontAsset _fontAsset;

	public LabelFactory(string text, Vector2? size = null, TMP_FontAsset fontAsset = null)
	{
		_text = text;
		_size = size;
		_fontAsset = fontAsset;
	}
	
	public GameObject MakeMenuItem(Transform parent = null)
    {
		GameObject label;
        if(parent != null)
            label = GameObject.Instantiate(Resources.Load<GameObject>(PATH), parent);
        else
            label = GameObject.Instantiate(Resources.Load<GameObject>(PATH));
		label.GetComponent<TMP_Text>().text = _text;
		if(_size != null)
			label.GetComponent<RectTransform>().sizeDelta = _size.Value;
		if(_fontAsset == null)
			_fontAsset = UISettings.Defaults.Font;
		label.GetComponent<TMP_Text>().font = _fontAsset;
		return label;
    }

}
}
