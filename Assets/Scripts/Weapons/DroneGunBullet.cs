using UnityEngine;

public class DroneGunBullet : Bullet
{
  [SerializeField] private DroneGunBulletImpact m_ImpactPrefab;

  private void Start()
  {
    m_ImpactPrefab.CreatePool(5);
  }

  protected override void OnHit(RaycastHit hit)
  {
    var target = hit.collider.GetComponent<ITarget>();
    if (target != null) {
      target.OnShot(weapon);
    }

    m_ImpactPrefab.Spawn(hit.point + hit.normal * 0.1f);
    this.Recycle();
  }
}

