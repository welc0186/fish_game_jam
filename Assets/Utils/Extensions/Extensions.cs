using UnityEngine;
using System;
using System.Collections;
using System.Reflection;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public static class Vector2Extensions
{
    public static Vector2 rotate(this Vector2 v, float delta) {
        return new Vector2(
            v.x * Mathf.Cos(delta) - v.y * Mathf.Sin(delta),
            v.x * Mathf.Sin(delta) + v.y * Mathf.Cos(delta)
        );
    }

    public static Vector2 RandOffset(this Vector2 v, float minOffset, float maxOffset)
    {
        var offsetDir = new Vector2(
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f)
        );
        offsetDir.Normalize();
        var offsetMag = Random.Range(minOffset, maxOffset);
        return v + offsetDir*offsetMag;
    }
}

public static class Vector2IntExtensions
{
    public static Vector2Int sign(this Vector2Int v)
    {
        var ret = Vector2Int.zero;
        if(v.x > 0)
            ret.x = 1;
        if(v.x < 0)
            ret.x = -1;
        if(v.y > 0)
            ret.y = 1;
        if(v.y < 0)
            ret.y = -1;
        return ret;
    }

    public static List<Vector2Int> AdjacentCoords(this Vector2Int v, bool includeDiagonal = false)
    {
        var directions = new List<Vector2Int>() {
            Vector2Int.right,
            Vector2Int.down,
            Vector2Int.left,
            Vector2Int.up,
        };
        if(includeDiagonal)
        {
            directions.AddRange(new List<Vector2Int>(){
                new Vector2Int( 1, 1),
                new Vector2Int( 1,-1),
                new Vector2Int(-1,-1),
                new Vector2Int(-1, 1)
            });
        }
        var ret = new List<Vector2Int>();
        foreach(Vector2Int direction in directions)
            ret.Add(v + direction);
        return ret;
    }

    public static Vector3Int ToVector3Int(this Vector2Int v)
    {
        return new Vector3Int(v.x, v.y, 0);
    }

    public static Vector3Int ToVector3Int(this Vector2Int v, int z)
    {
        return new Vector3Int(v.x, v.y, z);
    }
}

public static class Vector3IntExtensions
{
    public static Vector2Int ToVector2Int(this Vector3Int v)
    {
        return new Vector2Int(v.x, v.y);
    }
}

public static class Vector3Extenstions
{
    public static Vector2 ToVector2(this Vector3 v)
    {
        return new Vector2(v.x, v.y);
    }
}

// Note: use public fields as properties (easier with Unity's inspector)
public interface IComponentProperties
{
    Type ComponentType();
    PropertyInfo[] GetProperties();
}

public static class GameObjectExtensions
{
    public static void AddComponentProperties(this GameObject gameObject, IComponentProperties componentProperties)
    {
        var newComponent = gameObject.AddComponent(componentProperties.ComponentType());
        foreach(PropertyInfo copyProperty in componentProperties.GetProperties())
        {
            foreach(PropertyInfo newProperty in newComponent.GetType().GetProperties())
            {
                if(newProperty.Name.Equals(copyProperty.Name))
                {
                    newProperty.SetValue(newComponent, copyProperty.GetValue(componentProperties));
                }
            }
        }
    }

    public static void RemoveAddComponent(this GameObject gameObject, IComponentProperties componentProperties)
    {
        var component = gameObject.GetComponent(componentProperties.ComponentType());
        if (component != null)
            GameObject.Destroy(component);
        gameObject.AddComponentProperties(componentProperties);
    }

    public static void SetParent(this GameObject gameObject, Transform parent, bool worldPositionStays = true)
    {
        gameObject.transform.SetParent(parent, worldPositionStays);
    }

    public static void SetParent(this List<GameObject> gameObjects, Transform parent, bool worldPositionStays = true)
    {
        foreach(GameObject child in gameObjects)
        {
            child.SetParent(parent, worldPositionStays);
        }
    }

}

public static class TransformExtensions
{
    public static Transform FindRecursive(this Transform self, string exactName) => self.FindRecursive(child => child.name == exactName);

    public static Transform FindRecursive(this Transform self, Func<Transform, bool> selector)
    {
        foreach (Transform child in self)
        {
            if (selector(child))
            {
                return child;
            }

            var finding = child.FindRecursive(selector);

            if (finding != null)
            {
                return finding;
            }
        }

        return null;
    }
}

public static class RectTransformExtensions
{
    public enum Anchor
    {
        UPPER_LEFT,
        UPPER_MIDDLE,
        UPPER_RIGHT,
        MIDDLE_LEFT,
        MIDDLE_MIDDLE,
        MIDDLE_RIGHT,
        LOWER_LEFT,
        LOWER_MIDDLE,
        LOWER_RIGHT
    }

    public static RectTransform SetAnchor(this RectTransform self, Anchor anchor)
    {
        Vector2 anchorMax;
        Vector2 anchorMin;
        switch(anchor)
        {
            case(Anchor.UPPER_RIGHT):
                anchorMin = new Vector2(1,1);
                anchorMax = new Vector2(1,1);
                break;
            default:
                anchorMin = new Vector2(0,0);
                anchorMax = new Vector2(0,0);
                Debug.LogWarning("Anchor preset not implemented!");
                break;
        }
        self.anchorMin = anchorMin;
        self.anchorMax = anchorMax;
        return self;
    }

}