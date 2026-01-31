using Unity.VisualScripting;
using UnityEngine;

public class LerpThing : MonoBehaviour
{
    [SerializeField] private GameObject m_Player;


    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector2.Lerp(transform.position, m_Player.transform.position, Time.deltaTime * 1.3f);
    }
}
