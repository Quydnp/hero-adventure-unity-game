using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    GameObject pauseMenu;

    [SerializeField]
    GameObject weapon;

    [SerializeField]
    Transform shopItemTemplate;

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;

        weapon.GetComponent<MouseFollow>().enabled = false;

        PlayerController.Instance.UnableControls();
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;

        weapon.GetComponent<MouseFollow>().enabled = true;
        shopItemTemplate.gameObject.SetActive(false);
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
        if (shopItemTemplate.gameObject.activeSelf)
        {
            shopItemTemplate.gameObject.SetActive(false);
        }
        else
        {
            shopItemTemplate.gameObject.SetActive(true);
        }
    }
}
