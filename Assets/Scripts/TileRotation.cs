using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = System.Random;

public class TileRotation : MonoBehaviour {
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
    private readonly int[] _probabilites = {400, 400, 30, 30, 30, 30, 18, 18, 18, 18, 8};
    private readonly int[] _fTiles = {64, 72, 79, 86, 78, 71, 129, 130, 131, 132, 128, 66};
    private readonly int[] _upEdgeTiles = {134, 135, 136, 140, 141, 142, 143, 72, 79, 86, 130, 131, 132, 128, 66, 67};
    private readonly int[] _downEdgeTiles = {134, 137, 138, 139, 140, 142, 143, 72, 78, 71, 129, 130, 132, 128, 66, 67};
    private readonly int[] _leftEdgeTiles = {133, 135, 138, 139, 140, 141, 143, 64, 79, 71, 129, 130, 131, 128, 66, 67};
    private readonly int[] _rightEdgeTiles = {133, 136, 137, 139, 141, 142, 143, 64, 86, 78,129, 131, 132, 128, 66, 67};
    
    private Vector3Int _draggedTile = new Vector3Int(-50, -50, -50);
    private Random _rand;
    private readonly Vector3Int[] _tilePalette = {new Vector3Int(0, 15, 0), new Vector3Int(-2, 13, 0)};

    private int GetTileNo(Vector3Int cellPosition) {
        var tileNo = -1;
        if (_tilemap.GetTile(cellPosition))
            tileNo = Int32.Parse(_tilemap.GetTile(cellPosition).name.Substring(15, 3));
        return tileNo;
    }
    
    private bool IsPipe(int tileNo) {
        return tileNo != 66 && (_eTiles.Contains(tileNo) || _fTiles.Contains(tileNo));
    }

    private bool IsHouse(int tileNo) {
        // TODO: Check if the current cell is a house
        return false;
    }

    private bool HasWater(int tileNo) {
        return _fTiles.Contains(tileNo);
    }

    private void FillWaterInTile(Vector3Int cellPosition) {
        var tileNoToFill = GetTileNo(cellPosition);
        if (!_eTiles.Contains(tileNoToFill))
            return;
        int index = Array.IndexOf(_eTiles, tileNoToFill);
        Tile tile = ScriptableObject.CreateInstance<Tile>();
        tile.sprite = _landscapeTiles[_fTiles[index]];
        tile.transform = Matrix4x4.Scale(new Vector3((float) 0.8, (float) 0.8, 1));
        tile.name = "landscapeTiles_" + String.Format("{0:000}", _fTiles[index]) + ".png";
        _tilemap.SetTile(cellPosition, tile);
    }

    private int GetNextTileIndex() {
        int toss = _rand.Next(1000);
        int index = 0;
        int curr = 0;
        foreach (int p in _probabilites) {
            curr += p;
            if (toss <= curr)
                return index;
            index++;
        }

        return index;
    }

    // Start is called before the first frame update
    void Start() {
        _camera = Camera.main;
        _gridLayout = transform.parent.GetComponentInParent<GridLayout>();
        _tilemap = GetComponents<Tilemap>()[0];
        _dragLayerTilemap = GetComponentsInChildren<Tilemap>()[1];
        _landscapeTiles = Resources.LoadAll<Sprite>("landscapeTiles_sheet");
        _tileNo = -1;
        _rand = new Random(Guid.NewGuid().GetHashCode());
        foreach (var tilePos in _tilePalette) {
            Tile tile = ScriptableObject.CreateInstance<Tile>();
            int index = GetNextTileIndex();
            tile.sprite = _landscapeTiles[_eTiles[index]];
            tile.transform = Matrix4x4.Scale(new Vector3((float) 0.8, (float) 0.8, 1));
            tile.name = "landscapeTiles_" + String.Format("{0:000}", _eTiles[index]) + ".png";
            _tilemap.SetTile(tilePos, tile);
        }
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyUp("space"))
            if (!AddFlow())
                Debug.Log("GAME OVER");
        if (Input.GetMouseButtonDown(1) && !GamePlay.isGameOver()) {
            Vector3 pz = _camera.ScreenToWorldPoint(Input.mousePosition);
            pz.z = 0;

            // convert mouse click's position to Grid position
            Vector3Int cellPosition = _gridLayout.WorldToCell(pz);
            //Debug.Log(cellPosition);
            //Debug.Log(_tilemap.GetTile(cellPosition));

            if (!_tilePalette.Contains(cellPosition)) {
                return;
            }

            int tileNoToRotate = GetTileNo(cellPosition);

            // I Rotation
            if (_eITiles.Contains(tileNoToRotate)) {
                int indexOfRotatedTile = (Array.IndexOf(_eITiles, tileNoToRotate) + 1) % 2;
                Tile tile = ScriptableObject.CreateInstance<Tile>();
                tile.sprite = _landscapeTiles[_eITiles[indexOfRotatedTile]];
                tile.transform = Matrix4x4.Scale(new Vector3((float) 0.8, (float) 0.8, 1));
                tile.name = "landscapeTiles_" + String.Format("{0:000}", _eITiles[indexOfRotatedTile]) + ".png";
                _tilemap.SetTile(cellPosition, tile);
            }

            // L Rotation
            else if (_eLTiles.Contains(tileNoToRotate)) {
                int indexOfRotatedTile = (Array.IndexOf(_eLTiles, tileNoToRotate) + 1) % 4;
                Tile tile = ScriptableObject.CreateInstance<Tile>();
                tile.sprite = _landscapeTiles[_eLTiles[indexOfRotatedTile]];
                tile.transform = Matrix4x4.Scale(new Vector3((float) 0.8, (float) 0.8, 1));
                tile.name = "landscapeTiles_" + String.Format("{0:000}", _eLTiles[indexOfRotatedTile]) + ".png";
                _tilemap.SetTile(cellPosition, tile);
            }

            // T Rotation
            else if (_eTTiles.Contains(tileNoToRotate)) {
                int indexOfRotatedTile = (Array.IndexOf(_eTTiles, tileNoToRotate) + 1) % 4;
                Tile tile = ScriptableObject.CreateInstance<Tile>();
                tile.sprite = _landscapeTiles[_eTTiles[indexOfRotatedTile]];
                tile.transform = Matrix4x4.Scale(new Vector3((float) 0.8, (float) 0.8, 1));
                tile.name = "landscapeTiles_" + String.Format("{0:000}", _eTTiles[indexOfRotatedTile]) + ".png";
                _tilemap.SetTile(cellPosition, tile);
            }
        }
    }

    private void OnMouseDown() {
        if (GamePlay.isGameOver())
            return;

        var pz = _camera.ScreenToWorldPoint(Input.mousePosition);
        pz.z = 0;
        var cellPosition = _gridLayout.WorldToCell(pz);
        if (_tilemap.GetTile(cellPosition) && _tilePalette.Contains(cellPosition)) {
            _tileNo = GetTileNo(cellPosition);
            _draggedTile = cellPosition;
            if (!_eITiles.Contains(_tileNo) && !_eLTiles.Contains(_tileNo) && !_eTTiles.Contains(_tileNo) &&
                !_eXTiles.Contains(_tileNo)) {
                _tileNo = -1;
                _draggedTile = new Vector3Int(-50, -50, -50);
            }
        }
    }

    private void OnMouseDrag() {
        if (_tileNo != -1) {
            var pz = _camera.ScreenToWorldPoint(Input.mousePosition);
            pz.z = 0;
            var cellPosition = _gridLayout.WorldToCell(pz);

            Tile tile = ScriptableObject.CreateInstance<Tile>();
            tile.sprite = _landscapeTiles[_tileNo];
            tile.transform = Matrix4x4.Scale(new Vector3((float) 0.8, (float) 0.8, 1));
            _dragLayerTilemap.ClearAllTiles();
            _dragLayerTilemap.SetTile(cellPosition, tile);
        }
    }

    private void OnMouseUp() {
        if (_dragLayerTilemap.GetUsedTilesCount() == 1) {
            var pz = _camera.ScreenToWorldPoint(Input.mousePosition);
            pz.z = 0;
            var cellPosition = _gridLayout.WorldToCell(pz);
            if (_tilemap.GetTile(cellPosition) && !_tilePalette.Contains(cellPosition)) {
                int tileNo = GetTileNo(cellPosition);
                // 67 = Ground, 83 = Brown Farm
                if (_tilemap.GetTile(cellPosition).name[0] == 'l' && (tileNo == 67 || tileNo == 83)) {
                    Tile tile = ScriptableObject.CreateInstance<Tile>();
                    tile.sprite = _landscapeTiles[_tileNo];
                    tile.name = "landscapeTiles_" + String.Format("{0:000}", _tileNo) + ".png";
                    tile.transform = Matrix4x4.Scale(new Vector3((float) 0.8, (float) 0.8, 1));
                    _tilemap.SetTile(cellPosition, tile);

                    // New Tile in Tile Palette at Position just used
                    tile = ScriptableObject.CreateInstance<Tile>();
                    int index = GetNextTileIndex();
                    tile.sprite = _landscapeTiles[_eTiles[index]];
                    tile.transform = Matrix4x4.Scale(new Vector3((float) 0.8, (float) 0.8, 1));
                    tile.name = "landscapeTiles_" + String.Format("{0:000}", _eTiles[index]) + ".png";
                    _tilemap.SetTile(_draggedTile, tile);
                }
            }
        }

        _dragLayerTilemap.ClearAllTiles();
        _tileNo = -1;
        _draggedTile = new Vector3Int(-50, -50, -50);
    }
    
    private bool WillWater(Vector3Int pos, int tileNo) {
        bool shouldWater = false;
        if (_leftEdgeTiles.Contains(tileNo)) {
            int leftTile = GetTileNo(pos + Vector3Int.left);
            shouldWater |= (HasWater(leftTile) && _rightEdgeTiles.Contains(leftTile) && 
                            (IsPipe(leftTile) || IsPipe(tileNo)));
        }
        if (_rightEdgeTiles.Contains(tileNo)) {
            int rightTile = GetTileNo(pos + Vector3Int.right);
            shouldWater |= (HasWater(rightTile) && _leftEdgeTiles.Contains(rightTile) &&
                            (IsPipe(rightTile) || IsPipe(tileNo)));
        }
        if (_upEdgeTiles.Contains(tileNo)) {
            int upTile = GetTileNo(pos + Vector3Int.up);
            shouldWater |= (HasWater(upTile) && _downEdgeTiles.Contains(upTile) && 
                            (IsPipe(upTile) || IsPipe(tileNo)));
        }
        if (_downEdgeTiles.Contains(tileNo)) {
            int downTile = GetTileNo(pos + Vector3Int.down);
            shouldWater |= (HasWater(downTile) && _upEdgeTiles.Contains(downTile) && 
                            (IsPipe(downTile) || IsPipe(tileNo)));
        }

        return shouldWater;
    }

    public bool AddFlow() {
        var toWaterPos = new List<Vector3Int>();
        var bounds = _tilemap.cellBounds;

        var watered = 0;
        bool gameOver = false;
        for (var x=bounds.xMin; x<bounds.xMax; ++x) {
            for (var y=bounds.yMin; y<bounds.yMax; ++y) {
                for (var z=bounds.zMin; z<bounds.zMax; ++z) {
                    var pos = new Vector3Int(x, y, z);
                    var tileNo = GetTileNo(pos);
                    if (IsPipe(tileNo) && !HasWater(tileNo) && WillWater(pos, tileNo)) {
                        toWaterPos.Add(pos);
                        ++watered;
                    }

                    if (!IsPipe(tileNo) && !HasWater(tileNo) && WillWater(pos, tileNo) && !IsHouse(tileNo))
                        gameOver = true;
                }
            }
        }

        toWaterPos.ForEach(pos => FillWaterInTile(pos));

        return !gameOver && (watered > 0);
    }
}