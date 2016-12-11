using DigitalRuby.Tween;
using UnityEngine;

public class StartGameTitle : MonoBehaviour
{
  [SerializeField] private RectTransform m_Title;

  private void Start()
  {
    var cg = GetComponent<CanvasGroup>();
    var a = new Vector2(-2000.0f, 0.0f);
    var b = Vector2.zero;

    TweenFactory.Tween("ShowTitle", 0.0f, 1.0f, 0.4f, TweenScaleFunctions.SineEaseIn, t =>
    {
      cg.alpha = t.CurrentValue;
      m_Title.anchoredPosition = Vector2.Lerp(a, b, t.CurrentValue);
    }, t =>
    {
      Invoke("HideInvokable", 1.0f);
    });
  }

  private void HideInvokable()
  {
    var cg = GetComponent<CanvasGroup>();
    var b = Vector2.zero;
    var c = new Vector2(2000.0f, 0.0f);
    TweenFactory.Tween("HideTitle", 1.0f, 0.0f, 0.4f, TweenScaleFunctions.SineEaseOut, t =>
    {
      cg.alpha = t.CurrentValue;
      m_Title.anchoredPosition = Vector2.Lerp(c, b, t.CurrentValue);
    }, t => {});
  }
}