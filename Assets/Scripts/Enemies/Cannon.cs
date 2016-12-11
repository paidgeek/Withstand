using UnityEngine;

public class Cannon : Enemy, ITarget
{
  private static readonly float s_RotationSpeed = 9.0f;
  private static readonly float s_MoveSpeed = 100.0f;
  [SerializeField]
  private float m_HoverHeight;
  [SerializeField]
  private float m_HoverForce;
  private Gravity m_Gravity;
  private Rigidbody m_Rigidbody;
  private Quaternion m_TargetRotation;
  private float m_Health;
  [SerializeField]
  private CannonGun m_Gun;
  private float m_GunTimer;
  private Vector3 m_MoveDirection;

  public void OnShot(float damage)
  {
    m_Health -= damage;
    Hurt();

    if (m_Health <= 0.0f) {
      LootManager.Spawn(transform.position);

      Instantiate(m_DeathEffect, transform.position, Quaternion.identity);
      Destroy(gameObject);
    }
  }

  private void Awake()
  {
    m_Gravity = GetComponent<Gravity>();
    m_Rigidbody = GetComponent<Rigidbody>();

    m_Health = 5;
  }

  private void Start()
  {
    m_MoveDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
  }

  protected new void Update()
  {
    base.Update();

    var player = Player.instance;
    if (player) {
      var d = player.transform.position - transform.position;

      if (d.sqrMagnitude < 300.0f) {
        var t = player.transform.position + player.rigidbody.velocity * 0.03f - m_Gun.muzzle.transform.position;

        if (d.sqrMagnitude < 250.0f) {
          Fire();
        }
      }

      if (Vector3.Distance(player.transform.position, transform.position) < 20.0f) {
        m_MoveDirection = Vector3.ProjectOnPlane((player.transform.position - transform.position).normalized, -m_Gravity.direction).normalized;
      }
    }
  }

  private void Fire()
  {
    m_GunTimer -= Time.deltaTime;
    if (m_GunTimer <= 0.0f) {
      m_GunTimer = 2.0f;
      m_Gun.transform.LookAt(Player.instance.transform.position);
      m_Gun.Fire();
    }
  }

  private void FixedUpdate()
  {
    m_TargetRotation = Quaternion.FromToRotation(m_TargetRotation * Vector3.up, -m_Gravity.direction) * m_TargetRotation;
    transform.rotation = Quaternion.Slerp(transform.rotation, m_TargetRotation, s_RotationSpeed * Time.fixedDeltaTime);

    m_Rigidbody.AddForce(m_MoveDirection * s_MoveSpeed * Time.fixedDeltaTime, ForceMode.Acceleration);

    if (m_Gravity.altitude < m_HoverHeight) {
      var f = (m_HoverHeight - m_Gravity.altitude) / m_HoverHeight * m_HoverForce;
      m_Rigidbody.AddForce(-m_Gravity.direction * m_Gravity.multiplier * f * Time.fixedDeltaTime, ForceMode.VelocityChange);
    } else {
      m_Rigidbody.AddForce(m_Gravity.direction * m_Gravity.multiplier * Time.fixedDeltaTime, ForceMode.Impulse);
    }
  }

}