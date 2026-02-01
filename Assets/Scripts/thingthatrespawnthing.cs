using UnityEngine;

public class thingthatrespawnthing : MonoBehaviour
{
    [SerializeField] private GameObject m_Prefab;
    private GameObject m_child;


    public void spawnThings()
    {
        if (m_child != null)
        {
            Destroy(m_child);
        }
        m_child = Instantiate(m_Prefab, transform.position, Quaternion.identity);
    }
}
