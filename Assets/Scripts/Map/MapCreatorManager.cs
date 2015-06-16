using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EnumSpace;

public class MapCreatorManager : MonoBehaviour {
	public static MapCreatorManager instance;

	public int mapSize;
	public List <List<Tile>> map = new List<List<Tile>>();
    //public List<MiscObject> crates = new List<MiscObject>();
    //public List<MiscObject> trees = new List<MiscObject>();
    public List<MiscObject> spawnTiles = new List<MiscObject>();

    public List<MiscObject> miscObjects = new List<MiscObject>();

    public List<MiscObject> AIUnits = new List<MiscObject>();

	public TileType palletSelection = TileType.Normal;
	public EnumSpace.editorStates editorState;
	public bool up;
	Transform mapTransform;
    public Tile tileSelected;
    public string miscObjectToSpawnName;



	// Use this for initialization
	void Awake () {
		instance = this;

		mapTransform = transform.FindChild("Map");

		generateBlankMap(mapSize);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void generateBlankMap(int mSize) {


		//initially remove all children
		for(int i = 0; i < mapTransform.childCount; i++) {
			Destroy (mapTransform.GetChild(i).gameObject);
		}

		map = new List<List<Tile>>();
		for (int i = 0; i < mapSize; i++) {
			List <Tile> row = new List<Tile>();
			for (int j = 0; j < mapSize; j++) {
				Tile tile = ((GameObject)Instantiate(GM.Prefabs.BASE_TILE_PREFAB, new Vector3(i - Mathf.Floor(mapSize/2),0, -j + Mathf.Floor(mapSize/2)), Quaternion.Euler(new Vector3()))).GetComponent<Tile>();
				tile.transform.parent = mapTransform;
				tile.gridPosition = new Vector2(i, j);
				tile.setType(TileType.Normal);
				row.Add (tile);
			}
			map.Add(row);
		}
	}

	void loadMapFromXml() {
        MapXmlContainer container = MapSaveLoad.Load("Resources/Level1/map.xml");

        MissionDetailsXmlContainer missionContainer = MapSaveLoad.LoadMapDetails("Resources/Level1/mission.xml");

		mapSize = container.size;

        for (int i = 0; i < mapTransform.childCount; i++)
        {
            Destroy(mapTransform.GetChild(i).gameObject);
        }

        map = new List<List<Tile>>();
        for (int i = 0; i < mapSize; i++)
        {
            List<Tile> row = new List<Tile>();
            for (int j = 0; j < mapSize; j++)
            {
                float tileHeight = container.tiles.Where(x => x.locX == i && x.locY == j).First().height;
                Tile tile = ((GameObject)Instantiate(GM.Prefabs.BASE_TILE_PREFAB, new Vector3(i - Mathf.Floor(GM.Scenario.mapSize / 2), tileHeight, -j + Mathf.Floor(GM.Scenario.mapSize / 2)), Quaternion.Euler(new Vector3()))).GetComponent<Tile>();

                tile.transform.parent = mapTransform;
                tile.gridPosition = new Vector2(i, j);
                tile.setType((EnumSpace.TileType)container.tiles.Where(x => x.locX == i && x.locY == j).First().id);
                tile.height = tileHeight;
                row.Add(tile);
                GM.BattleData.allTiles.Add(tile);
            }
            map.Add(row);
        }
        GM.Scenario.map = map;
        for (int i = 0; i < mapSize; i++)
        {
            for (int j = 0; j < mapSize; j++)
            {
                //map[i][j].generateNeighbors();
            }
        }

        int objectsCount = container.objects.Count;
        for (int i = 0; i < objectsCount; i++)
        {
            string stuffType = container.objects.ElementAt(i).prefabName;
            Tile tileTospawn = GM.Scenario.map[container.objects.ElementAt(i).locX][container.objects.ElementAt(i).locY];
            MiscObject obj = ((GameObject)Instantiate(GM.Prefabs.Prefabs[stuffType], tileTospawn.transform.position + new Vector3(0f, .5f, 0f), Quaternion.Euler(-90, 90, 0))).GetComponent<MiscObject>();
            obj.transform.parent = tileTospawn.transform;
            tileTospawn.impassible = true;
            tileTospawn.occupied = true;
            GM.BattleData.blockedTiles.Add(tileTospawn);
            miscObjects.Add(obj);
        }

        /*int cratesCount = container.crates.Count;

        for (int i = 0; i < cratesCount; i++)
        {
            string stuffType = container.crates.ElementAt(i).prefabName;
            Tile tileTospawn = GM.Scenario.map[container.crates.ElementAt(i).locX][container.crates.ElementAt(i).locY];
            Instantiate(GM.Prefabs.Prefabs[stuffType], tileTospawn.transform.position, Quaternion.Euler(-90, 90, 0));
            tileTospawn.impassible = true;
            GM.BattleData.blockedTiles.Add(tileTospawn);
        }*/

        spawnTiles.Clear();

        for (int i = 0; i < missionContainer.spawnTiles.Count; i++ )
        {
            GameObject sp = new GameObject();
            sp.AddComponent<MiscObject>();
            MiscObject spawn = sp.GetComponent<MiscObject>();
            spawn.locX = missionContainer.spawnTiles[i].locX;
            spawn.locY = missionContainer.spawnTiles[i].locY;
            spawnTiles.Add(spawn);
        }
	}

	void saveMapToXml() {
		//MapSaveLoad.Save(MapSaveLoad.CreateMapContainer(map, crates, trees), "map-test.xml");
        MapSaveLoad.Save(MapSaveLoad.CreateMapContainer(map, miscObjects), "map-test.xml");
        MapSaveLoad.SaveMission(MapSaveLoad.CreateMissionContainer(spawnTiles, AIUnits), "map-test-mission.xml");

	}

	void OnGUI() {
		//pallet
		Rect rect = new Rect(10, Screen.height - 80, 100, 60);

		if (GUI.Button(rect, "Normal")) {
			editorState = editorStates.setType;
			palletSelection = TileType.Normal;
		}

		rect = new Rect(10 + (100 + 10) * 1, Screen.height - 80, 100, 60);
		
		if (GUI.Button(rect, "Difficult")) {
			editorState = editorStates.setType;
			palletSelection = TileType.Difficult;
		}

		rect = new Rect(10 + (100 + 10) * 2, Screen.height - 80, 100, 60);
		
		if (GUI.Button(rect, "VeryDifficult")) {
			editorState = editorStates.setType;
			palletSelection = TileType.VeryDifficult;
		}

		rect = new Rect(10 + (100 + 10) * 3, Screen.height - 80, 100, 60);
		
		if (GUI.Button(rect, "Impassible")) {
			editorState = editorStates.setType;
			palletSelection = TileType.Impassible;
		}

        rect = new Rect(10 + (100 + 10) * 4, Screen.height - 80, 100, 60);

        if (GUI.Button(rect, "SpawnZone"))
        {
            GameObject sp = new GameObject();
            sp.AddComponent<MiscObject>();
            MiscObject spawn = sp.GetComponent<MiscObject>();
            editorState = editorStates.setSpawnZone;
            spawn.locX = (int)tileSelected.gridPosition.x;
            spawn.locY = (int)tileSelected.gridPosition.y;
            spawnTiles.Add(spawn);
        }

        /*if (GUI.Button(rect, "Crate"))
        {
            MiscObject crate = ((GameObject)Instantiate(GM.Prefabs.MiscPrefabsHolder[0], tileSelected.transform.position + new Vector3(0f, .5f, 0f), Quaternion.identity)).GetComponent<MiscObject>();
            crate.transform.parent = tileSelected.transform;
            crate.locX = (int)tileSelected.gridPosition.x;
            crate.locY = (int)tileSelected.gridPosition.y;
            crate.prefabName = GM.Prefabs.MiscPrefabsHolder[0].name;
            crates.Add(crate);
        }

        rect = new Rect(10 + (100 + 10) * 5, Screen.height - 80, 100, 60);

        if (GUI.Button(rect, "Tree"))
        {
            MiscObject tree = ((GameObject)Instantiate(GM.Prefabs.MiscPrefabsHolder[1], tileSelected.transform.position + new Vector3(0f, .5f, 0f), Quaternion.identity)).GetComponent<MiscObject>();
            tree.transform.parent = tileSelected.transform;
            tree.locX = (int)tileSelected.gridPosition.x;
            tree.locY = (int)tileSelected.gridPosition.y;
            tree.prefabName = GM.Prefabs.MiscPrefabsHolder[1].name;
            trees.Add(tree);
        }*/

        rect = new Rect(10 + (100 + 10) * 6, Screen.height - 80, 100, 60);

        if (GUI.Button(rect, "DeleteObject"))
        {
            editorState = editorStates.deleteObject;
        }
		//

		//IO 
		rect = new Rect(Screen.width - (10 + (100 + 10) * 3), Screen.height - 80, 100, 60);
		
		if (GUI.Button(rect, "Clear Map")) {
            for (int i = 0; i < mapTransform.childCount; i++)
            {
                Destroy(mapTransform.GetChild(i).gameObject);
            }
			generateBlankMap(mapSize);
            //trees.Clear();
            //crates.Clear();
            miscObjects.Clear();
            spawnTiles.Clear();
            miscObjectToSpawnName = "";
		}

		rect = new Rect(Screen.width - (10 + (100 + 10) * 2), Screen.height - 80, 100, 60);
		
		if (GUI.Button(rect, "Load Map")) {
			loadMapFromXml();
		}

		rect = new Rect(Screen.width - (10 + (100 + 10) * 1), Screen.height - 80, 100, 60);
		
		if (GUI.Button(rect, "Save Map")) {
			saveMapToXml();
		}
		//

	}

	public void setUP(bool u)
	{
		editorState = editorStates.setHeight;
		up = u;
	}

    public void handleButton(string prefabName)
    {
        Debug.Log("Button '" + prefabName + "' pressed!");
        editorState = editorStates.spawnObject;
        miscObjectToSpawnName = prefabName;
    }

    public void spawnmiscobject(string objectname) 
    {
        if (tileSelected.occupied == false)
        {
            MiscObject mapObject = ((GameObject)Instantiate(GM.Prefabs.Prefabs[objectname], tileSelected.transform.position + new Vector3(0f, 0f, 0f), Quaternion.Euler(-90, 90, 0))).GetComponent<MiscObject>();
            mapObject.transform.parent = tileSelected.transform;
            mapObject.locX = (int)tileSelected.gridPosition.x;
            mapObject.locY = (int)tileSelected.gridPosition.y;
            mapObject.prefabName = objectname;
            if ((mapObject.GetComponent("Unit") as Unit) != null)
            {
                AIUnits.Add(mapObject);
            }
            else
            {
                miscObjects.Add(mapObject);
            }
            tileSelected.occupied = true;
        }
    }
}
