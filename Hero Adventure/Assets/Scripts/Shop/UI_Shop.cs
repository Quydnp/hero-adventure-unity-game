using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Shop : MonoBehaviour
{
    private Transform container;
    private Transform shopItemTemplate;
    private Transform bow;
    private Transform stamina;
    private Transform health;
    private void Awake()
    {
        container = transform.Find("Container");
        shopItemTemplate = container.Find("ShopItemTemplate");
        bow = shopItemTemplate.Find("Bow");
        stamina = shopItemTemplate.Find("Stamina");
        health = shopItemTemplate.Find("Health");
        shopItemTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
       
        var bowText = bow.Find("BowText").GetComponent<TextMeshProUGUI>().text;
        
        
    }

 
}
