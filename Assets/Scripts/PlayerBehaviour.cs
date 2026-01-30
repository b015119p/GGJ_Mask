using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    //Player's rigidbody component
    private Rigidbody2D m_rigidbody;

    //Direction we want the player to move in. Should only be on the x axis
    private Vector2 m_playerDirection;
    //Speed of the player. Kinda simple
    [SerializeField] private float m_playerSpeed = 200f;

    private bool m_flipFlop = true;

    public LayerMask wallLayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_playerDirection = Vector2.right;
    }

    // Update is called once per fixed update, think the default is 40
    void FixedUpdate()
    {
        m_rigidbody.linearVelocity = m_playerDirection * (m_playerSpeed * Time.fixedDeltaTime);

        if (Physics2D.Raycast(transform.position, m_playerDirection, 1f, wallLayer))
        {
            if (m_flipFlop)
            {
                m_playerDirection = Vector2.left;
            }
            else
            {
                m_playerDirection = Vector2.right;
            }
            m_flipFlop = !m_flipFlop;
        }
    }
}
