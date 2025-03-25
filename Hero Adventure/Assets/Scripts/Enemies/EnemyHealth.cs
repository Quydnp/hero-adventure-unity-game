using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth = 3;
    [SerializeField] private GameObject deathVFXPrefab;
    [SerializeField] private float knockBackThrust = 15f;

    private int currentHealth;
    private Knockback knockback;
    private Flash flash;
    private AudioManager audioManager;

    private void Awake()
    {
        flash = GetComponent<Flash>();
        knockback = GetComponent<Knockback>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        knockback.GetKnockedBack(PlayerController.Instance.transform, knockBackThrust);
        audioManager.PlaySFX(audioManager.hitClip);
        StartCoroutine(flash.FlashRoutine());
        StartCoroutine(CheckDetectDeathRoutine());
    }

    private IEnumerator CheckDetectDeathRoutine()
    {
        yield return new WaitForSeconds(flash.GetRestoreMatTime());
        DetectDeath();
    }

    public void DetectDeath()
    {
        if (currentHealth <= 0)
        {
            int scoreToAdd = 0;
            if (gameObject.CompareTag("Slime"))
            {
                scoreToAdd = 1;
            }
            else if (gameObject.CompareTag("Ghost"))
            {
                scoreToAdd = 2;
            }

            ScoreManager.Instance.AddScore(scoreToAdd);

            Instantiate(deathVFXPrefab, transform.position, Quaternion.identity);
            audioManager.PlaySFX(audioManager.killClip);
            GetComponent<PickUpSpawner>().DropItems();

            Destroy(gameObject);

        }
    }

    public void OnDestroy()
    {
        var tag = gameObject.tag;
        if (gameObject.CompareTag(tag))
        {
            var remainingSlimes = GameObject.FindWithTag(tag);

            if (remainingSlimes == null)
            {
                GameObject areaExit = GameObject.FindWithTag("AreaExit");
                if (areaExit != null)
                {
                    BoxCollider2D collider = areaExit.GetComponent<BoxCollider2D>();
                    collider.isTrigger = true;
                    GameObject exitArrow = areaExit.transform.GetChild(2).gameObject;
                    exitArrow.SetActive(true);
                }
            }
        }

    }
}
