using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    private Sprite _sprite = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void BuyDamage()
    {
        Debug.Log("Click BuyDamage");

        //EconomyManager.Instance.MinusCurrentGold(1);
        // Stamina.Instance.SetMaxStaminaWithoutUpdateUI(Stamina.Instance.getMaxStamina() + 1);
        // Debug.Log(Stamina.Instance.getMaxStamina() + "Max stamina");
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
}
