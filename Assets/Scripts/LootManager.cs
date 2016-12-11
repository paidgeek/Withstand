using UnityEngine;

public class LootManager : Singleton<LootManager>
{
  [SerializeField] private Pixel m_PixelPrefab;
  [SerializeField] private HealthPack m_HealthPackPrefab;
  [SerializeField] private Shield m_ShieldPrefab;

  private void Start()
  {
    m_PixelPrefab.CreatePool(10);
    m_HealthPackPrefab.CreatePool(3);
    m_ShieldPrefab.CreatePool(3);
  }

  public static void Spawn(Vector3 position)
  {
    SpawnPixels(position);
    if (Random.value < 0.15f) {
      SpawnExtra(position);
    }
  }

  private static void SpawnPixels(Vector3 position)
  {
    for (var i = 0; i < Random.Range(3, 10); i++) {
      var pixel = instance.m_PixelPrefab.Spawn(position + Random.onUnitSphere * 0.15f);
      pixel.GetComponent<Rigidbody>().AddExplosionForce(50.0f, position, 1.0f);
    }
  }

  private static void SpawnExtra(Vector3 position)
  {
    if (Random.Range(0, 2) == 0) {
      var hp = instance.m_HealthPackPrefab.Spawn(position);
      hp.GetComponent<Rigidbody>().AddExplosionForce(50.0f, position, 1.0f);
    } else {
      var s = instance.m_ShieldPrefab.Spawn(position);
      s.GetComponent<Rigidbody>().AddExplosionForce(50.0f, position, 1.0f);
    }
  }
}