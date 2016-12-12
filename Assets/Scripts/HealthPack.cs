public class HealthPack : Loot
{
  protected override void OnPickup()
  {
    this.Recycle();
    Player.PickupHealthPack();
  }

  protected override void OnTimeout()
  {
    this.Recycle();
  }
}