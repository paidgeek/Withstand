using DigitalRuby.Tween;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : Singleton<MenuController>
{
  public void OnTwitterClick()
  {
    Application.OpenURL("https://twitter.com/paidgeek");
  }

  public void OnLeaderboardClick()
  {
    LeaderboardController.Show();
    Hide();
  }

  public void OnOptionsClick()
  {
    OptionsController.Show();
    Hide();
  }

  public static void Show()
  {
    var cg = instance.GetComponent<CanvasGroup>();
    var rt = instance.GetComponent<RectTransform>();

    TweenFactory.Tween("ShowMenu", 0.0f, 1.0f, 0.5f, TweenScaleFunctions.CubicEaseInOut, t =>
    {
      cg.alpha = t.CurrentValue;
      rt.anchoredPosition = Vector2.Lerp(new Vector2(-2000.0f, 0.0f), Vector2.zero, t.CurrentValue);
    }, t =>
    {
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

    TweenFactory.Tween("HideMenu", 1.0f, 0.0f, 0.5f, TweenScaleFunctions.CubicEaseInOut, t =>
    {
      cg.alpha = t.CurrentValue;
      rt.anchoredPosition = Vector2.Lerp(new Vector2(-2000.0f, 0.0f), Vector2.zero, t.CurrentValue);
    }, t => {});
  }

  public void OnPlayClick()
  {
    SceneManager.LoadScene("Game");
  }

  public void OnQuitClick()
  {
    Application.Quit();
  }
}