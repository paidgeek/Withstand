using System.Collections;
using UnityEngine;

public class LaserGun : Weapon
{
  [SerializeField] private LayerMask m_HitLayer;
  [SerializeField] private LaserGunImpact m_ImpactPrefab;
  [SerializeField] private LineRenderer m_LineRenderer;
  [SerializeField] private Transform m_PowerIndicator;
  [SerializeField] private AudioSource m_ShootAudioSource;
  [SerializeField] private AudioSource m_OutOfPowerAudioSource;
  private float m_Timer;
  private float m_FireKick;
  private float m_Power;
  private float m_RefillPowerTimer;

  private void Start()
  {
    m_ImpactPrefab.CreatePool(10);
    m_Power = 1.0f;
  }

  public override void Fire()
  {
    m_Timer -= Time.deltaTime;
    if (m_Timer <= 0.0f) {
      m_Timer = 0.15f;
      m_FireKick = -0.1f;

      m_RefillPowerTimer = 0.5f;

      if (m_Power <= 0.0f) {
        m_OutOfPowerAudioSource.Play();
      } else {
        m_Power -= 0.03f;
        PerformFire();
      }
    }
  }

  private void PerformFire()
  {
    var origin = m_Muzzle.origin;
    var direction = Quaternion.Euler(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)) * m_Muzzle.direction;

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

    m_ShootAudioSource.Play();
  }

  private void Update()
  {
    if (m_FireKick < 0.0f) {
      m_FireKick += Time.deltaTime * 1.0f;
      transform.localPosition = new Vector3(0.0f, 0.0f, m_FireKick);
    } else {
      transform.localPosition = Vector3.zero;
    }

    if (m_RefillPowerTimer <= 0.0f) {
      m_Power = Mathf.Clamp01(m_Power + 0.3f * Time.deltaTime);
    } else {
      m_RefillPowerTimer -= Time.deltaTime;
    }

    m_PowerIndicator.localScale = new Vector3(0.01f, 0.02f, 0.1f * m_Power);
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