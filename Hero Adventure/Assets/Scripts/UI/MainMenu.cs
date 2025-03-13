using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        foreach (GameObject obj in FindObjectsByType<GameObject>(FindObjectsSortMode.None))
        {
            if (obj.scene.buildIndex == -1) 
                Destroy(obj);
        }
        SceneManager.LoadScene("Scene1");
        Time.timeScale = 1f;
    }

    public void Continue()
    {
        SaveSystem.Instance.Load();
    }

    public void Quit()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
}
