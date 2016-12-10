using UnityEngine;

public class WeaponController : Singleton<WeaponController>
{
  [SerializeField]
  private Weapon m_CurrentWeapon;

  public void Fire()
  {
    m_CurrentWeapon.Fire();
  }
}