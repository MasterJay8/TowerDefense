using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Shop
{
    public string name;
    public int cost;
    public GameObject prefab;

    public Shop(string name, int cost, GameObject prefab)
    {
        this.name = name;
        this.cost = cost;
        this.prefab = prefab;
    }
}
