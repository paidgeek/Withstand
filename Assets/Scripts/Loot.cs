using UnityEngine;

public abstract class Loot : MonoBehaviour
{
  private Rigidbody m_Rigidbody;
  private Transform m_Player;

  private void Start()
  {
    m_Rigidbody = GetComponent<Rigidbody>();
    m_Player = Player.instance.transform;
  }

  private void Update()
  {
    if (m_Player) {
      var d = m_Player.position - transform.position;

      if (d.sqrMagnitude < 0.2f) {
        OnPickup();
      } else if (d.sqrMagnitude < 5.0f) {
        m_Rigidbody.isKinematic = true;
        transform.position += (m_Player.position - transform.position) * 10.0f * Time.deltaTime;
      } else {
        m_Rigidbody.isKinematic = false;
      }
    }
  }

  protected abstract void OnPickup();
}