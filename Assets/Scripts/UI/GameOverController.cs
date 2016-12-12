using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
  [SerializeField] private GameObject m_ZeroScore;
  [SerializeField] private GameObject m_SomeScore;
  [SerializeField] private Text m_ScoreText;
  [SerializeField] private InputField m_NameField;
  [SerializeField] private InputField m_PasswordField;
  [SerializeField] private Text m_ErrorText;

  public void OnGameOver()
  {
    if (GameController.score == 0) {
      m_ZeroScore.SetActive(true);
    } else {
      m_SomeScore.SetActive(true);
      m_ScoreText.text = GameController.score.ToString("N0");
    }

    var cg = GetComponent<CanvasGroup>();
    cg.alpha = 1.0f;
    cg.interactable = true;
    cg.blocksRaycasts = true;
  }

  public void OnPostClick()
  {
    Backend.PostScore(m_NameField.text, m_PasswordField.text, GameController.score, error =>
    {
      if (!string.IsNullOrEmpty(error)) {
        m_ErrorText.gameObject.SetActive(true);
        m_ErrorText.text = error;
      } else {
        m_ErrorText.gameObject.SetActive(false);
        OnSkipClick();
      }
    });
  }

  public void OnSkipClick()
  {
    SceneManager.LoadScene("Menu");
  }
}