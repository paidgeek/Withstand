using UnityEngine;

public class Shake : MonoBehaviour
{
  [SerializeField] private float m_Amplitude;
  [SerializeField] private float m_LerpSpeed;

  private float m_Timer;
  private Vector3 m_StartPosition;

  private void Start()
  {
    m_StartPosition = transform.localPosition;
  }

  private void Update()
  {
    if (m_Timer > 0.0f) {
      m_Timer -= Time.deltaTime;

      var d = Random.onUnitSphere * m_Timer * m_Amplitude;
      transform.localPosition = Vector3.Lerp(m_StartPosition, m_StartPosition + d, m_LerpSpeed * Time.deltaTime);
    }
  }

  public void Push(float amount)
  {
    m_Timer = amount;
  }
}
