//https://chat.openai.com/share/4f16dedd-7227-4234-ad25-c584b8290fe5
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TowerBuilder : MonoBehaviour
{
    public GameObject towerPrefab;
    public Tilemap tilemap;
    public TileBase grassTile;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector3Int cellPosition = tilemap.WorldToCell(mouseWorldPos);

            if (tilemap.GetTile(cellPosition) == grassTile && !IsTowerAtPosition(cellPosition))
            {
                SpawnTower(cellPosition);
                //Debug.Log(cellPosition);
            }
        }
    }

    void SpawnTower(Vector3Int position)
    {
        Instantiate(towerPrefab, tilemap.GetCellCenterWorld(position), Quaternion.identity);
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
