using UnityEngine;
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
	private ScenarioController gm;
    private GlobalPrefabHolder _prefabHolder;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update () {
	
	}

	public void loadMapFromXml() {
        _prefabHolder = GlobalPrefabHolder.Instance;
		gm = ScenarioController.Instance;
        MapXmlContainer container = MapSaveLoad.Load("Resources/Level1/map.xml");
		
		gm.mapSize = container.size;
		
		//initially remove all children
		for(int i = 0; i < mapTransform.childCount; i++) {
			Destroy (mapTransform.GetChild(i).gameObject);
		}
		
		map = new List<List<Tile>>();
		for (int i = 0; i < gm.mapSize; i++) {
			List <Tile> row = new List<Tile>();
			for (int j = 0; j < gm.mapSize; j++) {
				float tileHeight = container.tiles.Where(x => x.locX == i && x.locY == j).First().height;
                Tile tile = ((GameObject)Instantiate(_prefabHolder.BASE_TILE_PREFAB, new Vector3(i - Mathf.Floor(gm.mapSize / 2), tileHeight, -j + Mathf.Floor(gm.mapSize / 2)), Quaternion.Euler(new Vector3()))).GetComponent<Tile>();
				
				tile.transform.parent = mapTransform;
				tile.gridPosition = new Vector2(i, j);
				tile.setType((EnumSpace.TileType)container.tiles.Where(x => x.locX == i && x.locY == j).First().id);
				tile.height = tileHeight;
				row.Add (tile);
			}
			map.Add(row);
		}
		gm.map = map;
		for (int i = 0; i < gm.mapSize; i++) {
			for (int j = 0; j < gm.mapSize; j++) {
				map[i][j].generateNeighbors();
			}
		}
	}

	public void loadStuffFromXml() {
		gm = ScenarioController.Instance;
		MapStuffXmlContainer container = MapSaveLoad.LoadStuff("Resources/Level1/stuff.xml");
		
		int treesCount = container.trees.Count;
		for (int i = 0; i < treesCount; i++) {
				gm = ScenarioController.Instance;
				string stuffType = container.trees.ElementAt(i).prefabName;
				Tile tileTospawn = gm.map[container.trees.ElementAt(i).locX][container.trees.ElementAt(i).locY];
				Instantiate(_prefabHolder.Prefabs[stuffType], tileTospawn.transform.position, Quaternion.Euler(-90, 90, 0));
				tileTospawn.impassible = true;
		}

		int cratesCount = container.crates.Count;
		
		for (int i = 0; i < cratesCount; i++) {
			gm = ScenarioController.Instance;
			string stuffType = container.crates.ElementAt(i).prefabName;
			Tile tileTospawn = gm.map[container.crates.ElementAt(i).locX][container.crates.ElementAt(i).locY];
			Instantiate(_prefabHolder.Prefabs[stuffType], tileTospawn.transform.position, Quaternion.Euler(-90, 90, 0));
			tileTospawn.impassible = true;
		}

		int spawnTilesCount = container.spawnTiles.Count;
		Debug.Log (spawnTilesCount);
		for (int i = 0; i < spawnTilesCount; i++) {
			gm = ScenarioController.Instance;
			gm.spawnArea.Add(gm.map[container.spawnTiles.ElementAt(i).locX][container.spawnTiles.ElementAt(i).locY]);
		}

        //int playersCount = container.players.Count;
        //Debug.Log(playersCount);

        Debug.Log(container.players[1].units[0].prefabName);
		
	}

    public void loadMapDetailsFromXml()
    {
        gm = ScenarioController.Instance;
        MissionDetailsXmlContainer container = MapSaveLoad.LoadMapDetails("Resources/Level1/mission.xml");

        Debug.Log("Mission name = "+container.missionName.name);
        Debug.Log("Mission description = "+container.missionDescription.description);
        Debug.Log("Mission target = "+container.missionTarget.target);
        Debug.Log("Mission map = " +container.missionMap.map);
    }
}
