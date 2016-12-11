using UnityEngine;

[RequireComponent(typeof (Gravity), typeof (Rigidbody))]
public class ApplyGravity : MonoBehaviour
{
  private Gravity m_Gravity;
  private Rigidbody m_Rigidbody;

  private void Awake()
  {
    m_Gravity = GetComponent<Gravity>();
    m_Rigidbody = GetComponent<Rigidbody>();
  }

  private void FixedUpdate()
  {
    m_Rigidbody.AddForce(m_Gravity.direction * m_Gravity.multiplier * Time.fixedDeltaTime, ForceMode.VelocityChange);
  }
}