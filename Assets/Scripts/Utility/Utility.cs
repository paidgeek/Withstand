using System;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;

public static class Utility
{
  public static bool RandomBool()
  {
    return Random.Range(0, 2) == 0;
  }

  public static int RandomSign()
  {
    return Random.Range(0, 2) == 0 ? -1 : 1;
  }

  public static void SetLayerRecursively(this GameObject gameObject, int layer)
  {
    gameObject.layer = layer;
    var t = gameObject.transform;

    for (var i = 0; i < t.childCount; i++) {
      t.GetChild(i).gameObject.SetLayerRecursively(layer);
    }
  }

  public static Vector3 Abs(this Vector3 v)
  {
    return new Vector3(Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z));
  }

  public static string ToBase64(string text)
  {
    return Convert.ToBase64String(Encoding.UTF8.GetBytes(text)).TrimEnd('=').Replace('+', '-').Replace('/', '_');
  }

  public static string FromBase64(string encoded)
  {
    encoded = encoded.Replace('_', '/').Replace('-', '+');

    switch (encoded.Length % 4) {
      case 2:
        encoded += "==";
        break;
      case 3:
        encoded += "=";
        break;
    }

    return Encoding.UTF8.GetString(Convert.FromBase64String(encoded));
  }
}