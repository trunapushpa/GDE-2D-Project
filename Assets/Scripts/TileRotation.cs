using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileRotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pz.z = 0;
            // convert mouse click's position to Grid position
            GridLayout gridLayout = transform.parent.GetComponentInParent<GridLayout>();
            Vector3Int cellPosition = gridLayout.WorldToCell(pz);
            Debug.Log(cellPosition);
            if (cellPosition.x == 0 && cellPosition.y == 16)
            {
                Tilemap tilemap = this.GetComponents<Tilemap>()[0];
                Debug.Log(tilemap.GetTile(cellPosition));
                if (tilemap.GetTile(cellPosition).name.Equals("landscapeTiles_064.png"))
                {
                    Sprite[] landscapeTiles = Resources.LoadAll<Sprite>("landscapeTiles_sheet");
                    Tile tile = ScriptableObject.CreateInstance<Tile>();
                    tile.sprite = landscapeTiles[72];
                    tile.name = "landscapeTiles_072.png";
                    tilemap.SetTile(cellPosition, tile);
                }
                else
                {
                    Sprite[] landscapeTiles = Resources.LoadAll<Sprite>("landscapeTiles_sheet");
                    Tile tile = ScriptableObject.CreateInstance<Tile>();
                    tile.sprite = landscapeTiles[64];
                    tile.name = "landscapeTiles_064.png";
                    tilemap.SetTile(cellPosition, tile);
                }
            }
        }
    }
}
