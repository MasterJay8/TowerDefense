using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSelector : MonoBehaviour
{
    public static TowerSelector main;

    [SerializeField] Shop[] shop;

    int selectedTower = 0;

    private void Awake()
    {
        main = this;
    }
    public Shop GetSelectedTower()
    {
        return shop[selectedTower];
    }

    public void SetSelectedTower(int selectedTower)
    {
        this.selectedTower = selectedTower;
    }


}
