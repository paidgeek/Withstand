public class Shield : Loot
{
  protected override void OnPickup()
  {
    this.Recycle();
    Player.PickupShield();
  }

  protected override void OnTimeout()
  {
    this.Recycle();
  }
}