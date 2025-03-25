using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{
    [SerializeField]
    private GameObject slashAnimPrefab;

    [SerializeField]
    private Transform slashAnimSpawnPoint;

    [SerializeField]
    private WeaponInfo weaponInfo;
    private Transform weaponCollider;

    private Animator myAnimator;

    private GameObject slashAnim;

    private AudioManager audioManager;
    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void Start()
    {
        weaponCollider = PlayerController.Instance.GetWeaponCollider();
        slashAnimSpawnPoint = GameObject.Find("SlashSpawnPoint").transform;
    }

    public void Attack()
    {
        myAnimator.SetTrigger("Attack");
        weaponCollider.gameObject.SetActive(true);
        slashAnim = Instantiate(slashAnimPrefab, slashAnimSpawnPoint.position, Quaternion.identity);
        slashAnim.transform.parent = this.transform.parent;
        audioManager.PlaySFX(audioManager.swordClip);
    }

    public void DoneAttackingAnimEvent()
    {
        weaponCollider.gameObject.SetActive(false);
    }

    public void SwingUpFlipAnimEvent()
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);

        if (PlayerController.Instance.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void SwingDownFlipAnimEvent()
    {
        if (slashAnim != null)
        {
            slashAnim.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

            if (PlayerController.Instance.FacingLeft)
            {
                slashAnim.GetComponent<SpriteRenderer>().flipX = true;
            }
        }
    }

    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }
}
