using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    static BuildManager main;

    [SerializeField] GameObject[] towerPrefabs;

    int selectedTower = 0;

    private void Awake()
    {
        main = this;
    }
    public GameObject GetSelectedTower()
    {
        return towerPrefabs[selectedTower];
    }
}
