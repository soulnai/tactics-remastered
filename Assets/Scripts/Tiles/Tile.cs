using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EnumSpace;

public class Tile : MonoBehaviour {

	GameObject PREFAB;

	public GameObject visual;
	public GameObject highlight;

	public TileType type = TileType.Normal;

	public Vector2 gridPosition = Vector2.zero;
	
	public int movementCost = 1;
	public float height = 0;
	public bool impassible = false;
	
	public List<Tile> neighbors = new List<Tile>();

	public Tile upNeighbour;
	public Tile rightNeighbour;
	public Tile downNeighbour;
	public Tile leftNeighbour;

	private ScenarioController gm;
	// Use this for initialization
	void Awake(){
//		highlight.SetActive(false);

	}

	void Start () {

	}
	
	public void generateNeighbors() {		
		neighbors = new List<Tile>();
		gm = ScenarioController.instance;
		//up
		if (gridPosition.y > 0) {
			Vector2 n = new Vector2(gridPosition.x, gridPosition.y - 1);
			neighbors.Add(gm.map[(int)Mathf.Round(n.x)][(int)Mathf.Round(n.y)]);
			upNeighbour = gm.map[(int)Mathf.Round(n.x)][(int)Mathf.Round(n.y)];
		}
		//down
		if (gridPosition.y < gm.mapSize - 1) {
			Vector2 n = new Vector2(gridPosition.x, gridPosition.y + 1);
			neighbors.Add(gm.map[(int)Mathf.Round(n.x)][(int)Mathf.Round(n.y)]);
			downNeighbour = gm.map[(int)Mathf.Round(n.x)][(int)Mathf.Round(n.y)];
		}		
		
		//left
		if (gridPosition.x > 0) {
			Vector2 n = new Vector2(gridPosition.x - 1, gridPosition.y);
			neighbors.Add(gm.map[(int)Mathf.Round(n.x)][(int)Mathf.Round(n.y)]);
			leftNeighbour = gm.map[(int)Mathf.Round(n.x)][(int)Mathf.Round(n.y)];
		}
		//right
		if (gridPosition.x < gm.mapSize - 1) {
			Vector2 n = new Vector2(gridPosition.x + 1, gridPosition.y);
			neighbors.Add(gm.map[(int)Mathf.Round(n.x)][(int)Mathf.Round(n.y)]);
			rightNeighbour = gm.map[(int)Mathf.Round(n.x)][(int)Mathf.Round(n.y)];
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseEnter() {
        showHighlight(Color.red);
	}
	
	void OnMouseExit() {
        hideHighlight();
	}
	
	//TODO move to the Controller
	void OnMouseDown(){
		Tile tile;
		if (Input.GetMouseButtonDown(0)) 
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if(Physics.Raycast(ray,out hit))
			{
				Debug.Log(hit.collider.gameObject.name);
				tile = hit.collider.gameObject.GetComponent<Tile>();
				if (tile.impassible !=true){
				gm.GeneratePath(gm.unitsAll[0].currentTile, tile );
				}
			}
		}
	}

	public void changeHeight(bool up)
	{
		Vector3 tempHeight = new Vector3(0,0.2f,0 );
		if(up == true)
		{
			transform.position += tempHeight;
		}
		else
		{
			transform.position -= tempHeight;
		}
		height = transform.position.y;
	}

	public void setType(TileType t) {
		type = t;
		//definitions of TileType properties
		switch(t) {
			case TileType.Normal:
				movementCost = 1;
				impassible = false;
				PREFAB = PrefabHolder.instance.TILE_NORMAL_PREFAB;
				break;
			
			case TileType.Difficult:
				movementCost = 2;
				impassible = false;
				PREFAB = PrefabHolder.instance.TILE_DIFFICULT_PREFAB;
				break;
				
			case TileType.VeryDifficult:
				movementCost = 3;
				impassible = false;
				PREFAB = PrefabHolder.instance.TILE_VERY_DIFFICULT_PREFAB;
				break;
				
			case TileType.Impassible:
				movementCost = 999;
				impassible = true;
				PREFAB = PrefabHolder.instance.TILE_IMPASSIBLE_PREFAB;
				break;

			default:
				movementCost = 1;
				impassible = false;
				PREFAB = PrefabHolder.instance.TILE_NORMAL_PREFAB;
				break;
		}

		generateVisuals();
	}

	public void generateVisuals() {
		GameObject container = transform.FindChild("Visuals").gameObject;
		//initially remove all children
		for(int i = 0; i < container.transform.childCount; i++) {
			Destroy (container.transform.GetChild(i).gameObject);
		}

		GameObject newVisual = (GameObject)Instantiate(PREFAB, transform.position, Quaternion.identity);
		newVisual.transform.parent = container.transform;
		visual = newVisual;
	}

	public void showHighlight(Color color){
		highlight.SetActive(true);
		highlight.GetComponent<Renderer>().material.color = color;
	}

	public void hideHighlight(){
		highlight.SetActive(false);
	}
}
