using UnityEngine;

public class MouseLook : MonoBehaviour
{
  [SerializeField] private float m_RotationSpeed;
  private Quaternion m_OriginalRotation;
  private CursorLockMode m_LockMode;
  private Vector3 m_TargetAngles;

  private void Start()
  {
    m_OriginalRotation = transform.localRotation;
    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;

    m_RotationSpeed *= PlayerPrefs.GetFloat("MouseSensitivity", 1.0f);
  }

  private void Update()
  {
    transform.localRotation = m_OriginalRotation;

    var inputH = Input.GetAxis("Mouse X");
    var inputV = Input.GetAxis("Mouse Y");

    if (m_TargetAngles.y > 180.0f) {
      m_TargetAngles.y -= 360.0f;
    }

    if (m_TargetAngles.x > 180.0f) {
      m_TargetAngles.x -= 360.0f;
    }

    if (m_TargetAngles.y < -180.0f) {
      m_TargetAngles.y += 360.0f;
    }

    if (m_TargetAngles.x < -180.0f) {
      m_TargetAngles.x += 360.0f;
    }

    m_TargetAngles.y += inputH * m_RotationSpeed;
    m_TargetAngles.x += inputV * m_RotationSpeed;
    m_TargetAngles.x = Mathf.Clamp(m_TargetAngles.x, -90.0f, 90.0f);

    transform.localRotation = m_OriginalRotation * Quaternion.Euler(-m_TargetAngles.x, m_TargetAngles.y, 0);

#if UNITY_EDITOR
    if (Input.GetKeyDown(KeyCode.Escape)) {
      Cursor.lockState = CursorLockMode.None;
      Cursor.visible = true;
    }
#endif
  }
}