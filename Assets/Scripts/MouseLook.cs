using UnityEngine;

public class MouseLook : MonoBehaviour
{
  [SerializeField] private float m_RotationSpeed;
  [SerializeField] private float m_DampingTime;
  private Vector3 m_TargetAngles;
  private Vector3 m_FollowAngles;
  private Vector3 m_FollowVelocity;
  private Quaternion m_OriginalRotation;
  private CursorLockMode m_LockMode;

  private void Start()
  {
    m_OriginalRotation = transform.localRotation;
  }

  private void Update()
  {
    transform.localRotation = m_OriginalRotation;

    var inputH = Input.GetAxis("Mouse X");
    var inputV = Input.GetAxis("Mouse Y");

    if (m_TargetAngles.y > 180.0f) {
      m_TargetAngles.y -= 360.0f;
      m_FollowAngles.y -= 360.0f;
    }

    if (m_TargetAngles.x > 180.0f) {
      m_TargetAngles.x -= 360.0f;
      m_FollowAngles.x -= 360.0f;
    }

    if (m_TargetAngles.y < -180.0f) {
      m_TargetAngles.y += 360.0f;
      m_FollowAngles.y += 360.0f;
    }

    if (m_TargetAngles.x < -180.0f) {
      m_TargetAngles.x += 360.0f;
      m_FollowAngles.x += 360.0f;
    }

    m_TargetAngles.y += inputH * m_RotationSpeed;
    m_TargetAngles.x += inputV * m_RotationSpeed;
    m_TargetAngles.x = Mathf.Clamp(m_TargetAngles.x, -90.0f, 90.0f);
    m_FollowAngles = Vector3.SmoothDamp(m_FollowAngles, m_TargetAngles, ref m_FollowVelocity, m_DampingTime);

    transform.localRotation = m_OriginalRotation * Quaternion.Euler(-m_FollowAngles.x, m_FollowAngles.y, 0);

#if UNITY_EDITOR
    if (Input.GetKeyDown(KeyCode.Escape)) {
      m_LockMode = m_LockMode == CursorLockMode.None ? CursorLockMode.Locked : CursorLockMode.None;
    }

    Cursor.lockState = m_LockMode;
    Cursor.visible = CursorLockMode.Locked != m_LockMode;
#endif
  }
}