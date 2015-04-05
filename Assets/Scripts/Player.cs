using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    //имя игрока
    public string PlayerName;
    //управляется пользователем
    public bool UserControlled = false;
    //все доступные на текущий момент юнити данного игрока
    public List<Unit> AvailableUnits;
    //юнити в пати данного игрока
    public List<Unit> PartyUnits;
      
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
