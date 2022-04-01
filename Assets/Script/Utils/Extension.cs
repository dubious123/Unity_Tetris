using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public static class Extension
{
    public static T GetOrAddComponent<T>(this GameObject go) where T : Component
    {
        return Util.GetOrAddComponent<T>(go);
    }
    public static Transform[] GetChildren(this GameObject go)
    {
        return Util.GetChildren(go);
    }
    public static T[] GetChildren<T>(this GameObject go)
    {
        return Util.GetChildren<T>(go);
    }
    public static Vector3 ConvertToCart(this Vector3 iso, int z = 0)
    {
        return Util.ConvertToCart(iso, z);
    }
    public static Vector3 ConvertToCart(this Vector2 iso, int z = 0)
    {
        return Util.ConvertToCart(iso, z);
    }
    //public static Vector3 ConvertToIso(this Vector3 cart, int z = 0)
    //{
    //    return Util.ConvertToIso(cart, z);
    //}
    //public static Vector3 ConvertToIso(this Vector2 cart, int z = 0)
    //{
    //    return Util.ConvertToIso(cart, z);
    //}
    public static void SetTile(this Tilemap[] tilemaps, Vector3Int tileCartPos, TileBase[] tiles)
    {
        Util.SetTile(tilemaps, tileCartPos, tiles);
    }
    public static T ChangeAlpha<T>(this T g, float newAlpha) where T : Graphic
    {
        var color = g.color;
        color.a = newAlpha;
        g.color = color;
        return g;
    }
    public static int Factorial(this int x)
    {
        int result = 1;
        if (x < 0) return 0;
        for(int i = 1; i <= x; i++)
        {
            result *= i;
        }
        return result;
    }
}
