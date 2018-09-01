using UnityEngine;
using UnityEngine.SceneManagement;

public class Persistent : MonoBehaviour
{
    private GameObject m_PauseMenu;
    
    private static Persistent m_Instance;

    private void Awake()
    {
        if (m_Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        m_Instance = this;
        DontDestroyOnLoad(gameObject);

        m_PauseMenu = transform.Find("PauseMenu").gameObject;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            TogglePause();
    }

    private void TogglePause()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
            return;

        bool pause = Time.timeScale != 0;

        m_PauseMenu.SetActive(pause);
        Time.timeScale = pause ? 0 : 1;
    }

    public void LoadMainMenu()
    {
        TogglePause();
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}