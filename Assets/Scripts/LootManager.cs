using UnityEngine;

public class LootManager : Singleton<LootManager>
{
  [SerializeField] private Pixel m_PixelPrefab;
  [SerializeField] private HealthPack m_HealthPackPrefab;

  private void Start()
  {
    m_PixelPrefab.CreatePool(10);
    m_HealthPackPrefab.CreatePool(3);
  }

  public static void SpawnPixels(Vector3 position)
  {
    for (var i = 0; i < Random.Range(3, 10); i++) {
      var pixel = instance.m_PixelPrefab.Spawn(position + Random.onUnitSphere * 0.15f);
      pixel.GetComponent<Rigidbody>().AddExplosionForce(50.0f, position, 1.0f);
    }
  }

  public static void SpawnHealthPack(Vector3 position)
  {
    var hp = instance.m_HealthPackPrefab.Spawn(position);
    hp.GetComponent<Rigidbody>().AddExplosionForce(50.0f, position, 1.0f);
  }
}