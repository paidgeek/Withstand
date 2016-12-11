using UnityEngine;

public class CannonBallExplosion : MonoBehaviour
{
  private void Start()
  {
    Invoke("RecycleInvokable", 2.0f);
  }

  private void RecycleInvokable()
  {
    this.Recycle();
  }
}