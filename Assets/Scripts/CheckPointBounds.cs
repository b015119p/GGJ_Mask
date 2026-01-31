using UnityEngine;
using UnityEngine.Events;

public class CheckPointBounds : MonoBehaviour
{
    public UnityEvent<CheckPointBounds> Checkpoint;
    private BoxCollider2D m_Collider;
    private bool m_activated = false;

    void Awake()
    {
        m_Collider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !m_activated)
        {
            m_activated = true;
            Debug.Log("Checkpoint!");
            Checkpoint?.Invoke(this);
        }
    }
}
