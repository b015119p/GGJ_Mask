using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    //This is the panel which should be active at first
    [SerializeField] private GameObject m_activePanel;
    [SerializeField] private GameObject m_pausePanel;
    [SerializeField] private GameObject m_settingsPanel;
    [SerializeField] private bool m_pausable;
    InputSystem_Actions m_GameControls;

    private void Awake()
    {
        m_GameControls = new InputSystem_Actions();
        m_GameControls.UI.Enable();
    }

    private void OnEnable()
    {
        m_GameControls.UI.Cancel.performed += PauseGame;
    }

    //Changes the scene
    public void ChangeScene(string m_scene)
    {
        SceneManager.LoadScene(m_scene);
        Time.timeScale = 1f;
    }

    //Deactivates the panel which is currently active, activates another panel which is given as a parameter, then makes that panel the active panel
    public void ChangePanel(GameObject m_newPanel)
    {
        m_activePanel.SetActive(false);
        m_newPanel.SetActive(true);
        m_activePanel = m_newPanel;
        if (m_activePanel == m_pausePanel || m_activePanel == m_settingsPanel)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    //Quits the game
    public void QuitGame()
    {
        Application.Quit();
    }

    //Pauses the game
    private void PauseGame(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && m_pausable)
        {
            ChangePanel(m_pausePanel);
        }
    }
}
