using UnityEngine;

[RequireComponent(typeof (RigidbodyCharacterController))]
public class PlayerInput : MonoBehaviour
{
  private RigidbodyCharacterController m_GravityCharacterController;
  [SerializeField] private LaserGun m_LaserGun;
  private Vector2 m_InputDirection;
  [SerializeField] private Transform m_View;

  private void Start()
  {
    m_GravityCharacterController = GetComponent<RigidbodyCharacterController>();
  }

  private void Update()
  {
    if (Input.GetButtonDown("Jump")) {
      m_GravityCharacterController.Jump();
    }

#if !UNITY_EDITOR
     if (Input.GetKeyDown(KeyCode.Escape)) {
      GetComponent<Player>().Die();
      return;
    }
#endif

    if (Input.GetButton("Fire")) {
      m_LaserGun.Fire();
    } else {
      m_LaserGun.EndFire();
    }
  }

  private void FixedUpdate()
  {
    m_InputDirection.x = Input.GetAxis("Horizontal");
    m_InputDirection.y = Input.GetAxis("Vertical");

    m_GravityCharacterController.Move(m_InputDirection);
  }
}