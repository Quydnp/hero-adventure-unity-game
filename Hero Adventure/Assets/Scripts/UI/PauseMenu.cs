using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject weapon;
    public static bool isPaused = false;
    public void Pause()
    {
        pauseMenu.SetActive(true);
        isPaused = true;
        Time.timeScale = 0f;

        weapon.GetComponent<MouseFollow>().enabled = false;

        PlayerController.Instance.UnableControls();
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;

        weapon.GetComponent<MouseFollow>().enabled = true;

        PlayerController.Instance.EnableControls();
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
