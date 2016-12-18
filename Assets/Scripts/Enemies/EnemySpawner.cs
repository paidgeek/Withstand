using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
  [SerializeField] private Transform[] m_EnemyPrefabs;
  private float m_Rate;

  private void Start()
  {
    m_Rate = 8.0f;
    StartCoroutine(SpawnerCoroutine());
  }

  public void OnGameOver()
  {
    StopAllCoroutines();
  }

  private IEnumerator SpawnerCoroutine()
  {
    yield return new WaitForSeconds(2.0f);

    while (true) {
      m_Rate = Mathf.Clamp(m_Rate - 0.06f, 3.0f, 8.0f);
      Spawn();
      yield return new WaitForSeconds(m_Rate);
    }
  }

  private void Spawn()
  {
    var pos = Random.onUnitSphere * 13.0f;
    Instantiate(m_EnemyPrefabs[Random.Range(0, m_EnemyPrefabs.Length)], pos, Quaternion.identity);
  }
}