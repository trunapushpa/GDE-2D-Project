using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileRotation : MonoBehaviour
{
    private Camera _camera;
    private GridLayout _gridLayout;
    private Tilemap _tilemap;
    private Tilemap _dragLayerTilemap;
    private Sprite[] _landscapeTiles;
    private int _tileNo;
    private readonly int[] _fITiles = {64, 72};
    private readonly int[] _fLTiles = {79, 86, 78, 71};
    private readonly int[] _fTTiles = {129, 130, 131, 132};
    private readonly int[] _fXTiles = {128};
    private readonly int[] _eITiles = {133, 134};
    private readonly int[] _eLTiles = {135, 136, 137, 138};
    private readonly int[] _eTTiles = {139, 140, 141, 142};
    private readonly int[] _eXTiles = {143};
    private readonly int[] _eTiles = {133, 134, 135, 136, 137, 138, 139, 140, 141, 142, 143};
    private readonly int[] _fTiles = {64, 72, 79, 86, 78, 71, 129, 130, 131, 132, 128};
    
    private void FillWaterInTile(Vector3Int cellPosition)
    {
        int tileNoToFill = -1;
        if (_tilemap.GetTile(cellPosition))
            tileNoToFill = Int32.Parse(_tilemap.GetTile(cellPosition).name.Substring(15, 3));
        if (!_eTiles.Contains(tileNoToFill))
            return;
        int index = Array.IndexOf(_eTiles, tileNoToFill);
        Tile tile = ScriptableObject.CreateInstance<Tile>();
        tile.sprite = _landscapeTiles[_fTiles[index]];
        tile.name = "landscapeTiles_" + String.Format("{0:000}", _fTiles[index]) + ".png";
        _tilemap.SetTile(cellPosition, tile);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        _gridLayout = transform.parent.GetComponentInParent<GridLayout>();
        _tilemap = GetComponents<Tilemap>()[0];
        _dragLayerTilemap = GetComponentsInChildren<Tilemap>()[1];
        _landscapeTiles = Resources.LoadAll<Sprite>("landscapeTiles_sheet");
        _tileNo = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && !GamePlay.isGameOver())
        {
            Vector3 pz = _camera.ScreenToWorldPoint(Input.mousePosition);
            pz.z = 0;

            // convert mouse click's position to Grid position
            Vector3Int cellPosition = _gridLayout.WorldToCell(pz);
            //Debug.Log(cellPosition);
            //Debug.Log(_tilemap.GetTile(cellPosition));

            int tileNoToRotate = -1;
            if (_tilemap.GetTile(cellPosition))
                tileNoToRotate = Int32.Parse(_tilemap.GetTile(cellPosition).name.Substring(15, 3));
            
            // I Rotation
            if (_eITiles.Contains(tileNoToRotate))
            {
                int indexOfRotatedTile = (Array.IndexOf(_eITiles, tileNoToRotate) + 1) % 2;
                Tile tile = ScriptableObject.CreateInstance<Tile>();
                tile.sprite = _landscapeTiles[_eITiles[indexOfRotatedTile]];
                tile.name = "landscapeTiles_" + String.Format("{0:000}", _eITiles[indexOfRotatedTile]) + ".png";
                _tilemap.SetTile(cellPosition, tile);
            }

            // L Rotation
            else if (_eLTiles.Contains(tileNoToRotate))
            {
                int indexOfRotatedTile = (Array.IndexOf(_eLTiles, tileNoToRotate) + 1) % 4;
                Tile tile = ScriptableObject.CreateInstance<Tile>();
                tile.sprite = _landscapeTiles[_eLTiles[indexOfRotatedTile]];
                tile.name = "landscapeTiles_" + String.Format("{0:000}", _eLTiles[indexOfRotatedTile]) + ".png";
                _tilemap.SetTile(cellPosition, tile);
            }
            
            // T Rotation
            else if (_eTTiles.Contains(tileNoToRotate))
            {
                int indexOfRotatedTile = (Array.IndexOf(_eTTiles, tileNoToRotate) + 1) % 4;
                Tile tile = ScriptableObject.CreateInstance<Tile>();
                tile.sprite = _landscapeTiles[_eTTiles[indexOfRotatedTile]];
                tile.name = "landscapeTiles_" + String.Format("{0:000}", _eTTiles[indexOfRotatedTile]) + ".png";
                _tilemap.SetTile(cellPosition, tile);
            }
        }
    }

    private void OnMouseDown()
    {
        if(GamePlay.isGameOver())
            return;
        var  pz = _camera.ScreenToWorldPoint(Input.mousePosition);
        pz.z = 0;
        var cellPosition = _gridLayout.WorldToCell(pz);
        if (_tilemap.GetTile(cellPosition))
        {
            _tileNo = Int32.Parse(_tilemap.GetTile(cellPosition).name.Substring(15,3));
            if (!_eITiles.Contains(_tileNo) && !_eLTiles.Contains(_tileNo) && !_eTTiles.Contains(_tileNo) &&
                !_eXTiles.Contains(_tileNo))
            {
                _tileNo = -1;
            }
            
            // TODO: Set this to a randomly generated pipe
            // _tilemap.SetTile(cellPosition, null);
        }
    }

    private void OnMouseDrag()
    {
        if (_tileNo != -1)
        {
            var  pz = _camera.ScreenToWorldPoint(Input.mousePosition);
            pz.z = 0;
            var cellPosition = _gridLayout.WorldToCell(pz);
        
            Tile tile = ScriptableObject.CreateInstance<Tile>();
            tile.sprite = _landscapeTiles[_tileNo];
            tile.transform = Matrix4x4.Scale(new Vector3((float)0.8, (float)0.8, 1));
            _dragLayerTilemap.ClearAllTiles();
            _dragLayerTilemap.SetTile(cellPosition, tile);
        }
    }

    private void OnMouseUp()
    {
        if (_dragLayerTilemap.GetUsedTilesCount() == 1)
        {
            var  pz = _camera.ScreenToWorldPoint(Input.mousePosition);
            pz.z = 0;
            var cellPosition = _gridLayout.WorldToCell(pz);
            Tile tile = ScriptableObject.CreateInstance<Tile>();
            tile.sprite = _landscapeTiles[_tileNo];
            tile.name = "landscapeTiles_" + String.Format("{0:000}", _tileNo) + ".png";
            // Debug.Log(tile.name);
            tile.transform = Matrix4x4.Scale(new Vector3((float)0.8, (float)0.8, 1));
            // Debug.Log("Left at " + cellPosition);
            _tilemap.SetTile(cellPosition, tile);
        }
        _dragLayerTilemap.ClearAllTiles();
        _tileNo = -1;
    }
}