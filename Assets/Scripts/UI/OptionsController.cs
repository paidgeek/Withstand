using DigitalRuby.Tween;
using UnityEngine;

public class OptionsController : Singleton<OptionsController>
{
  public void OnBackClick()
  {
    MenuController.Show();
    Hide();
  }

  public static void Show()
  {
    var cg = instance.GetComponent<CanvasGroup>();
    var rt = instance.GetComponent<RectTransform>();

    TweenFactory.Tween("ShowOptions", 0.0f, 1.0f, 0.5f, TweenScaleFunctions.CubicEaseInOut, t => {
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

    TweenFactory.Tween("HideOptions", 1.0f, 0.0f, 0.5f, TweenScaleFunctions.CubicEaseInOut, t => {
      cg.alpha = t.CurrentValue;
      rt.anchoredPosition = Vector2.Lerp(new Vector2(2000.0f, 0.0f), Vector2.zero, t.CurrentValue);
    }, t => { });
  }
}