using UnityEngine;

public class MovingThing : MonoBehaviour
{
[SerializeField] private Transform[] m_waypoints;
[SerializeField] private int m_index = 0;
[SerializeField] private float m_speed = 5;

[SerializeField] private Transform m_targetA;
    [SerializeField] private Transform m_targetB;
    [SerializeField] private Transform m_target;

    private bool ough = false;

    private Rigidbody2D m_rigidbody;

private void Awake()
{
    m_rigidbody = GetComponent<Rigidbody2D>();
}

// Start is called once before the first execution of Update after the MonoBehaviour is created
void Start()
{
    m_targetA = m_waypoints[0];
        m_targetB = m_waypoints[1];
        updateTarget();
}

// Update is called once per frame
void Update()
{
    transform.position = Vector2.MoveTowards(transform.position, m_target.position, m_speed * Time.deltaTime);
        if (transform.position == m_target.position)
        {
            updateTarget();
        }
}

private void OnTriggerEnter2D(Collider2D collision)
{
    if (collision.gameObject.CompareTag("Waypoint"))
    {
        Debug.Log("Whomp");
        updateTarget();
    }
}

void updateTarget()
{
        if (ough)
        {
            m_target = m_targetB.transform;
        }
        else
        {
            m_target = m_targetA.transform;

        }
        ough = !ough;
    }


}
