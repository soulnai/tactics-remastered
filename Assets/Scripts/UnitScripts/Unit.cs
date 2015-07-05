using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using EnumSpace;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    public Sprite Icon;
    public bool isFlying = false;

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
	public Tile CurrentTile;
	//Текущий путь
	public List<Tile> CurrentPath;
    //текущая позиция на карте
    public Vector2 GridPosition = Vector2.zero;
	//количество очков действия

    public BaseAttribute AP;
    public BaseAttribute APMax;

    public BaseAttribute HP;
    public BaseAttribute HPMax;

    public BaseAttribute MP;
    public BaseAttribute MPMax;

    public BaseAttribute Dexterity;
    public BaseAttribute Strenght;
    public BaseAttribute Magic;

    //текущее действие юнита
    private unitActions _currentAction;
    public unitActions CurrentAction { get { return _currentAction; } set { _currentAction = value; GM.Events.UnitActionChanged(this, _currentAction, value); } }
    
    //Шанс попасть по противнику
    public float ToHitChance;
    //Шанс увернуться от удара
    public float EvadeChance;
    //Шанс увернуться от удара
    public float CritChance;
    //Множитель критического удара
    public float CritMultiplier;
    //Минимальный урон текущего оружия
    public int MinCurrentWeaponAtk;
    //Максимальный урон текущего оружия
    public int MaxCurrentWeaponAtk;

    //Физическая защита (поглощение урона)
    public int PhysicalDef;


    public void Start()
    {
        //GM.Events.OnPlayerTurnStart += TurnInit;
        CurrentAction = unitActions.idle;
    }

    public void MoveTo(Tile destinationTile)
    {
        GM.Map.GeneratePath(CurrentTile, destinationTile);
        CurrentAction = unitActions.moving;
        Move();
    }

    public void Move()
    {
        if (CurrentPath.Count > 0)
        {
            if (AP.Value > 0 && MovementRange >= GM.Map.CalcPathCost(this))
            {
                GM.BattleData.UnitControlState = unitTurnStates.blockInteract;

                Vector3[] VectorPath = new Vector3[CurrentPath.Count];
                int i = 0;
                foreach (Tile t in CurrentPath)
                {
                    VectorPath[i] = t.transform.position;
                    i++;
                    t.hideHighlight();
                }
                float pathTime = CurrentPath.Count * 0.5f;
                transform.DOPath(VectorPath, pathTime).OnWaypointChange(OnWaypointChange).OnComplete(OnMoveComplete);

                ReduceAP();
            }
            else
            {
                Debug.Log("Недостаточно АР");
            }
        }
    }

    private void OnMoveComplete()
    {
        CurrentAction = unitActions.idle;
        GM.Events.UnitActionCompleted(this);
        CurrentTile = CurrentPath[CurrentPath.Count-1];
        CurrentPath.Clear();
        GM.BattleData.UnitControlState = unitTurnStates.canInteract;
    }

    void OnWaypointChange(int waypointIndex)
    {
        transform.LookAt(CurrentPath[waypointIndex].transform.position);
        CurrentTile = CurrentPath[waypointIndex];
    }

    public void ReduceAP()
    {
        if (AP.Value > 0)
        {
            AP.Value -= 1;
        }
        else if (AP.Value <= 0)
        {
        }
    }
}
