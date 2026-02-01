using UnityEngine;

public class behaviourthatkillsyou : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject m_Player = collision.gameObject;
            m_Player.GetComponent<PlayerBehaviour>().KILLPLAYER();
        }
    }
}
