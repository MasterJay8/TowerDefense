using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSelect : MonoBehaviour
{
    public static TowerSelect main;

    //[SerializeField] GameObject[] towerPrefabs;
    [SerializeField] Shop[] shop;

    int selectedTower = -1;

    private void Awake()
    {
        main = this;
    }
    //public GameObject GetSelectedTower()
    public Shop GetSelectedTower()
    {
        //return towerPrefabs[selectedTower];
        return shop[selectedTower];
    }
    public void SetSelectedTower(int selectedTower)
    {
        this.selectedTower = selectedTower;
    }
}
