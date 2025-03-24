using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Shop : MonoBehaviour
{
    private Sprite _sprite = null;

    public GameObject _currentPlayer; // Player hiện tại (đã được gán trong Inspector)

    public RuntimeAnimatorController newAnimatorController;

    public void BuyDamage()
    {
        Debug.Log("Click BuyDamage");
    }

    public void BuyHealth()
    {
        if (EconomyManager.Instance.CurrentGold > 0)
        {
            EconomyManager.Instance.MinusCurrentGold(1);
            PlayerHealth.Instance.SetMaxHealthWithoutUpdateUI(PlayerHealth.Instance.MaxHealth + 1);
            PlayerHealth.Instance.SetCurrentHealth(PlayerHealth.Instance.CurrentHealth + 1);
            Debug.Log("Max health: " + PlayerHealth.Instance.MaxHealth);
        }
        else
        {
            Debug.Log("Not enough gold");
        }
    }

    public void SetSprite(Sprite sprite)
    {
        _sprite = sprite;
    }

    public void BuyBowWeapon()
    {
        if (EconomyManager.Instance.CurrentGold >= 5 && !ActiveInventory.Instance.IsBoughtBowWeapon)
        {
            if (_sprite == null)
            {
                Debug.Log("Sprite is null");

                return;
            }
            Debug.Log(EconomyManager.Instance.CurrentGold);
            ActiveInventory.Instance.IsBoughtBowWeapon = true;
            EconomyManager.Instance.MinusCurrentGold(5);
            ActiveInventory.Instance.SetInventoryActiveByIndex(1);
        }
        else
        {
            Debug.Log("Not enough gold");
        }
    }

    public void setPlayerOldPrefab(GameObject oldPlayer)
    {
        _currentPlayer = oldPlayer;
    }

    public void SetGameAnimator(RuntimeAnimatorController animatorController)
    {
        newAnimatorController = animatorController;
    }

    public void BuyKnightPlayer()
    {
        if (EconomyManager.Instance.CurrentGold >= 1)
        {
            if (PlayerController.Instance.IsKnightPlayer)
            {
                Debug.Log("Already Knight Player");
                return;
            }
            Animator animator = _currentPlayer.GetComponent<Animator>();
            animator.runtimeAnimatorController = newAnimatorController;

            PlayerController.Instance.setKnightProp(8f, 4f);
            PlayerController.Instance.IsKnightPlayer = true;
            EconomyManager.Instance.MinusCurrentGold(5);
        }
        else
        {
            Debug.Log("Not enough gold");
        }
    }
}
