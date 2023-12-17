using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI currencyUI;
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
    }
    /*public void SetSelectedTower()
    {

    }*/

}
