using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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
	//AI
	public bool AIControlled = false;
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
	//количество очков жизни
	public int HP = 5;
    //текущее действие юнита
    public EnumSpace.unitActions CurrentAction;
    //иконка/портрет юнита
    public Image IconImage;

    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Move(Tile destinationTile)
    {
        GM.Map.GeneratePath(currentTile, destinationTile);
        Move(this);
        //TODO CheckAP(this);
        
        //_battleData.CurrentUnit.currentPath = null;
    }

    public void Move(Unit unit)
    {
        if (unit.AP > 0 && unit.MovementRange >= GM.Map.CalcPathCost(unit))
        {
            Vector3[] VectorPath = new Vector3[unit.currentPath.Count];
            Tile destTile = null;
            for (int i = 0; i < unit.currentPath.Count; i++)
            {
                VectorPath[i] = new Vector3(unit.currentPath[i].transform.position.x, unit.currentPath[i].transform.position.y, unit.currentPath[i].transform.position.z);
                destTile = unit.currentPath[i];
            }
            float pathTime = unit.currentPath.Count * 0.5f;
            unit.transform.DOPath(VectorPath, pathTime).OnWaypointChange(ChangeLookAt); ;
            unit.currentTile = destTile;
            foreach (Tile t in unit.currentPath)
            {
                t.hideHighlight();
            }
            ReduceAP();
        }
        else
        {
            Debug.Log("Недостаточно АР");
        }
    }

    void ChangeLookAt(int waypointIndex)
    {
        transform.LookAt(currentPath[waypointIndex].transform.position);
    }

    public void ReduceAP()
    {
        if (AP > 0)
        {
            AP -= 1;
        }
    }


}
