using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TilePath {
	public List<Tile> Tiles = new List<Tile>();
	
	public int Cost = 0;	
	
	public Tile lastTile;
	
	public TilePath() {}
	
	public TilePath(TilePath tp) {
		Tiles = tp.Tiles.ToList();
		Cost = tp.Cost;
		lastTile = tp.lastTile;
	}
	
	public void addTile(Tile t) {
        if (GM.BattleData.CurrentUnit.currentTile == t)
        {
            Cost += 0;
        }
        else
        {
            Cost += t.movementCost;
        }
		Tiles.Add(t);
		lastTile = t;
	}
}