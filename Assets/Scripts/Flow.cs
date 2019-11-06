//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
//public class Flow : MonoBehaviour
//{
//    // Start is called before the first frame update
//    void Start()
//    {
//        
//    }
//
//    // Update is called once per frame
//    void Update()
//    {
//        
//    }
//
//    void addWater(Vector3Int pos, Tile tile)
//    {
//        // TODO : Switch to watered tile
//    }
//
//    void hasWater(Tile tile)
//    {
//        // TODO : Check if tile belongs to water array
//    }
//
//    void isPipe(Tile tile)
//    {
//        // TODO : Check if tile belongs to pipe array
//    }
//
//    bool flowWaterTo(Vector3Int pos, tile)
//    {
//        bool shouldWater = false;
//        if (_leftEdgeTiles.Contains(pos))
//        {
//            Tile leftTile = tilemap.getTile(pos.left);
//            shouldWater ||= (hasWater(leftTile) && _rightEdgeTiles.Contains(leftTile));
//        }
//        if (_rightEdgeTiles.Contains(pos))
//        {
//            Tile rightTile = tilemap.getTile(pos.right);
//            shouldWater ||= (hasWater(rightTile) && _leftEdgeTiles.Contains(rightTile));
//        }
//        if (_upEdgeTiles.Contains(pos))
//        {
//            Tile upTile = tilemap.getTile(pos.up);
//            shouldWater ||= (hasWater(upTile) && _downEdgeTiles.Contains(upTile));
//        }
//        if (_downEdgeTiles.Contains(pos))
//        {
//            Tile downTile = tilemap.getTile(pos.down);
//            shouldWater ||= (hasWater(downTile) && _upEdgeTiles.Contains(downTile));
//        }
//
//        return shouldWater;
//    }
//
//    bool addFlow()
//    {
//        List<Vector3Int> toWaterPos = new List<Vector3Int>();
//        var bounds = _tilemap.cellBounds;
//        for (int x=bounds.xMin; x<bounds.xMax; ++x)
//        {
//            for (int y=bounds.yMin; y<bounds.yMax; ++y)
//            {
//                for (int z=bounds.zMin; z<bounds.zMax; ++z)
//                {
//                    Vector3Int pos = new Vector3Int(x, y, z);
//                    var _tile = getTile(pos);
//
//                    if (isPipe(tile) && !hasWater(tile))
//                        if (willWater(pos, tile))
//                            toWaterPos.Add(pos);
//                }
//            }
//        }
//
//        toWaterPos.ForEach(pos => addWater(pos));
//
//        return (watered > 0);
//    }
//
//}
