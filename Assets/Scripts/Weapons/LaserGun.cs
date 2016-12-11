using System.Collections;
using UnityEngine;

public class LaserGun : Weapon
{
  [SerializeField] private LayerMask m_HitLayer;
  [SerializeField] private LaserGunImpact m_ImpactPrefab;
  [SerializeField] private LineRenderer m_LineRenderer;
  private float m_Timer;

  private void Start()
  {
    m_ImpactPrefab.CreatePool(10);
  }

  public override void Fire()
  {
    m_Timer -= Time.deltaTime;
    if (m_Timer <= 0.0f) {
      m_Timer = 0.15f;

      var origin = m_Muzzle.origin;
      var direction = Quaternion.Euler(Random.Range(-2.0f, 2.0f), Random.Range(-2.0f, 2.0f), Random.Range(-2.0f, 2.0f)) * m_Muzzle.direction;

      m_Muzzle.Flash();

      RaycastHit hit;
      if (Physics.Raycast(origin, direction, out hit, 100.0f, m_HitLayer)) {
        m_ImpactPrefab.Spawn(hit.point + hit.normal * 0.1f);

        m_LineRenderer.SetPosition(0, m_Muzzle.transform.position);
        m_LineRenderer.SetPosition(1, hit.point);
        m_LineRenderer.enabled = true;

        StartCoroutine(HideLine());

        var target = hit.collider.GetComponent<ITarget>();
        if (target != null) {
          target.OnShot(Random.Range(1.0f, 2.0f));
        }
      }
    }
  }

  public void EndFire()
  {
    m_Timer = 0.0f;
  }

  private IEnumerator HideLine()
  {
    yield return new WaitForEndOfFrame();
    m_LineRenderer.enabled = false;
  }
}