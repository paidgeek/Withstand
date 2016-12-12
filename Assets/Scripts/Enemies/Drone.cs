using UnityEngine;

public class Drone : Enemy, ITarget
{
  private static readonly float s_MoveSpeed = 100.0f;
  [SerializeField] private float m_HoverHeight;
  [SerializeField] private float m_HoverForce;
  private Gravity m_Gravity;
  private Rigidbody m_Rigidbody;
  private float m_Health;
  [SerializeField] private DroneGun m_Gun;
  private float m_GunTimer;
  [SerializeField] private Transform m_Pivot;
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
        m_Pivot.localRotation = Quaternion.LookRotation(t.normalized, -m_Gravity.direction);

        if (d.sqrMagnitude < 250.0f) {
          Fire();
        }
      } else {
        m_Pivot.localRotation = Quaternion.LookRotation(m_MoveDirection, -m_Gravity.direction);
      }

      if (Vector3.Distance(player.transform.position, transform.position) < 30.0f) {
        m_MoveDirection = Vector3.ProjectOnPlane((player.transform.position - transform.position).normalized, -m_Gravity.direction).normalized;
      }
    }
  }

  private void Fire()
  {
    m_GunTimer -= Time.deltaTime;
    if (m_GunTimer <= 0.0f) {
      m_GunTimer = 0.5f;
      m_Gun.Fire();
    }
  }

  private void FixedUpdate()
  {
    m_Rigidbody.AddForce(m_MoveDirection * s_MoveSpeed * Time.fixedDeltaTime, ForceMode.Acceleration);

    if (m_Gravity.altitude < m_HoverHeight) {
      var f = (m_HoverHeight - m_Gravity.altitude) / m_HoverHeight * m_HoverForce;
      m_Rigidbody.AddForce(-m_Gravity.direction * m_Gravity.multiplier * f * Time.fixedDeltaTime, ForceMode.VelocityChange);
    } else {
      m_Rigidbody.AddForce(m_Gravity.direction * m_Gravity.multiplier * Time.fixedDeltaTime, ForceMode.Impulse);
    }
  }
}