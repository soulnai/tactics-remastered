using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class MapUtils : MonoBehaviour {
	public int mapSize = 22;
	public Transform mapTransform;
	public static MapUtils instance;
	public List <List<Tile>> map = new List<List<Tile>>();
	private ScenarioController gm;

	void OnAwake(){
		instance = this;
		gm = ScenarioController.instance;
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void loadMapFromXml() {
		MapXmlContainer container = MapSaveLoad.Load("/Asets/Resources/map.xml");
		
		mapSize = container.size;
		
		//initially remove all children
		for(int i = 0; i < mapTransform.childCount; i++) {
			Destroy (mapTransform.GetChild(i).gameObject);
		}
		
		map = new List<List<Tile>>();
		for (int i = 0; i < mapSize; i++) {
			List <Tile> row = new List<Tile>();
			for (int j = 0; j < mapSize; j++) {
				float tileHeight = container.tiles.Where(x => x.locX == i && x.locY == j).First().height;
				Tile tile = ((GameObject)Instantiate(PrefabHolder.instance.BASE_TILE_PREFAB, new Vector3(i - Mathf.Floor(mapSize/2),tileHeight, -j + Mathf.Floor(mapSize/2)), Quaternion.Euler(new Vector3()))).GetComponent<Tile>();
				
				tile.transform.parent = mapTransform;
				tile.gridPosition = new Vector2(i, j);
				tile.setType((TileType)container.tiles.Where(x => x.locX == i && x.locY == j).First().id);
				tile.height = tileHeight;
				row.Add (tile);
			}
			map.Add(row);
		}
		gm.map = map;
		for (int i = 0; i < mapSize; i++) {
			for (int j = 0; j < mapSize; j++) {
				map[i][j].generateNeighbors();
			}
		}
	}
}
