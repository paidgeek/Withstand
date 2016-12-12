public class Pixel : Loot
{
  protected override void OnPickup()
  {
    this.Recycle();
    Player.PickupPixel();
  }

  protected override void OnTimeout()
  {
    this.Recycle();
  }
}