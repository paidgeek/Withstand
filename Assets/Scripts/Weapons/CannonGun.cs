using UnityEngine;

public class CannonGun : Weapon
{
  [SerializeField] private CannonBall m_CannonBallPrefab;

  private void Start()
  {
    m_CannonBallPrefab.CreatePool(5);
  }

  public override void Fire()
  {
    m_Muzzle.Flash();
    m_CannonBallPrefab.Spawn(m_Muzzle.transform.position, m_Muzzle.transform.rotation);
  }
}