using UnityEngine;
using UnityEngine.Events;

public class Player : Singleton<Player>, ITarget
{
  [SerializeField] private Camera m_Camera;
  [SerializeField] private UnityEvent m_OnHealthChange;
  private float m_RegenerationTimer;
  public static float health { get; private set; }
  public static float maxHealth { get; private set; }
  public Rigidbody rigidbody { get; private set; }
  public Gravity gravity { get; private set; }

  public void OnShot(Weapon weapon)
  {
    health -= Random.Range(1.0f, 2.0f);
    m_RegenerationTimer = 5.0f;

    if (health <= 0.0f) {
      Die();
    } else if (m_OnHealthChange != null) {
      m_OnHealthChange.Invoke();
    }
  }

  private void Update()
  {
    if (m_RegenerationTimer <= 0.0f) {
      if (health < maxHealth) {
        health = Mathf.Clamp(health + Time.deltaTime * 0.5f, 0.0f, maxHealth);
        if (m_OnHealthChange != null) {
          m_OnHealthChange.Invoke();
        }
      }
    } else {
      m_RegenerationTimer -= Time.deltaTime;
    }
  }

  public void Die()
  {
    m_Camera.transform.SetParent(null);
    Destroy(gameObject);
    GameController.EndGame();

    Cursor.lockState = CursorLockMode.None;
    Cursor.visible = true;
  }

  public void Start()
  {
    rigidbody = GetComponent<Rigidbody>();
    gravity = GetComponent<Gravity>();
    maxHealth = 20.0f;
    health = maxHealth;
  }

  public static void PickupHealthPack()
  {
    health = Mathf.Clamp(health + 7.0f, 0.0f, maxHealth);
    if (instance.m_OnHealthChange != null) {
      instance.m_OnHealthChange.Invoke();
    }
  }
}