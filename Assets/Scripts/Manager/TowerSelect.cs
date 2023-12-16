using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSelecter : MonoBehaviour
{
    public static TowerSelecter main;

    //[SerializeField] GameObject[] towerPrefabs;
    [SerializeField] Shop[] shop;

    int selectedTower = 0;

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
}
