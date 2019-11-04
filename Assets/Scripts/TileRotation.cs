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
            Tilemap tilemap = this.GetComponents<Tilemap>()[0];
            
            // I Rotation
            if (tilemap.GetTile(cellPosition).name.Equals("landscapeTiles_064.png"))
            {
                Sprite[] landscapeTiles = Resources.LoadAll<Sprite>("landscapeTiles_sheet");
                Tile tile = ScriptableObject.CreateInstance<Tile>();
                tile.sprite = landscapeTiles[72];
                tile.name = "landscapeTiles_072.png";
                tilemap.SetTile(cellPosition, tile);
            }
            else if (tilemap.GetTile(cellPosition).name.Equals("landscapeTiles_072.png"))
            {
                Sprite[] landscapeTiles = Resources.LoadAll<Sprite>("landscapeTiles_sheet");
                Tile tile = ScriptableObject.CreateInstance<Tile>();
                tile.sprite = landscapeTiles[64];
                tile.name = "landscapeTiles_064.png";
                tilemap.SetTile(cellPosition, tile);
            }
            
            // L Rotation
            else if (tilemap.GetTile(cellPosition).name.Equals("landscapeTiles_079.png"))
            {
                Sprite[] landscapeTiles = Resources.LoadAll<Sprite>("landscapeTiles_sheet");
                Tile tile = ScriptableObject.CreateInstance<Tile>();
                tile.sprite = landscapeTiles[86];
                tile.name = "landscapeTiles_086.png";
                tilemap.SetTile(cellPosition, tile);
            }
            else if (tilemap.GetTile(cellPosition).name.Equals("landscapeTiles_086.png"))
            {
                Sprite[] landscapeTiles = Resources.LoadAll<Sprite>("landscapeTiles_sheet");
                Tile tile = ScriptableObject.CreateInstance<Tile>();
                tile.sprite = landscapeTiles[78];
                tile.name = "landscapeTiles_078.png";
                tilemap.SetTile(cellPosition, tile);
            }
            else if (tilemap.GetTile(cellPosition).name.Equals("landscapeTiles_078.png"))
            {
                Sprite[] landscapeTiles = Resources.LoadAll<Sprite>("landscapeTiles_sheet");
                Tile tile = ScriptableObject.CreateInstance<Tile>();
                tile.sprite = landscapeTiles[71];
                tile.name = "landscapeTiles_071.png";
                tilemap.SetTile(cellPosition, tile);
            }
            else if (tilemap.GetTile(cellPosition).name.Equals("landscapeTiles_071.png"))
            {
                Sprite[] landscapeTiles = Resources.LoadAll<Sprite>("landscapeTiles_sheet");
                Tile tile = ScriptableObject.CreateInstance<Tile>();
                tile.sprite = landscapeTiles[79];
                tile.name = "landscapeTiles_079.png";
                tilemap.SetTile(cellPosition, tile);
            }
            
            // T Rotation
            else if (tilemap.GetTile(cellPosition).name.Equals("landscapeTiles_129.png"))
            {
                Sprite[] landscapeTiles = Resources.LoadAll<Sprite>("landscapeTiles_sheet");
                Tile tile = ScriptableObject.CreateInstance<Tile>();
                tile.sprite = landscapeTiles[130];
                tile.name = "landscapeTiles_130.png";
                tilemap.SetTile(cellPosition, tile);
            }
            else if (tilemap.GetTile(cellPosition).name.Equals("landscapeTiles_130.png"))
            {
                Sprite[] landscapeTiles = Resources.LoadAll<Sprite>("landscapeTiles_sheet");
                Tile tile = ScriptableObject.CreateInstance<Tile>();
                tile.sprite = landscapeTiles[131];
                tile.name = "landscapeTiles_131.png";
                tilemap.SetTile(cellPosition, tile);
            }
            else if (tilemap.GetTile(cellPosition).name.Equals("landscapeTiles_131.png"))
            {
                Sprite[] landscapeTiles = Resources.LoadAll<Sprite>("landscapeTiles_sheet");
                Tile tile = ScriptableObject.CreateInstance<Tile>();
                tile.sprite = landscapeTiles[132];
                tile.name = "landscapeTiles_132.png";
                tilemap.SetTile(cellPosition, tile);
            }
            else if (tilemap.GetTile(cellPosition).name.Equals("landscapeTiles_132.png"))
            {
                Sprite[] landscapeTiles = Resources.LoadAll<Sprite>("landscapeTiles_sheet");
                Tile tile = ScriptableObject.CreateInstance<Tile>();
                tile.sprite = landscapeTiles[129];
                tile.name = "landscapeTiles_129.png";
                tilemap.SetTile(cellPosition, tile);
            }
        }
    }
}
