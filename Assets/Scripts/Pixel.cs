public class Pixel : Loot
{
  protected override void OnPickup()
  {
    this.Recycle();
    GameController.EarnPixel();
  }
}