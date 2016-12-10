using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : Singleton<MenuController>
{
  public void OnTwitterClick()
  {
    Application.OpenURL("https://twitter.com/paidgeek");
  }

  public void OnOptionsClick() {}

  public void OnPlayClick()
  {
    SceneManager.LoadScene("Game");
  }
}