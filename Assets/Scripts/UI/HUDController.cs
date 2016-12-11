using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
  [SerializeField] private Text m_ScoreText;
  [SerializeField] private Image m_HealthFill;
  [SerializeField] private Image m_HurtOverlay;

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
}