using DigitalRuby.Tween;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : Singleton<OptionsController>
{
  [SerializeField] private Slider m_SoundSlider;
  [SerializeField] private Slider m_FovSlider;
  [SerializeField] private Text m_FovText;
  [SerializeField] private Slider m_MouseSensitivity;

  public void OnBackClick()
  {
    MenuController.Show();
    Hide();
  }

  public static void Show()
  {
    instance.m_SoundSlider.value = PlayerPrefs.GetFloat("SoundVolume", 1.0f);
    instance.m_FovSlider.value = PlayerPrefs.GetInt("Fov", 70);
    instance.m_FovText.text = PlayerPrefs.GetInt("Fov", 70).ToString();
    instance.m_MouseSensitivity.value = PlayerPrefs.GetFloat("MouseSensitivity", 1.0f);

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

  public void OnSoundChange()
  {
    var value = m_SoundSlider.value;
    PlayerPrefs.SetFloat("SoundVolume", value);
    PlayerPrefs.Save();

    AudioListener.volume = value;
  }

  public void OnFovChange()
  {
    var value = (int) m_FovSlider.value;
    PlayerPrefs.SetInt("Fov", value);
    PlayerPrefs.Save();
    instance.m_FovText.text = value.ToString();
  }

  public void OnMouseSensitivityChange()
  {
    var value = m_MouseSensitivity.value;
    PlayerPrefs.SetFloat("MouseSensitivity", value);
    PlayerPrefs.Save();
  }
}