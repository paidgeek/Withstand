using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
  [SerializeField] private Renderer[] m_Renderers;
  [SerializeField] protected Transform m_DeathEffect;
  [SerializeField] private AudioSource m_HurtAudioSource;
  private float m_HurtTimer;

  protected void Hurt()
  {
    m_HurtAudioSource.Play();
    m_HurtTimer = 0.15f;
  }

  protected void Update()
  {
    if (m_HurtTimer >= 0.0f) {
      m_HurtTimer -= Time.deltaTime;
      var t = Mathf.Sin((m_HurtTimer / 0.15f) * Mathf.PI);

      if (m_HurtTimer <= 0.0f) {
        t = 0.0f;
      }

      for (var i = 0; i < m_Renderers.Length; i++) {
        m_Renderers[i].material.color = Color.Lerp(Color.white, Color.red, t);
      }
    }
  }
}