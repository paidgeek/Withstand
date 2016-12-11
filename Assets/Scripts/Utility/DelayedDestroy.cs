using UnityEngine;

public class DelayedDestroy : MonoBehaviour
{
  [SerializeField] private float m_Delay;

  private void Start()
  {
    Destroy(gameObject, m_Delay);
  }
}