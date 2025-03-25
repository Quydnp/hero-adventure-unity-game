using System;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public int currentHealth;
    public int maxHealth;
    public int currentStamina;
    public int coin;
    public int point;
    public string sceneName;
    public float[] position; // [x, y, z]
    public string[] weapons;
    public bool isKnightPlayer;

    public PlayerData(Vector3 position)
    {
        this.position = new float[3] { position.x, position.y, position.z };
    }
}
