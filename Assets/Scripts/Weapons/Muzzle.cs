using UnityEngine;

public class Muzzle : MonoBehaviour
{
  [SerializeField] private Light m_Light;
  private bool m_Flashing;
  private float m_Timer;

  public Vector3 origin
  {
    get
    {
      return transform.position;
    }
  }

  public Vector3 direction
  {
    get
    {
      return transform.forward;
    }
  }

  public void Flash()
  {
    m_Flashing = true;
  }

  private void Update()
  {
    if (m_Flashing) {
      m_Timer += Time.deltaTime;

      if (m_Light) {
        m_Light.intensity = ((0.1f - m_Timer) / 0.1f) * 4.0f;
      }

      if (m_Timer >= 0.1f) {
        m_Timer = 0.0f;
        m_Flashing = false;
      }
    }
  }
}