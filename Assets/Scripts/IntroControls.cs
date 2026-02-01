using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroControls : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_currentDialogue;
    [SerializeField] private Image m_currentImage;
    [SerializeField] private string[] m_dialogue;
    [SerializeField] private Sprite[] m_image;
    private int m_counter = 0;

    //Goes to the next image/dialogue
    public void Next()
    {
        m_currentDialogue.text = m_dialogue[m_counter];
        m_currentImage.sprite = m_image[m_counter]; 
        if (m_counter == 5)
        {
            SceneManager.LoadScene("MainMenu");
        }
        m_counter++;
    }
}
