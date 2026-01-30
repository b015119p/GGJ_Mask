using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject m_activePanel;

    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void ChangePanel(GameObject m_newPanel)
    {
        m_activePanel.SetActive(false);
        m_newPanel.SetActive(true);
        m_activePanel = m_newPanel;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
