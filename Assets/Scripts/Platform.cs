using System.Collections;
using UnityEngine;

public class Platform : MonoBehaviour
{
  private Vector3 m_StartPosition;
  private float m_Factor;
  private int m_State;

  private void Start()
  {
    m_StartPosition = transform.position;
    StartCoroutine(Lift());
  }

  private void FixedUpdate()
  {
    switch (m_State) {
      case 0:
        break;
      case 1:
        m_Factor = Mathf.Clamp01(m_Factor + 5.0f * Time.fixedDeltaTime);
        SetPosition(m_Factor);

        if (m_Factor >= 1.0f) {
          Push();
          m_State = 2;
        }
        break;
      case 2:
        m_Factor = Mathf.Clamp01(m_Factor - 5.0f * Time.fixedDeltaTime);
        SetPosition(m_Factor);

        if (m_Factor <= 0.0f) {
          m_State = 0;
          StartCoroutine(Lift());
        }
        break;
    }
  }

  private void Push()
  {
    var hits = Physics.BoxCastAll(transform.position, new Vector3(4.0f, 4.0f, 4.0f), transform.up, Quaternion.identity, 1.0f);
    for (var i = 0; i < hits.Length; i++) {
      var col = hits[i].collider;
      var rb = col.GetComponent<Rigidbody>();

      if (rb) {
        rb.AddForce(transform.up * 800.0f, ForceMode.Acceleration);
        /*
        var rgc = col.GetComponent<RigidbodyCharacterController>();
        if (rgc) {
          rb.drag = 0.0f;
        }
        */
      }
    }
  }

  private IEnumerator Lift()
  {
    yield return new WaitForSeconds(Random.Range(5, 20));

    m_State = 1;
  }

  private void SetPosition(float f)
  {
    transform.position = Vector3.Lerp(m_StartPosition, m_StartPosition + transform.up * 4.0f, f);
  }
}