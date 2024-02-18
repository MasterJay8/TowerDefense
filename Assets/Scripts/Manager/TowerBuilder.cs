using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;

public class TowerBuilder : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase grassTile;

    public GameObject deleteButton;
    private GameObject selectedTower;
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellPosition = tilemap.WorldToCell(mouseWorldPos);

            if (tilemap.GetTile(cellPosition) == grassTile && !IsTowerAtPosition(cellPosition) && TowerSelector.main.GetSelectedTower().prefab != null)
            {
                SpawnTower(cellPosition);
            }
            else if (TowerSelector.main.GetSelectedTower().prefab == null)
            {
                CheckForTurretSelection(cellPosition);
            }
        }

        if (TowerSelector.main.GetSelectedTower().prefab != null)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f;

            TowerSelector.main.GetSelectedTower().prefab.transform.position = mousePos;
            //selectedTower.transform.position = mousePos;
        }
    }

    void SpawnTower(Vector3Int position)
    {
        Shop towerToBuild = TowerSelector.main.GetSelectedTower();
        if (towerToBuild.cost > Manager.main.currency)
        {
            Debug.Log("You are poor");
            return;
        }
        Manager.main.SpendCurrency(towerToBuild.cost);
        //Instantiate(towerToBuild.prefab, tilemap.GetCellCenterWorld(position), Quaternion.identity);
        GameObject newTower = Instantiate(towerToBuild.prefab, tilemap.GetCellCenterWorld(position), Quaternion.identity);

        SpriteRenderer towerRenderer = newTower.GetComponentInChildren<SpriteRenderer>();

        int sortingOrder = Mathf.RoundToInt(-newTower.transform.position.y * 2);
        towerRenderer.sortingOrder = sortingOrder;
    }
    bool IsTowerAtPosition(Vector3Int position)
    {
        Collider2D[] colliders = Physics2D.OverlapPointAll(tilemap.GetCellCenterWorld(position));
        foreach (var collider in colliders)
        {
            if (collider.gameObject.CompareTag("Tower"))
            {
                return true;
            }
        }
        return false;
    }
    void CheckForTurretSelection(Vector3Int position)
    {
        Collider2D[] colliders = Physics2D.OverlapPointAll(tilemap.GetCellCenterWorld(position));
        foreach (var collider in colliders)
        {
            if (collider.gameObject.CompareTag("Tower"))
            {
                selectedTower = collider.gameObject;
                deleteButton.SetActive(true);
                return;
            }
        }
        selectedTower = null;
        deleteButton.SetActive(false);
    }
    public void DeleteTurret()
    {
        //Debug.Log(selectedTower.name);
        if (selectedTower.name == "Tower 1(Clone)") Manager.main.IncreaseCurrency(50);
        else if(selectedTower.name == "Tower 2(Clone)") Manager.main.IncreaseCurrency(75);
        //int towerCost = TowerSelector.main.GetSelectedTower().cost;
        //int refundAmount = Mathf.RoundToInt(towerCost * 0.5f);
        //Manager.main.IncreaseCurrency(refundAmount);
        Destroy(selectedTower);
        deleteButton.SetActive(false); 
    }
}
