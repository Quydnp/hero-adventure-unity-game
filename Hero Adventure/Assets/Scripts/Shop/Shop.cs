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

        //if (EconomyManager.Instance.CurrentGold >= 0)
        //{
        //    Debug.Log("Click Buy Player");

        //    if (_currentPlayer != null)
        //    {

        //        _currentPlayer.SetActive(false);

        //    }

        //    if (_newPlayer != null)
        //    {


        //        Debug.Log("new player is not null");




        //        Vector3 oldPosition = _currentPlayer.transform.position;
        //        Quaternion oldRotation = _currentPlayer.transform.rotation;



        //        // Tạo Player mới từ Prefab
        //        GameObject newPlayer = Instantiate(_newPlayer, oldPosition, oldRotation);
        //        Debug.Log("Tên của new Player: " +  newPlayer.name);
        //        // Cập nhật Player hiện tại
        //        oldWeapon.SetParent(newPlayer.transform);
        //        PlayerHealth playerHealth = newPlayer.GetComponent<PlayerHealth>();
        //        if (playerHealth != null)
        //        {
        //            // Gán gameOverPanel từ UI chính vào Player mới
        //            playerHealth.SetGameOverPanel(_gameOverPanel);
        //            Debug.Log("GameOverPanel đã được gán cho Player mới!");
        //        }
        //        _currentPlayer = newPlayer;

        //        Debug.Log("Tên của Player hiện tại: " + _currentPlayer.name);

        //    }
        //    else
        //    {
        //        Debug.Log("new player is null");
        //    }



        //}
        //else
        //{
        //    Debug.Log("Not enough gold");
        //}
    }
}
