using DigitalRuby.Tween;
using UnityEngine;

public class LeaderboardController : Singleton<LeaderboardController>
{
  [SerializeField] private PlayerScore m_PlayerScorePrefab;
  [SerializeField] private RectTransform m_Content;

  private void OnEnable()
  {
    for (var i = 0; i < m_Content.childCount; i++) {
      Destroy(m_Content.GetChild(i).gameObject);
    }

    Backend.GetScores(scores =>
    {
      if (scores == null || scores.Count == 0) {} else {
        for (var i = 0; i < scores.Count; i++) {
          var s = scores[i];
          var playerScore = Instantiate(m_PlayerScorePrefab, m_Content);
          playerScore.order = i + 1;
          playerScore.playerName = s.name;
          playerScore.score = s.score;
        }
      }
    });
  }

  public void OnBackClick()
  {
    MenuController.Show();
    Hide();
  }

  public static void Show()
  {
    var cg = instance.GetComponent<CanvasGroup>();
    var rt = instance.GetComponent<RectTransform>();

    TweenFactory.Tween("ShowLeaderboard", 0.0f, 1.0f, 0.5f, TweenScaleFunctions.CubicEaseInOut, t => {
      cg.alpha = t.CurrentValue;
      rt.anchoredPosition = Vector2.Lerp(new Vector2(2000.0f, 0.0f), Vector2.zero, t.CurrentValue);
    }, t => {
      cg.interactable = true;
      cg.blocksRaycasts = true;
    });
  }

  public static void Hide()
  {
    var cg = instance.GetComponent<CanvasGroup>();
    var rt = instance.GetComponent<RectTransform>();
    cg.interactable = false;
    cg.blocksRaycasts = false;

    TweenFactory.Tween("HideLeaderboard", 1.0f, 0.0f, 0.5f, TweenScaleFunctions.CubicEaseInOut, t => {
      cg.alpha = t.CurrentValue;
      rt.anchoredPosition = Vector2.Lerp(new Vector2(2000.0f, 0.0f), Vector2.zero, t.CurrentValue);
    }, t => { });
  }
}