using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EnumSpace;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    //имя
    public string UnitName;
    //класс
    public unitClass UnitClass;
    //владелец
    public Player OwnerPlayer = null;
    //уровень юнита
    public int Lvl = 0;
    //набранный опыт
    public float Expirience = 0;
    //список умений
    public List<Ability> Abilities;
    //дальность перемещения тайлов за 1 АР
    public int MovementRange = 5;
    //максимальная разница высот для преодоления
    public int MaxHeight = 100;
    public unitStates State;
	//Тайл на котором находится юнит
	public Tile currentTile;
	//Текущий путь
	public List<Tile> currentPath;
    //текущая позиция на карте
    public Vector2 gridPosition = Vector2.zero;
	//количество очков действия
	public int AP = 2;
    //текущее действие юнита
    public EnumSpace.unitActions CurrentAction;
    //иконка/портрет юнита
    public Image IconImage;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		Unit unit;
		if (Input.GetMouseButtonDown(0)) 
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if(Physics.Raycast(ray,out hit))
			{
				Debug.Log(hit.collider.gameObject.name);
				unit = hit.collider.gameObject.GetComponent<Unit>();
					InputController.Instance.OnUnitClicked(this);
			}
		}
	}
}
