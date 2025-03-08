using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Home()
    {
        SceneManager.LoadScene("Main-Menu");
    }

    public void Save()
    {
        PlayerController.Instance.SaveGame();
    }

    public void Mute()
    {
        AudioListener.volume = (AudioListener.volume == 0) ? 1 : 0;
        Debug.Log("Mute: " + (AudioListener.volume == 0 ? "ON" : "OFF"));
    }

    public void Shop()
    {
        // TODO: Implement shop
    }

}
