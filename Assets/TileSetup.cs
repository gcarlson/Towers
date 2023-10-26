using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using TMPro;

public class TileSetup : MonoBehaviour
{
    public GameObject rock, theBase, spawn;
    public GameObject endScreen;
    public TextMeshProUGUI text;
    public GameManager manager;
    void Start()
    {
        Debug.Log("dddx starting");
        Tilemap tileMap = GetComponent<Tilemap>();

        //BoundsInt bounds = tilemap.cellBounds;
        //TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

       List<Vector3> obstacles = new List<Vector3>();

        for (int n = tileMap.cellBounds.xMin; n < tileMap.cellBounds.xMax; n++)
        {
            for (int p = tileMap.cellBounds.yMin; p < tileMap.cellBounds.yMax; p++)
            {
                Vector3Int localPlace = (new Vector3Int(n, p, (int)tileMap.transform.position.y));
                Vector3 place = tileMap.CellToWorld(localPlace);
                if (tileMap.HasTile(localPlace))
                {
                    if (tileMap.GetTile(localPlace).name == "Wall")
                    {
                        Debug.Log("dddx:" + n + " y:" + p + " tile:" + tileMap.GetTile(localPlace).name + " position: " + place);

                        Instantiate(rock, place, Quaternion.Euler(0, Random.Range(0, 6) * 60, 0));
                        obstacles.Add(place);
                    } else if (tileMap.GetTile(localPlace).name == "Base")
                    {
                        Debug.Log("dddx:" + n + " y:" + p + " tile:" + tileMap.GetTile(localPlace).name + " position: " + place);

                        var o = Instantiate(theBase, place, Quaternion.identity);
                        o.GetComponentInChildren<EnemyGoal>().endScreen = endScreen;
                        o.GetComponentInChildren<EnemyGoal>().text = text;
                        HexController.spawnOutpost(place);
                        //obstacles.Add(place);
                    } else if (tileMap.GetTile(localPlace).name == "Spawn")
                    {
                        var o = Instantiate(spawn, place, Quaternion.identity);
                        HexController.spawns.Add(HexController.getNearest(place));
                        HexController.computePaths();
                        GameManager.startingPos.Add(o.transform);
                    }
                }
            }
        }
        HexController.addTileObstacles(obstacles);
    }
}