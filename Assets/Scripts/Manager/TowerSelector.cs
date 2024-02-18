using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class TowerSelector : MonoBehaviour
{
    public static TowerSelector main;

    [SerializeField] Shop[] shop;
    [SerializeField] Color normalColor;
    [SerializeField] Color selectedColor;
    [SerializeField] Button[] buttons;
    //private Button[] buttons;
    private Button selectedButton;

    int selectedTower = 0;

    private void Awake()
    {
        main = this;
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(() => SelectButton(button));
        }
    }
    public Shop GetSelectedTower()
    {
        return shop[selectedTower];
    }

    public void SetSelectedTower(int selectedTower)
    {
        if (this.selectedTower != selectedTower)
        {
            this.selectedTower = selectedTower;
        }
        else
        {
            this.selectedTower = 0;
        }
    }
    void SelectButton(Button button)
    {
        if (selectedButton == button)
        {
            button.image.color = normalColor;
            selectedButton = null;
        }
        else
        {
            if (selectedButton != null)
            {
                selectedButton.image.color = normalColor;
                selectedButton = null;
            }
            selectedButton = button;
            selectedButton.image.color = selectedColor;
        }
    }

}
//hex - #633838
