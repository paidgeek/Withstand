using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
  [SerializeField] private Text m_OrderText;
  [SerializeField] private Text m_NameText;
  [SerializeField] private Text m_ScoreText;

  public int order
  {
    set
    {
      m_OrderText.text = "#" + value.ToString("N0");
    }
  }

  public string playerName
  {
    set
    {
      m_NameText.text = value.Trim();
    }
  }

  public int score
  {
    set
    {
      m_ScoreText.text = value.ToString("N0");
    }
  }
}