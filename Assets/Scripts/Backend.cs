using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

[Serializable]
public class BackendPlayer
{
  public string name;
  public int score;

  public BackendPlayer(string name = "", int score = 0)
  {
    this.name = name;
    this.score = score;
  }
}

public class Backend : Singleton<Backend>
{
  private static readonly string s_Url = "https://withstandleaderboard.appspot.com/scores";

  public static void PostScore(string name, string password, int score, UnityAction<string> callback)
  {
    if (name.Length < 4) {
      callback.Invoke("Name must be at least 4 characters long");
    } else if (name.Length > 12) {
      callback.Invoke("Name is too long");
    } else if (password.Length < 4) {
      callback.Invoke("Password must be at least 4 characters long");
    } else if (password.Length > 32) {
      callback.Invoke("Password is too long");
    } else {
      instance.StartCoroutine(instance.PostScoreCoroutine(name, password, score, callback));
    }
  }

  private IEnumerator PostScoreCoroutine(string name, string password, int score, UnityAction<string> callback)
  {
    var url = string.Format("/{0}/{1}/{2}", Utility.ToBase64(name), Utility.ToBase64(password), score);
    var www = UnityWebRequest.Post(url, "42");
    www.SetRequestHeader("Content-Type", "application/json");
    www.SetRequestHeader("Cache-Control", "max-age=0, no-cache, no-store");
    www.SetRequestHeader("Pragma", "no-cache");
    yield return www.Send();

    if (www.isError) {
      Debug.LogError(www.error);
      callback.Invoke("");
    } else {
      var result = JsonUtility.FromJson<EmptyResult>(www.downloadHandler.text);
      callback.Invoke(result.error);
    }
  }

  public static void GetScores(UnityAction<List<BackendPlayer>> callback)
  {
    instance.StartCoroutine(instance.GetScoresCoroutine(callback));
  }

  private IEnumerator GetScoresCoroutine(UnityAction<List<BackendPlayer>> callback)
  {
    var www = UnityWebRequest.Get(s_Url);
    www.SetRequestHeader("Content-Type", "application/json");
    www.SetRequestHeader("Cache-Control", "max-age=0, no-cache, no-store");
    www.SetRequestHeader("Pragma", "no-cache");
    yield return www.Send();

    if (www.isError) {
      Debug.LogError(www.error);
      callback.Invoke(null);
    } else {
      var result = JsonUtility.FromJson<GetScoresResult>(www.downloadHandler.text);
      callback.Invoke(result.players);
    }
  }

  [Serializable]
  public class GetScoresResult
  {
    public List<BackendPlayer> players;
  }

  [Serializable]
  public class EmptyResult
  {
    public string error;
  }
}