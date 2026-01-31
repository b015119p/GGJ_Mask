using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    enum MovementState
    {
        Basic,
        Slam,
        Dash
    }

    //Input system itself
    InputSystem_Actions m_GameControls;

    //Player's rigidbody component
    private Rigidbody2D m_rigidbody;
    //Player's Circlebody component
    private CircleCollider2D m_collider;
    //Layer used for detecting collision
    public LayerMask wallLayer;

    [Header("Movement parameters")]
    [SerializeField] private MovementState m_currentState = MovementState.Basic;
    //Direction we want the player to move in. This variable should only be on the x axis
    private Vector2 m_playerDirection;
    //Speed of the player. Kinda simple
    [SerializeField] private float m_playerSpeed = 200f;
    //Scale of the thing mario does. yahoo!
    [SerializeField] private float m_jumpPower = 5f;
    //Used to handle which direction the player is moving in. Flip (true) = right, Flop (false) = left.
    private bool m_flipFlop = true;
    //Used to handle if the player can double jump
    public bool m_canDJ = false;
    //Effectively a state to handle the cooldown for the DJ
    private bool m_djReady = false;
    //Used to handle if the player can dash.
    private bool m_dashReady = true;
    //Used to handle if the player is actively dashing
    private bool m_Dashing = false;


    [Header("Ground check")]
    //Point to check for the ground.
    public Transform m_groundCheckPos;
    //size of the bounds for checking the ground
    public Vector2 m_groundCheckSize = new Vector2(0.5f, 0.05f);


    //[Header("Debug stuff")]


    #region OnEnable/Disable

    public void ChangeMType(int newState)
    {
        DisableMType(m_currentState);

        EnableMType((MovementState)newState);

        m_currentState = (MovementState)newState;
    }

    private void EnableMType(MovementState State)
    {
        switch (State)
        {
            case MovementState.Basic:
                Debug.Log("Basic Movement");
                m_GameControls.Basic.Enable();
                break;
            case MovementState.Slam:
                Debug.Log("Slam Movement");
                m_GameControls.Slam.Enable();
                break;
            case MovementState.Dash:
                Debug.Log("Dash Movement");
                m_GameControls.Dash.Enable();
                break;
        }
    }

    private void DisableMType(MovementState State)
    {
        switch (State)
        {
            case MovementState.Basic:
                m_GameControls.Basic.Disable();
                break;
            case MovementState.Slam:
                m_GameControls.Slam.Disable();
                break;
            case MovementState.Dash:
                m_GameControls.Dash.Disable();
                break;
        }
    }

    private void OnEnable()
    {
        m_GameControls.Basic.Jump.performed += HandleJump;
        m_GameControls.Basic.Jump.canceled += HandleJump;
        m_GameControls.Basic.Switch.canceled += HandleSwitch;

        m_GameControls.Slam.Jump.performed += HandleJump;
        m_GameControls.Slam.Slam.performed += HandleSlam;
        m_GameControls.Slam.Switch.canceled += HandleSwitch;

        m_GameControls.Dash.Jump.performed += HandleJump;
        m_GameControls.Dash.Jump.canceled += HandleJump;
        m_GameControls.Dash.Dash.performed += HandleDash;
        m_GameControls.Dash.Switch.canceled += HandleSwitch;
    }

    private void OnDisable()
    {
        m_GameControls.Basic.Jump.performed -= HandleJump;
        m_GameControls.Basic.Jump.canceled -= HandleJump;
        m_GameControls.Basic.Switch.canceled -= HandleSwitch;

        m_GameControls.Slam.Jump.performed -= HandleJump;
        m_GameControls.Slam.Slam.performed-= HandleSlam;
        m_GameControls.Slam.Switch.canceled -= HandleSwitch;

        m_GameControls.Dash.Jump.performed -= HandleJump;
        m_GameControls.Dash.Jump.canceled -= HandleJump;
        m_GameControls.Dash.Dash.performed -= HandleDash;
        m_GameControls.Dash.Switch.canceled -= HandleSwitch;
    }

    #endregion

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_collider = GetComponent<CircleCollider2D>();

        m_GameControls = new InputSystem_Actions();

        EnableMType(m_currentState);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_playerDirection = Vector2.right;
    }

    // Update is called once per fixed update, think the default is 40
    void FixedUpdate()
    {
        if (m_Dashing)
        {
            return;
        }

        m_rigidbody.linearVelocity = new Vector2(m_playerDirection.x * m_playerSpeed, m_rigidbody.linearVelocity.y);

        //Behaviour to change direction, effectively activates when the player hits a wall.
        if (Physics2D.Raycast(transform.position, m_playerDirection, m_collider.bounds.extents.x + 0.1f, wallLayer))
        {
            ChangeDirection();
        }

        //Reset values if grounded
        if (isGrounded())
        {
            m_dashReady = true;
            if (m_canDJ)
            {
                m_djReady = true;
            }
        }
    }

    private void ChangeDirection()
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

    private bool isGrounded()
    {
        if (Physics2D.OverlapBox(m_groundCheckPos.position, m_groundCheckSize, 0, wallLayer))
        {
            return true;
        }
        return false;
    }

    private void HandleJump(InputAction.CallbackContext ctx)
    {
        if(isGrounded())
        {
            if (ctx.performed)
            {
                m_rigidbody.linearVelocity = new Vector2(m_rigidbody.linearVelocity.x, m_jumpPower);
            }

        }
        else if (m_djReady)
        {
            if (ctx.performed)
            {
                m_djReady = false;
                m_rigidbody.linearVelocity = new Vector2(m_rigidbody.linearVelocity.x, m_jumpPower);
            }
        }

        if (ctx.canceled)
        {
            m_rigidbody.linearVelocity = new Vector2(m_rigidbody.linearVelocity.x, m_rigidbody.linearVelocity.y * 0.5f);
        }
    }

    private void HandleSlam(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            m_rigidbody.linearVelocity += new Vector2(0, -(m_jumpPower * 2));
        }
    }

    private void HandleDash(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            Debug.Log("Dash event");
            if (m_dashReady)
            {
                Debug.Log("Perform Dash");
                StartCoroutine(Dash());
            }
        }

    }

    private IEnumerator Dash()
    {
        m_Dashing = true;
        m_dashReady = false;
        float ogGravity = m_rigidbody.gravityScale;
        m_rigidbody.gravityScale = 0f;
        m_rigidbody.linearVelocity = new Vector2(m_playerDirection.x * 20f, 0f);
        yield return new WaitForSeconds(0.2f);
        m_rigidbody.gravityScale = ogGravity;
        m_Dashing = false;
    }

    private void HandleSwitch(InputAction.CallbackContext ctx)
    {
        if (isGrounded())
        {
            ChangeDirection();
        }
    }

    //Allows visualising the ground detection hitbox in editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(m_groundCheckPos.position, m_groundCheckSize);
    }
}