using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : Singleton<PlayerHealth>
{
    public bool isDead { get; private set; }

    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float knockBackThrustAmount = 10f;
    [SerializeField] private float damageRecoveryTime = 1f;
    [SerializeField] private GameObject gameOverPanel;

    private Slider healthSlider;
    private int currentHealth;
    private bool canTakeDamage = true;
    private Knockback knockback;
    private Flash flash;
    private Vector3 spawnPosition;

    const string HEALTH_SLIDER_TEXT = "Health Slider";
    const string SCENE_TEXT = "Scene1";
    readonly int DEATH_HASH = Animator.StringToHash("Death");

    protected override void Awake()
    {
        base.Awake();

        flash = GetComponent<Flash>();
        knockback = GetComponent<Knockback>();
    }

    private void Start()
    {
        spawnPosition = transform.position;
        isDead = false;
        currentHealth = maxHealth;
        UpdateHealthSlider();
        gameOverPanel.SetActive(false);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        EnemyAI enemy = other.gameObject.GetComponent<EnemyAI>();

        if (enemy)
        {
            TakeDamage(1, other.transform);
        }
    }

    public void HealPlayer()
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += 1;
            UpdateHealthSlider();
        }
    }

    public void TakeDamage(int damageAmount, Transform hitTransform)
    {
        if (!canTakeDamage) { return; }

        knockback.GetKnockedBack(hitTransform, knockBackThrustAmount);
        StartCoroutine(flash.FlashRoutine());
        canTakeDamage = false;
        currentHealth -= damageAmount;
        StartCoroutine(DamageRecoveryRoutine());
        UpdateHealthSlider();
        CheckIfPlayerDeath();
    }

    private void CheckIfPlayerDeath()
    {
        if (currentHealth <= 0 && !isDead)
        {
            Debug.Log("Player is dead");
            isDead = true;
            currentHealth = 0;
            GetComponent<Animator>().SetTrigger(DEATH_HASH);
            StartCoroutine(DeathLoadSceneRoutine());
        }
    }

    private IEnumerator DeathLoadSceneRoutine()
    {
        Debug.Log("DeathLoadSceneRoutine");
        yield return new WaitForSeconds(0.5f);
        ShowGameOverScreen();
        
    }
    private void ShowGameOverScreen()
    {
        Time.timeScale = 0f;

        if (gameOverPanel != null)
        {   Debug.Log("ShowGameOverScreen called");
            gameOverPanel.SetActive(true);
        }
    }

    public void RestartGame()
    {
        Debug.Log("RestartGame called");
        Time.timeScale = 1f;

        transform.position = spawnPosition;

        isDead = false;
        currentHealth = maxHealth;
        UpdateHealthSlider();
        

        GetComponent<Animator>().Rebind();
        GetComponent<Animator>().Update(0f);

        EconomyManager.Instance.ResetGold();
        ScoreManager.Instance.ResetScore();
        Stamina.Instance.ResetStamina();

        gameOverPanel.SetActive(false);
        SceneManager.LoadScene(SCENE_TEXT);
        SceneManager.sceneLoaded += OnSceneLoaded;


    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == SCENE_TEXT)
        {
            CameraController.Instance.SetPlayerCameraFollow();
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

    }
    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(damageRecoveryTime);
        canTakeDamage = true;
    }


    private void UpdateHealthSlider()
    {
        if (healthSlider == null)
        {
            healthSlider = GameObject.Find(HEALTH_SLIDER_TEXT).GetComponent<Slider>();
        }

        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }
}