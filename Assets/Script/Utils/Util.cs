using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Util
{
    public static T GetOrAddComponent<T>(GameObject go) where T : Component
    {
        T component = go.GetComponent<T>();
        if (component == null) { component = go.AddComponent<T>(); }
        return component;
    }
    public static Transform[] GetChildren(GameObject go)
    {
        int childcount = go.transform.childCount;
        Transform[] arr = null;
        if (childcount > 0)
        {
            arr = new Transform[childcount];
            for (int i = 0; i < childcount; i++)
            {
                arr[i] = go.transform.GetChild(i);
            }
        }
        return arr;
    }
    public static T[] GetChildren<T>(GameObject go)
    {
        int childcount = go.transform.childCount;
        T[] result = null;
        if (childcount > 0)
        {
            if (typeof(T) == typeof(GameObject))
            {
                GameObject[] newResult = new GameObject[childcount];
                for (int i = 0; i < childcount; i++)
                {
                    newResult[i] = go.transform.GetChild(i).gameObject;
                }
                return newResult as T[];
            }
            result = new T[childcount];
            for (int i = 0; i < childcount; i++)
            {
                result[i] = go.transform.GetChild(i).GetComponent<T>();
            }
        }
        return result;
    }

    public static Vector3 ConvertToCart(Vector3 iso, int z = 0)
    {
        Vector3 cart = new Vector3();
        cart.z = z;
        cart.x = (2.0f * iso.y + iso.x) * 0.5f;
        cart.y = (2.0f * iso.y - iso.x) * 0.5f;
        return cart;
    }
    public static Vector3 ConvertToCart(Vector2 iso, int z = 0)
    {
        Vector3 cart = new Vector3();
        cart.z = z;
        cart.x = (2.0f * iso.y + iso.x) * 0.5f;
        cart.y = (2.0f * iso.y - iso.x) * 0.5f;
        return cart;
    }
    public static void SetTile(Tilemap[] tilemaps, Vector3Int gridPosition, TileBase[] tiles)
    {
        for (int i = 0; i < tilemaps.Length; i++)
        {
            tilemaps[i].SetTile(gridPosition, tiles[i]);
        }
    }
}
