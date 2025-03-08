using Assets.Scripts.Player;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Management
{
    public static class SaveSystem
    {
        private static string path = Application.dataPath + "/GameData/playerData.json";

        public static void SavePlayer(Vector3 position)
        {
            string directory = Path.GetDirectoryName(path);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
                Debug.Log("Created directory: " + directory);
            }

            PlayerData data = new PlayerData(position)
            {
                currentHealth = PlayerHealth.Instance.CurrentHealth,
                maxHealth = PlayerHealth.Instance.MaxHealth,
                currentStamina = Stamina.Instance.CurrentStamina,
                coin = EconomyManager.Instance.CurrentGold,
                sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name,
            };

            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(path, json);
            Debug.Log("Game Saved: " + path);
        }

        public static PlayerData LoadPlayer()
        {
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                PlayerData data = JsonUtility.FromJson<PlayerData>(json);

                PlayerHealth.Instance.SetCurrentHealth(data.currentHealth);
                PlayerHealth.Instance.SetMaxHealth(data.maxHealth);
                Stamina.Instance.SetCurrentStamina(data.currentStamina);
                EconomyManager.Instance.SetCurrentGold(data.coin);


                SceneManager.LoadScene(data.sceneName);

                Debug.Log("Game Loaded!");
                return data;
            }
            else
            {
                Debug.LogWarning("No save file found!");
                return null;
            }
        }
    }
}
