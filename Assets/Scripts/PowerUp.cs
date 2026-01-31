using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PowerUp : MonoBehaviour
{
    enum MovementState
    {
        Basic,
        Slam,
        Dash
    }
    [SerializeField] private MovementState m_State = MovementState.Dash;

    private GameObject m_Player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            m_Player = collision.gameObject;
            m_Player.GetComponent<PlayerBehaviour>().ChangeMType((int)m_State);
            Destroy(gameObject);
        }
    }
}
