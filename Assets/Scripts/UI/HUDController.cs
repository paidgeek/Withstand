using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
  [SerializeField] private Text m_ScoreText;
  [SerializeField] private Image m_HealthFill;
  [SerializeField] private Image m_HurtOverlay;
  [SerializeField] private Image m_ShieldFill;
  private float m_ShieldTimer;

  public void OnEarnPixel()
  {
    m_ScoreText.text = GameController.score.ToString("N0");
  }

  public void OnHealthChange()
  {
    var f = Player.health / Player.maxHealth;
    m_HealthFill.fillAmount = f;

    m_HurtOverlay.color = new Color(1.0f, 0.0f, 0.0f, Mathf.Clamp01(0.5f - f) * 0.3f);
  }

  public void OnGameOver()
  {
    var cg = GetComponent<CanvasGroup>();
    cg.alpha = 0.0f;
  }

  private void Update()
  {
    if (m_ShieldTimer > 0.0f) {
      m_ShieldFill.fillAmount = m_ShieldTimer / 10.0f;
      m_ShieldTimer -= Time.deltaTime;
    } else {
      m_ShieldFill.fillAmount = 0.0f;
    }
  }

  public void OnShieldEnable()
  {
    m_ShieldTimer = 10.0f;
  }
}