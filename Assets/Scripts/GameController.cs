using UnityEngine;
using UnityEngine.Events;

public class GameController : Singleton<GameController>
{
  public static int score { get; private set; }
  [SerializeField] private UnityEvent m_OnEarnPixel;
  [SerializeField] private UnityEvent m_OnGameOver;

  private void EarnPixelImpl()
  {
    score++;
    if (m_OnEarnPixel != null) {
      m_OnEarnPixel.Invoke();
    }
  }

  public static void EarnPixel()
  {
    instance.EarnPixelImpl();
  }

  public static void EndGame()
  {
    instance.m_OnGameOver.Invoke();
  }
}