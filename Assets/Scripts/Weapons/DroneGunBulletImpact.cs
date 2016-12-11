public class DroneGunBulletImpact : Impact
{
  private void Start()
  {
    Invoke("RecycleInvokable", 0.5f);
  }

  private void RecycleInvokable()
  {
    this.Recycle();
  }
}