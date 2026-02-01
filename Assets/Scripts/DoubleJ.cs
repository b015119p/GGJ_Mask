using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class DoubleJ : MonoBehaviour
{

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
            m_Player.GetComponent<PlayerBehaviour>().m_canDJ = true;
            m_Player.GetComponent<PlayerBehaviour>().m_djReady = true;
            Destroy(gameObject);
        }
    }
}
