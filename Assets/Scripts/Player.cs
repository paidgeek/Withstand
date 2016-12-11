using UnityEngine;
using UnityEngine.Events;

public class Player : Singleton<Player>, ITarget
{
  [SerializeField] private Camera m_Camera;
  [SerializeField] private UnityEvent m_OnHealthChange;
  [SerializeField] private UnityEvent m_OnShieldEnable;
  [SerializeField] private Shake m_Shake;
  [SerializeField] private AudioSource m_HurtAudioSource;
  [SerializeField] private AudioSource m_DeathAudioSource;
  [SerializeField] private AudioSource m_PowerupAudioSource;
  [SerializeField] private AudioSource m_PixelPickupAudioSource;
  private float m_RegenerationTimer;
  private float m_ShieldTimer;
  public static float health { get; private set; }
  public static float maxHealth { get; private set; }
  public Rigidbody rigidbody { get; private set; }
  public Gravity gravity { get; private set; }

  public void OnShot(float damage)
  {
    if (m_ShieldTimer > 0.0f) {
      return;
    }

    health -= damage;
    m_RegenerationTimer = 5.0f;

    if (health <= 0.0f) {
      Die();
      m_DeathAudioSource.Play();
    } else if (m_OnHealthChange != null) {
      m_OnHealthChange.Invoke();
      m_HurtAudioSource.Play();
    }

    if (m_Shake) {
      m_Shake.Push(damage * 0.3f);
    }
  }

  private void Update()
  {
    if (m_ShieldTimer >= 0.0f) {
      m_ShieldTimer -= Time.deltaTime;
    }

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

    var cameras = GetComponentsInChildren<Camera>();
    cameras[0].fieldOfView = PlayerPrefs.GetInt("Fov", 70);
    cameras[1].fieldOfView = PlayerPrefs.GetInt("Fov", 70);
  }

  public static void PickupHealthPack()
  {
    health = Mathf.Clamp(health + 7.0f, 0.0f, maxHealth);
    if (instance.m_OnHealthChange != null) {
      instance.m_OnHealthChange.Invoke();
    }

    instance.m_PowerupAudioSource.Play();
  }

  public static void PickupPixel()
  {
    GameController.EarnPixel();
    instance.m_PixelPickupAudioSource.Play();
  }

  public static void PickupShield()
  {
    instance.m_ShieldTimer = 10.0f;
    instance.m_OnShieldEnable.Invoke();

    instance.m_PowerupAudioSource.Play();
  }
}