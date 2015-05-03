using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    //имя игрока
    public string PlayerName = "";
    //управляется пользователем
    public bool UserControlled = false;
    //все доступные на текущий момент юнити данного игрока
    public List<Unit> AvailableUnits = new List<Unit>();
    //юнити в пати данного игрока
    public List<Unit> PartyUnits = new List<Unit>();
	public List<Unit> SpawnedPartyUnits = new List<Unit>();
      
	// Use this for initialization
	void Awake () {
	    AvailableUnits = new List<Unit>();
        PartyUnits = new List<Unit>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
