using UnityEngine;

[RequireComponent(typeof (Rigidbody))]
public class Gravity : MonoBehaviour
{
  [SerializeField] private float m_Multiplier;
  private Rigidbody m_RigidBody;
  private Vector3 m_LastSwitchPosition;
  public Vector3 direction { get; private set; }

  public float multiplier
  {
    get
    {
      return m_Multiplier;
    }
    set
    {
      m_Multiplier = value;
    }
  }

  private void Start()
  {
    m_RigidBody = GetComponent<Rigidbody>();
  }

  private void Update()
  {
    var p = transform.position;

    if ((p - m_LastSwitchPosition).sqrMagnitude > 1.0f) {
      var ap = p.Abs();
      var d = Vector3.zero;

      if (ap.x > ap.y && ap.x > ap.z) {
        d = p.x < 0.0f ? Vector3.left : Vector3.right;
      } else if (ap.y > ap.z) {
        d = p.y < 0.0f ? Vector3.down : Vector3.up;
      } else {
        d = p.z < 0.0f ? Vector3.back : Vector3.forward;
      }

      if (direction != d) {
        m_LastSwitchPosition = p;
      }

      direction = d;
    }
  }

  private void FixedUpdate()
  {
    m_RigidBody.AddForce(direction * m_Multiplier * Time.fixedDeltaTime, ForceMode.VelocityChange);
  }
}