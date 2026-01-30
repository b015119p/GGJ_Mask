using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    //Input system itself
    InputSystem_Actions m_GameControls;
    //The inputs that we need to retrieve from the input system.
    //private InputAction m_moveAction;

    //Player's rigidbody component
    private Rigidbody2D m_rigidbody;

    private CircleCollider2D m_collider;

    public LayerMask wallLayer;

    [Header("Movement parameters")]
    //Direction we want the player to move in. Should only be on the x axis
    private Vector2 m_playerDirection;
    //Speed of the player. Kinda simple
    [SerializeField] private float m_playerSpeed = 200f;
    //Scale of the thing mario does. yahoo!
    [SerializeField] private float m_jumpPower = 5f;

    [Header("Ground check")]
    public Transform m_groundCheckPos;
    public Vector2 m_groundCheckSize = new Vector2(0.5f, 0.05f);

    private bool m_flipFlop = true;


    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_collider = GetComponent<CircleCollider2D>();

        m_GameControls = new InputSystem_Actions();
        m_GameControls.Player.Enable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_playerDirection = Vector2.right;
    }

    private void OnEnable()
    {
        m_GameControls.Player.Jump.performed += HandleJump;
        m_GameControls.Player.Jump.canceled += HandleJump;

    }

    // Update is called once per fixed update, think the default is 40
    void FixedUpdate()
    {
        //m_rigidbody.linearVelocity = m_playerDirection * (m_playerSpeed * Time.fixedDeltaTime);

        m_rigidbody.linearVelocity = new Vector2(m_playerDirection.x * m_playerSpeed, m_rigidbody.linearVelocity.y);

        if (Physics2D.Raycast(transform.position, m_playerDirection, m_collider.bounds.extents.x + 0.1f, wallLayer))
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


    private void HandleSlamJump(InputAction.CallbackContext ctx)
    {
        if (isGrounded())
        {
            if (ctx.performed)
            {
                m_rigidbody.linearVelocity = new Vector2(m_rigidbody.linearVelocity.x, m_jumpPower);
                //m_rigidbody.linearVelocity += new Vector2(0, m_jumpPower);
            }

        }
        else if (ctx.performed)
        {
            m_rigidbody.linearVelocity += new Vector2(0, -(m_jumpPower * 2));
        }
    }

    private void HandleJump(InputAction.CallbackContext ctx)
    {
        if(isGrounded())
        {
            if (ctx.performed)
            {
                m_rigidbody.linearVelocity = new Vector2(m_rigidbody.linearVelocity.x, m_jumpPower);
                //m_rigidbody.linearVelocity += new Vector2(0, m_jumpPower);
            }

        }
        if (ctx.canceled)
        {
            m_rigidbody.linearVelocity = new Vector2(m_rigidbody.linearVelocity.x, m_rigidbody.linearVelocity.y * 0.5f);
        }
    }

    private bool isGrounded()
    {
        if (Physics2D.OverlapBox(m_groundCheckPos.position, m_groundCheckSize, 0, wallLayer))
        {
            return true;
        }
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(m_groundCheckPos.position, m_groundCheckSize);
    }
}