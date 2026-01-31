using UnityEngine;
using UnityEngine.Events;

public class WinBounds : MonoBehaviour
{
    public UnityEvent<WinBounds> Win;
    private BoxCollider2D m_Collider;

    void Awake()
    {
        m_Collider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Win!");
            Win?.Invoke(this);
        }
    }
}
