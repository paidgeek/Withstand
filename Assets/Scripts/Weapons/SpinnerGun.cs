using UnityEngine;

public class SpinnerGun : MonoBehaviour
{
  [SerializeField] private LineRenderer[] m_Holes;
  [SerializeField] private Transform[] m_Impacts;
  [SerializeField] private LayerMask m_HitLayer;

  private void Update()
  {
    RaycastHit hit;

    for (int i = 0; i < 4; i++) {
      var lr = m_Holes[i];
      var t = m_Holes[i].transform;

      if (Physics.Raycast(t.position, t.forward, out hit, 100.0f, m_HitLayer)) {
        lr.SetPosition(0, t.position);
        lr.SetPosition(1, hit.point);

        m_Impacts[i].position = hit.point + hit.normal * 0.1f;

        var target = hit.collider.GetComponent<ITarget>();
        if (target != null) {
          target.OnShot(20.0f * Time.deltaTime);
        }
      }
    }
  }
}