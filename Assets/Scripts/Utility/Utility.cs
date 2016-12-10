using UnityEngine;

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
}