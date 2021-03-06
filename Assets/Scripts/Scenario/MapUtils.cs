﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class MapUtils : Singleton<MapUtils>
{
    protected MapUtils() { } // guarantee this will be always a singleton only - can't use the constructor!


	public int mapSize;
	public Transform mapTransform;
	public List <List<Tile>> map = new List<List<Tile>>();
    public List<MiscObject> miscObjects = new List<MiscObject>();

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update () {
	
	}

    public void loadMapFromXml(string mapFile)
    {
        mapTransform = GameObject.Find("Map").transform;
        MapXmlContainer container = MapSaveLoad.Load(mapFile);

        GM.Scenario.mapSize = container.size;
		
		//initially remove all children
		for(int i = 0; i < mapTransform.childCount; i++) {
			Destroy (mapTransform.GetChild(i).gameObject);
		}
		
		map = new List<List<Tile>>();
		for (int i = 0; i < GM.Scenario.mapSize; i++) {
			List <Tile> row = new List<Tile>();
			for (int j = 0; j < GM.Scenario.mapSize; j++) {
				float tileHeight = container.tiles.Where(x => x.locX == i && x.locY == j).First().height;
                Tile tile = ((GameObject)Instantiate(GM.Prefabs.BASE_TILE_PREFAB, new Vector3(i - Mathf.Floor(GM.Scenario.mapSize / 2), tileHeight, -j + Mathf.Floor(GM.Scenario.mapSize / 2)), Quaternion.Euler(new Vector3()))).GetComponent<Tile>();
				
				tile.transform.parent = mapTransform;
				tile.gridPosition = new Vector2(i, j);
				tile.setType((EnumSpace.TileType)container.tiles.Where(x => x.locX == i && x.locY == j).First().id);
				tile.height = tileHeight;
				row.Add (tile);
				GM.BattleData.allTiles.Add(tile);
			}
			map.Add(row);
		}
		GM.Scenario.map = map;
        for (int i = 0; i < GM.Scenario.mapSize; i++)
        {
            for (int j = 0; j < GM.Scenario.mapSize; j++)
            {
				map[i][j].generateNeighbors();
			}
		}

        int objectsCount = container.objects.Count;
        for (int i = 0; i < objectsCount; i++)
        {
            string stuffType = container.objects.ElementAt(i).prefabName;
            Tile tileTospawn = GM.Scenario.map[container.objects.ElementAt(i).locX][container.objects.ElementAt(i).locY];
            MiscObject obj = ((GameObject)Instantiate(GM.Prefabs.Prefabs[stuffType], tileTospawn.transform.position + new Vector3(0f, 0f, 0f), Quaternion.Euler(-90, 90, 0))).GetComponent<MiscObject>();
            obj.transform.parent = tileTospawn.transform;
            tileTospawn.impassible = true;
            tileTospawn.occupied = true;
            GM.BattleData.blockedTiles.Add(tileTospawn);
            miscObjects.Add(obj);
        }
	}

    public void loadMapDetailsFromXml(string mapDetailesfile)
    {
		MissionDetailsXmlContainer container = MapSaveLoad.LoadMapDetails(mapDetailesfile);
		
		Debug.Log("Mission name = "+container.missionName.name);
        Debug.Log("Mission description = "+container.missionDescription.description);
        Debug.Log("Mission target = "+container.missionTarget.target);
        Debug.Log("Mission Map = " +container.missionMap.map);

		int spawnTilesCount = container.spawnTiles.Count;
		Debug.Log (spawnTilesCount);

        GM.Scenario.spawnArea.Clear();
		for (int i = 0; i < spawnTilesCount; i++) {
			GM.Scenario.spawnArea.Add(GM.Scenario.map[container.spawnTiles.ElementAt(i).locX][container.spawnTiles.ElementAt(i).locY]);
		}
    }

    public void GeneratePath(Tile from, Tile to)
    {
        List<Tile> path = TilePathFinder.FindPath(from, to, 0.5f);
        foreach (Tile tile in path)
        {
            tile.showHighlight(Color.red);
        }
        GM.BattleData.CurrentUnit.CurrentPath = path;
    }

    public int GetDistance(Tile a,Tile b)
    {
        int Distance = TilePathFinder.FindPathForAttack(a, b).Count();
        return Distance;
    }

    public Unit FindNearestEnemy(Unit unit, List<Unit> ListOfUnits)
    {
        Debug.Log(unit.name);
        foreach (Unit u in ListOfUnits)
        {
            Debug.Log(u.CurrentTile.gridPosition);
        }

        Unit opponent = new Unit();
        if (ListOfUnits.Count > 0)
        {
            var opponentsSortedByHp = (ListOfUnits.OrderBy(x => x.HP.Value));
            var opponentsSortedByPath = opponentsSortedByHp.OrderBy(x => x != null ? TilePathFinder.FindPath(unit.CurrentTile, x.CurrentTile, unit.MaxHeight).Count() : 1000);
            foreach (Unit opp in opponentsSortedByPath) 
            {
                if (TilePathFinder.FindPath(unit.CurrentTile, opp.CurrentTile, unit.MaxHeight).Count()<50)
                {
                    Debug.Log("Pathfinding count raw" + TilePathFinder.FindPath(unit.CurrentTile, opp.CurrentTile, unit.MaxHeight).Count());
                    opponent = opp;
                    break;
                }
            }
            Debug.Log("TARGET -> "+ListOfUnits.OrderBy(x => x != null ? -x.HP.Value : 1000).ThenBy(x => x != null ? TilePathFinder.FindPath(unit.CurrentTile, x.CurrentTile, unit.MaxHeight).Count() : 1000).First());
        }
        return opponent;
    }

    public Tile[] BlockedTiles()
    {
        List<Tile> tempBlocked = new List<Tile>();
        foreach (Player p in GM.BattleData.Players)
        {
            foreach (Unit u in p.SpawnedPartyUnits)
            {
                tempBlocked.Add(u.CurrentTile);
                //Debug.Log(u.currentTile.gridPosition);
            }
        }
        return tempBlocked.ToArray();
    }

    public List<Unit> getAllUnitsinArea(Tile center, int radius)
    {
        List<Tile> availableTiles = new List<Tile>();
        List<Unit> opponentsInRange = new List<Unit>();
        //find all available tiles in area
        availableTiles = TilePathFinder.FindArea(center, radius, GM.BattleData.blockedTiles.ToArray(), 100f);

        //find all units in area
        foreach (Unit u in GM.BattleData.Players[0].SpawnedPartyUnits)
        {
            if (u != GM.BattleData.CurrentUnit && u.AIControlled == false)
            {
                if (availableTiles.Contains(u.CurrentTile))
                {
                    opponentsInRange.Add(u);
                }
            }
        }
        return opponentsInRange;
    }

    public List<Unit> getAllUnitsinAreaForAttack(Tile center, int radius)
    {
        List<Tile> availableTiles = new List<Tile>();
        List<Unit> opponentsInRange = new List<Unit>();
        //find all available tiles in area
        availableTiles = TilePathFinder.FindAreaForAttack(center, radius, GM.BattleData.blockedTiles.ToArray(), 0.6f);

        //find all units in area
        foreach (Unit u in GM.BattleData.Players[0].SpawnedPartyUnits)
        {
            if (u != GM.BattleData.CurrentUnit && u.AIControlled == false)
            {
                if (availableTiles.Contains(u.CurrentTile))
                {
                    opponentsInRange.Add(u);
                }
            }
        }
        return opponentsInRange;
    }

    public int CalcPathCost(Unit unit)
    {
        int PathCost = 0;
        for (int i = 0; i < unit.CurrentPath.Count; i++)
        {
            PathCost += unit.CurrentPath[i].movementCost;
        }
        return PathCost;
    }

    public List<Tile> getAllTilesinArea(Tile center, int radius)
    {
        return TilePathFinder.FindArea(center, radius, GM.BattleData.blockedTiles.ToArray(), 100f);
    }

    public List<Tile> GetAllTiles()
    {
        List<Tile> AllTiles = new List<Tile>();
        foreach (Tile t in GM.BattleData.allTiles) 
        {
            AllTiles.Add(t);
        }
        return AllTiles;
    }
}
