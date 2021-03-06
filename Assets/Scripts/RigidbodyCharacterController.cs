﻿using UnityEngine;

[RequireComponent(typeof (Rigidbody))]
[RequireComponent(typeof (CapsuleCollider))]
[RequireComponent(typeof (Gravity))]
public class RigidbodyCharacterController : MonoBehaviour
{
  private static readonly float s_GroundCheckDistance = 0.01f;
  private static readonly float s_StickToGroundHelperDistance = 0.6f;
  private static readonly float s_SlowDownRate = 40.0f;
  private static readonly float s_ShellOffset = 0.0f;
  private static readonly float s_RotationSpeed = 9.0f;
  private CapsuleCollider m_CapsuleCollider;
  private float m_CurrentTargetSpeed;
  [SerializeField] private float m_Drag;
  private Vector3 m_GroundContactNormal;
  private Vector2 m_InputDirection;
  private bool m_Jump, m_Jumping;
  [SerializeField] private float m_JumpPower;
  private bool m_PreviouslyGrounded;
  private Gravity m_Gravity;
  private Rigidbody m_Rigidbody;
  public AnimationCurve m_SlopeCurveModifier = new AnimationCurve(new Keyframe(-90.0f, 1.0f), new Keyframe(0.0f, 1.0f), new Keyframe(90.0f, 0.0f));
  [Header("Properties")] [SerializeField] private float m_Speed;
  private Quaternion m_TargetRotation;
  [SerializeField] private Transform m_View;
  public bool grounded { get; private set; }

  public Vector3 velocity
  {
    get
    {
      return m_Rigidbody.velocity;
    }
  }

  private void Awake()
  {
    m_Rigidbody = GetComponent<Rigidbody>();
    m_CapsuleCollider = GetComponent<CapsuleCollider>();
    m_Gravity = GetComponent<Gravity>();
  }

  private void Start()
  {
    m_TargetRotation = transform.rotation;
  }

  public bool Jump()
  {
    if (!m_Jumping) {
      m_Jump = true;
      return true;
    }
    return false;
  }

  public void Move(Vector2 direction)
  {
    m_InputDirection = direction;
  }

  private void FixedUpdate()
  {
    m_TargetRotation = Quaternion.FromToRotation(m_TargetRotation * Vector3.up, -m_Gravity.direction) * m_TargetRotation;
    transform.rotation = Quaternion.Slerp(transform.rotation, m_TargetRotation, s_RotationSpeed * Time.fixedDeltaTime);

    GroundCheck();
    UpdateDesiredTargetSpeed();

    if ((Mathf.Abs(m_InputDirection.x) > float.Epsilon || Mathf.Abs(m_InputDirection.y) > float.Epsilon)) {
      var desiredMove = m_View.transform.forward * m_InputDirection.y + m_View.transform.right * m_InputDirection.x;
      desiredMove = Vector3.ProjectOnPlane(desiredMove, m_Gravity.direction).normalized;

      desiredMove.x = desiredMove.x * m_CurrentTargetSpeed;
      desiredMove.z = desiredMove.z * m_CurrentTargetSpeed;
      desiredMove.y = desiredMove.y * m_CurrentTargetSpeed;

      if (m_Rigidbody.velocity.sqrMagnitude < m_CurrentTargetSpeed * m_CurrentTargetSpeed) {
        m_Rigidbody.AddForce(desiredMove * SlopeMultiplier(), ForceMode.Impulse);
      }
    }

    if (grounded) {
      m_Rigidbody.drag = 5.0f;

      if (m_Jump) {
        m_Rigidbody.drag = 0.0f;
        m_Rigidbody.AddForce(-m_Gravity.direction * m_JumpPower * 50.0f, ForceMode.Impulse);
        m_Jumping = true;
        m_Jump = false;
      }

      if (!m_Jumping && Mathf.Abs(m_InputDirection.x) < float.Epsilon && Mathf.Abs(m_InputDirection.y) < float.Epsilon && m_Rigidbody.velocity.magnitude < 1.0f) {
        m_Rigidbody.Sleep();
      }
    } else {
      m_Rigidbody.drag = 0.0f;

      if (m_PreviouslyGrounded && !m_Jumping) {
        StickToGroundHelper();
      }
    }

    m_InputDirection = Vector2.zero;
  }

  private float SlopeMultiplier()
  {
    var angle = Vector3.Angle(m_GroundContactNormal, -m_Gravity.direction);

    return m_SlopeCurveModifier.Evaluate(angle);
  }

  private void StickToGroundHelper()
  {
    RaycastHit hitInfo;
    if (Physics.SphereCast(transform.position, m_CapsuleCollider.radius * (1.0f - s_ShellOffset), m_Gravity.direction, out hitInfo, ((m_CapsuleCollider.height / 2f) - m_CapsuleCollider.radius) + s_StickToGroundHelperDistance, ~0, QueryTriggerInteraction.Ignore)) {
      if (Mathf.Abs(Vector3.Angle(hitInfo.normal, Vector3.up)) < 85f) {
        m_Rigidbody.velocity = Vector3.ProjectOnPlane(m_Rigidbody.velocity, hitInfo.normal);
      }
    }
  }

  private void GroundCheck()
  {
    m_PreviouslyGrounded = grounded;
    RaycastHit hitInfo;
    if (Physics.SphereCast(transform.position, m_CapsuleCollider.radius * (1.0f - s_ShellOffset), m_Gravity.direction, out hitInfo, ((m_CapsuleCollider.height / 2.0f) - m_CapsuleCollider.radius) + s_GroundCheckDistance, ~0, QueryTriggerInteraction.Ignore)) {
      grounded = true;
      m_GroundContactNormal = hitInfo.normal;
    } else {
      grounded = false;
      m_GroundContactNormal = Vector3.up;
    }

    if (!m_PreviouslyGrounded && grounded && m_Jumping) {
      m_Jumping = false;
    }
  }

  private void UpdateDesiredTargetSpeed()
  {
    if (m_InputDirection == Vector2.zero) {
      return;
    }

    if (m_InputDirection.x > 0.0f || m_InputDirection.x < 0.0f) {
      // strafe
      m_CurrentTargetSpeed = m_Speed * 7.0f;
    }

    if (m_InputDirection.y < 0.0f) {
      // backwards
      m_CurrentTargetSpeed = m_Speed * 5.0f;
    }

    if (m_InputDirection.y > 0.0f) {
      // forwards
      m_CurrentTargetSpeed = m_Speed * 8.0f;
    }
  }
}