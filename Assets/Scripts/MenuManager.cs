using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    //This is the panel which should be active at first
    [SerializeField] private GameObject m_activePanel;
    [SerializeField] private GameObject m_pausePanel;
    [SerializeField] private TextMeshProUGUI m_scrollingText;
    [SerializeField] private bool m_pausable;
    private bool m_moveText;
    InputSystem_Actions m_GameControls;
    [SerializeField] private GameObject m_deathButton;
    [SerializeField] private GameObject m_Panel;

    private AudioSource m_AudioSource;

    private void Awake()
    {
        m_GameControls = new InputSystem_Actions();
        m_GameControls.UI.Enable();
        m_moveText = false;
        m_AudioSource = GetComponent<AudioSource>();

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
            FreezeTime(true);
            ChangePausability(false);
        }
    }

    //Freezes time bro :finnadie:
    public void FreezeTime(bool m_freeze)
    {
        if (m_freeze)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    //Toggles whether the player can pause or not
    public void ChangePausability(bool m_pause)
    {
        if (m_pause)
        {
            m_pausable = true;
        }
        else
        {
            m_pausable = false;
        }
    }

    public void TextScroll(string m_text)
    {
        m_scrollingText.text = m_text;
        m_moveText = true;
        m_scrollingText.transform.localPosition = new Vector3(1560, 152, 0);
    }

    private void Update()
    {
        if (m_moveText)
        {
            m_scrollingText.transform.localPosition -= new Vector3(200 * Time.deltaTime, 0, 0);
            if (m_scrollingText.transform.localPosition[0] <= -1550)
            {
                m_moveText = false;
                m_scrollingText.transform.localPosition = new Vector3(1560, 152, 0);
            }
        }
    }

    public void ShowDeathButton()
    {
        m_deathButton.SetActive(true);
        m_AudioSource.Play();
    }

    public void deathButtonPressed()
    {
        m_deathButton.SetActive(false);
        fadelol();
        m_AudioSource.Stop();
    }

    public void fadelol()
    {
        m_Panel.GetComponent<Animator>().SetTrigger("Fade");
    }
}
