using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
  private static readonly float s_Speed = 70.0f;
  [SerializeField] private LayerMask m_HitLayer;

  private void Update()
  {
    RaycastHit hit;
    if (Physics.Raycast(transform.position, transform.forward, out hit, s_Speed * Time.deltaTime, m_HitLayer)) {
      OnHit(hit);
    } else {
      transform.Translate(transform.forward * s_Speed * Time.deltaTime, Space.World);
    }
  }

  protected abstract void OnHit(RaycastHit hit);
}