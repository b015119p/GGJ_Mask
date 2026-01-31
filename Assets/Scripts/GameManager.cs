using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject m_PlayerPrefab;
    private GameObject m_slimbo;
    [SerializeField] private GameObject m_CheckpointParent;
    [SerializeField] private GameObject m_End;
    [SerializeField] private CinemachineCamera m_Camera;
    public Transform m_groundCheckPos;
    [SerializeField] Vector2 m_SpawnPosition;

    private int m_playerState = 0;
    private bool m_slimboCanDoubleJump = false;


    private void Start()
    {
        m_SpawnPosition = m_groundCheckPos.position;


        for (int i = 0; i < m_CheckpointParent.transform.childCount; i++)
        {
            m_CheckpointParent.transform.GetChild(i).GetComponent<CheckPointBounds>().Checkpoint.AddListener(onCheckpoint);
        }

        m_End.GetComponent<WinBounds>().Win.AddListener(OnWin);

        SpawnPlayer();
    }


    private void OnWin(WinBounds Win)
    {
        Debug.Log("WIN???");
        SpawnPlayer();
    }

    private void onCheckpoint(CheckPointBounds cuh)
    {
        m_SpawnPosition = new Vector2(cuh.transform.position.x, cuh.transform.position.y);

        m_playerState = m_slimbo.GetComponent<PlayerBehaviour>().getState();
        m_slimboCanDoubleJump = m_slimbo.GetComponent<PlayerBehaviour>().m_canDJ;

    }

    public void SpawnPlayer()
    {
        if (m_slimbo != null)
        {
            m_slimbo.GetComponent<PlayerBehaviour>().DisableMv(m_playerState);
            Destroy(m_slimbo);
        }

        m_slimbo = Instantiate(m_PlayerPrefab, new Vector3(m_SpawnPosition.x, m_SpawnPosition.y, 0f), Quaternion.identity);

        m_slimbo.GetComponent<PlayerBehaviour>().m_canDJ = m_slimboCanDoubleJump;
        m_slimbo.GetComponent<PlayerBehaviour>().ChangeMType(m_playerState);

        m_Camera.GetComponent<CinemachineCamera>().LookAt = m_slimbo.transform;
        m_Camera.GetComponent<CinemachineCamera>().Follow = m_slimbo.transform;
    }
}
