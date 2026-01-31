using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject m_Player;
    [SerializeField] private GameObject m_CheckpointParent;
    [SerializeField] private GameObject m_End;

    private void Start()
    {
        //for (int i = 0; i < m_CheckpointParent.transform.childCount; i++)
        //{
        //    m_CheckpointParent.transform.GetChild(i).GetComponent<CheckPointBounds>().Checkpoint.AddListener(OnTargetDestroyed);
        //}

        m_End.GetComponent<WinBounds>().Win.AddListener(OnWin);

        // m_Player.GetComponent<PlayerBehaviour>
    }


    private void OnWin(WinBounds Win)
    {
        Debug.Log("WIN???");
    }
}
