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

    public void MoveTo(Tile destinationTile)
    {
        GM.Map.GeneratePath(currentTile, destinationTile);
        Move();
        //TODO CheckAP(this);
        
        //_battleData.CurrentUnit.currentPath = null;
    }

    public void Move()
    {
        if (AP > 0 && MovementRange >= GM.Map.CalcPathCost(this))
        {
            Vector3[] VectorPath = new Vector3[currentPath.Count];
            Tile destTile = null;
            int i = 0;
            foreach (Tile t in currentPath)
            {
                VectorPath[i] = t.transform.position;
                i++;
                t.hideHighlight();
            }
            float pathTime = currentPath.Count * 0.5f;
            transform.DOPath(VectorPath, pathTime).OnWaypointChange(ChangeLookAt);
            currentTile = destTile;
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
        currentTile = currentPath[waypointIndex];
    }

    public void ReduceAP()
    {
        if (AP > 0)
        {
            AP -= 1;
        }
    }


}
