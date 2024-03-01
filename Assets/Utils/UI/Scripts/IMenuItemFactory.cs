using UnityEngine;

namespace Alf.UI
{
public interface IMenuItemFactory
{
	public GameObject MakeMenuItem(Transform parent = null);
}
}