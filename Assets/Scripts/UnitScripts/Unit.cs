using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EnumSpace;

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
    //скорость перемещения
    public int MovementSpeed = 0;
    //максимальная разница высот для преодоления
    public int MaxHeight = 100;
    public unitStates State;
	//Тайл на котором находится юнит
	public Tile currentTile;
	//Текущий путь
	public List<Tile> currentPath;
	//Скорость перемещения
	public int speed = 5;
    //текущая позиция на карте
    public Vector2 gridPosition = Vector2.zero;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
