﻿using System.Collections;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveSystem : Singleton<SaveSystem>
{
    private static string path = Application.dataPath + "/GameData/playerData.json";

    protected override void Awake()
    {
        base.Awake();
    }

    public void Save(Vector3 playerPosition)
    {
        string directory = Path.GetDirectoryName(path);

        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
            Debug.Log("Created directory: " + directory);
        }

        string[] weapons = ActiveInventory.Instance.GetWeaponNames().ToArray();

        PlayerData data = new PlayerData(playerPosition)
        {
            currentHealth = PlayerHealth.Instance.CurrentHealth,
            maxHealth = PlayerHealth.Instance.MaxHealth,
            currentStamina = Stamina.Instance.CurrentStamina,
            coin = EconomyManager.Instance.CurrentGold,
            point = ScoreManager.Instance.CurrentScore,
            sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name,
            weapons = weapons
        };

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);
        Debug.Log("Game Saved: " + path);
        Debug.Log("Weapons: " + string.Join("-", data.weapons));
    }

    public void Load()
    {
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);

            Debug.Log("Game Loaded!");
            StartCoroutine(LoadSceneAndRestore(data));
        }
        else
        {
            Debug.LogWarning("No save file found!");
        }
    }

    private IEnumerator LoadSceneAndRestore(PlayerData data)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(data.sceneName);
        yield return new WaitUntil(() => asyncLoad.isDone);

        PlayerController player = FindFirstObjectByType<PlayerController>();

        if (player != null)
        {
            player.transform.position = new Vector3(data.position[0], data.position[1], data.position[2]);
            PlayerHealth.Instance.SetCurrentHealth(data.currentHealth);
            PlayerHealth.Instance.SetMaxHealth(data.maxHealth);
            Stamina.Instance.SetCurrentStamina(data.currentStamina);
            EconomyManager.Instance.SetCurrentGold(data.coin);
            ScoreManager.Instance.SetCurrentScore(data.point);
            if (data.weapons != null)
            {
                for (int i = 0; i < data.weapons.Length; i++)
                {
                    ActiveInventory.Instance.SetInventoryActiveByIndex(i);
                }
            }
        }

        // Pause the game after loading the scene
        PauseMenu pauseMenu = FindFirstObjectByType<PauseMenu>();
        if (pauseMenu != null)
        {
            pauseMenu.Pause();
        }
    }
}
