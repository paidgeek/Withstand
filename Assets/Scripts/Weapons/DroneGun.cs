using UnityEngine;

public class DroneGun : Weapon
{
  [SerializeField] private DroneGunBullet m_BulletPrefab;

  private void Start()
  {
    m_BulletPrefab.CreatePool(10);
  }

  public override void Fire()
  {
    m_Muzzle.Flash();
    m_BulletPrefab.Spawn(m_Muzzle.transform.position, m_Muzzle.transform.rotation);
  }
}