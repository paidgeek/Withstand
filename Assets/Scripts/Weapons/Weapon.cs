using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
  [SerializeField] protected Muzzle m_Muzzle;

  public Muzzle muzzle
  {
    get
    {
      return m_Muzzle;
    }
  }

  public abstract void Fire();
}