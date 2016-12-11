using UnityEngine;

public class Rotate : MonoBehaviour
{
  [SerializeField] private Vector3 m_Axis;

  private void Update()
  {
    transform.Rotate(m_Axis * Time.deltaTime, Space.Self);
  }
}