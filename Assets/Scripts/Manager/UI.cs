using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI currencyUI;
    [SerializeField] TextMeshProUGUI baseHealthUI;
    [SerializeField] Animator animator;

    bool isShopOpen = true;
    public void ToggleMenu()
    {
        isShopOpen = !isShopOpen;
        animator.SetBool("ShopOpen", !isShopOpen);
    }
    private void OnGUI()
    {
        currencyUI.text = Manager.main.currency.ToString();
        baseHealthUI.text = Manager.main.baseHealth.ToString();
    }
}
