using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;

public class MovingObstacle : MonoBehaviour
{
    [SerializeField] private Transform[] m_waypoints;
    [SerializeField] private int m_index = 0;
    [SerializeField] private float m_speed = 5;

    [SerializeField] private Transform m_target;
    private Rigidbody2D m_rigidbody;
    private SpriteRenderer m_spriteRenderer;
    private bool m_flipFlop = false;

    private void Awake()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_target = m_waypoints[m_index];
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, m_target.position, m_speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Waypoint"))
        {
            //Debug.Log("Whomp");
            updateTarget();
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerBehaviour>().KILLPLAYER();
        }
    }

    void updateTarget()
    {
        m_index++;
        m_index %= m_waypoints.Length;
        m_target = m_waypoints[m_index];

        if (m_flipFlop)
        {
            m_spriteRenderer.flipX = true;
        }
        else
        {
            m_spriteRenderer.flipX = false;
        }
        m_flipFlop = !m_flipFlop;
    }
}
