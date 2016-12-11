using UnityEngine;

public class CannonBall : MonoBehaviour
{
  [SerializeField] private CannonBallExplosion m_ExplosionPrefab;
  [SerializeField] private Light m_Light;
  private float m_Timer;

  private void OnEnable()
  {
    m_Timer = 2.0f;
    GetComponent<Rigidbody>().AddForce(transform.forward * 1.3f, ForceMode.Impulse);
  }

  private void OnCollisionEnter(Collision collision)
  {
    if (collision.collider.CompareTag("Player")) {
      Explode();
    }
  }

  private void Update()
  {
    m_Timer -= Time.deltaTime;
    if (m_Timer <= 0.0f) {
      Explode();
    } else {
      m_Light.intensity = Mathf.Pow(2.0f - m_Timer, 2.0f);
    }
  }

  private void Explode()
  {
    var colliders = Physics.OverlapSphere(transform.position, 5.0f, 1 << LayerMask.NameToLayer("Player"));

    for (var i = 0; i < colliders.Length; i++) {
      var target = colliders[i].GetComponent<ITarget>();
      target.OnShot((1.0f - (transform.position - colliders[i].transform.position).sqrMagnitude / 25.0f) * 5.0f);
    }

    Instantiate(m_ExplosionPrefab, transform.position, Quaternion.identity);
    this.Recycle();
  }
}