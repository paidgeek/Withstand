using UnityEngine;

public class DroneGun : Weapon
{
  [SerializeField] private DroneGunBullet m_BulletPrefab;
  [SerializeField] private AudioSource m_FireAudioSource;

  private void Start()
  {
    m_BulletPrefab.CreatePool(10);
  }

  public override void Fire()
  {
    m_Muzzle.Flash();
    m_BulletPrefab.Spawn(m_Muzzle.transform.position, m_Muzzle.transform.rotation);
    m_FireAudioSource.Play();
  }
}