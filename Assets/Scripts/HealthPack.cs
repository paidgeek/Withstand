public class HealthPack : Loot
{
  protected override void OnPickup()
  {
    this.Recycle();
    Player.PickupHealthPack();
  }
}