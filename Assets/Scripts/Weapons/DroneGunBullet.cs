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
    var impact = m_ImpactPrefab.Spawn(hit.point + hit.normal * 0.1f);

    if (target != null) {
      target.OnShot(Random.Range(1.0f, 2.0f));
    } else {
      impact.GetComponent<AudioSource>().Play();      
    }

    this.Recycle();
  }
}

