using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class TileSetup : MonoBehaviour
{
    public GameObject rock;
    void Start()
    {
        Debug.Log("dddx starting");
        Tilemap tileMap = GetComponent<Tilemap>();

        //BoundsInt bounds = tilemap.cellBounds;
        //TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

       List<Vector3> availablePlaces = new List<Vector3>();

        for (int n = tileMap.cellBounds.xMin; n < tileMap.cellBounds.xMax; n++)
        {
            for (int p = tileMap.cellBounds.yMin; p < tileMap.cellBounds.yMax; p++)
            {
                Vector3Int localPlace = (new Vector3Int(n, p, (int)tileMap.transform.position.y));
                Vector3 place = tileMap.CellToWorld(localPlace);
                if (tileMap.HasTile(localPlace))
                {
                    //Tile at "place"
                    Debug.Log("dddx:" + n + " y:" + p + " tile:" + tileMap.GetTile(localPlace).name + " position: " + place);
                    Instantiate(rock, place, Quaternion.identity);
                    availablePlaces.Add(place);
                }
            }
        }
        HexController.addTileObstacles(availablePlaces);
    }
}