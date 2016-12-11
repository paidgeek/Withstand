using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
  [SerializeField] private Transform[] m_EnemyPrefabs;

  private void Start()
  {
    StartCoroutine(SpawnerCoroutine());
  }

  public void OnGameOver()
  {
    StopAllCoroutines();
  }

  private IEnumerator SpawnerCoroutine()
  {
    while (true) {
      yield return new WaitForSeconds(5.0f);
      Spawn();
    }
  }

  private void Spawn()
  {
    var pos = Random.onUnitSphere * 13.0f;
    Instantiate(m_EnemyPrefabs[Random.Range(0, m_EnemyPrefabs.Length)], pos, Quaternion.identity);
  }
}