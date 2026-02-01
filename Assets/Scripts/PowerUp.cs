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
    private AudioSource m_AudioSource;


    private void Awake()
    {
        m_AudioSource = GetComponent<AudioSource>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            m_Player = collision.gameObject;
            m_Player.GetComponent<PlayerBehaviour>().PlayTheSoundFromTheGame();
            m_Player.GetComponent<PlayerBehaviour>().ChangeMType((int)m_State);
            Destroy(gameObject);
        }
    }
}
