//https://chat.openai.com/share/4f16dedd-7227-4234-ad25-c584b8290fe5
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TowerBuilder : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase grassTile;
    void Update()
    {
        if (TowerSelector.main.GetSelectedTower().prefab == null) return;

        if (Input.GetMouseButtonDown(0)) 
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector3Int cellPosition = tilemap.WorldToCell(mouseWorldPos);

            if (tilemap.GetTile(cellPosition) == grassTile && !IsTowerAtPosition(cellPosition))
            {
                SpawnTower(cellPosition);
            }
        }

        if (TowerSelector.main.GetSelectedTower().prefab != null)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f;

            TowerSelector.main.GetSelectedTower().prefab.transform.position = mousePos;
        }

        if (Input.GetMouseButtonDown(1) && TowerSelector.main.GetSelectedTower().prefab != null)
        {
            Destroy(TowerSelector.main.GetSelectedTower().prefab);
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
        Instantiate(towerToBuild.prefab, tilemap.GetCellCenterWorld(position), Quaternion.identity);
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
}
