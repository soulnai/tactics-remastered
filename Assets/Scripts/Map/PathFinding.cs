using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TilePathFinder : MonoBehaviour {


	public static List<Tile> FindPath(Tile originTile, Tile destinationTile, float maxHeightDiff) {
		List<Tile> closed = new List<Tile>();
		List<TilePath> open = new List<TilePath>();
        Tile[] occupied = GM.Map.BlockedTiles();
        if (occupied.Contains(destinationTile)) 
        {
            List<Tile> tmp = occupied.ToList();
            tmp.Remove(destinationTile);
            occupied = tmp.ToArray();
        }

		TilePath originPath = new TilePath();
		originPath.addTile(originTile);
		
		open.Add(originPath);
		
		while (open.Count > 0) {
			open = open.OrderBy(x => x.Cost).ToList();
			TilePath current = open[0];
			open.Remove(open[0]);
			
			if (closed.Contains(current.lastTile)) {
				continue;
			} 
			if (current.lastTile == destinationTile) {
				current.Tiles.Distinct();
				current.Tiles.Remove(originTile);
				return current.Tiles;
			}
			
			closed.Add(current.lastTile);
			
			foreach (Tile t in current.lastTile.neighbors) {
                if (t.impassible || occupied.Contains(t) || GM.BattleData.blockedTiles.Contains(t) || Mathf.Abs(current.lastTile.height - t.height) > maxHeightDiff) continue;
				TilePath newTilePath = new TilePath(current);
				newTilePath.addTile(t);
				open.Add(newTilePath);
			}
		}
		return closed;
	}

    public static List<Tile> FindPathForAttack(Tile originTile, Tile destinationTile)
    {
        List<Tile> closed = new List<Tile>();
        List<TilePath> open = new List<TilePath>();

        TilePath originPath = new TilePath();
        originPath.addTile(originTile);

        open.Add(originPath);

        while (open.Count > 0)
        {
            open = open.OrderBy(x => x.Cost).ToList();
            TilePath current = open[0];
            open.Remove(open[0]);

            if (closed.Contains(current.lastTile))
            {
                continue;
            }
            if (current.lastTile == destinationTile)
            {
                current.Tiles.Distinct();
                current.Tiles.Remove(originTile);
                return current.Tiles;
            }

            closed.Add(current.lastTile);

            foreach (Tile t in current.lastTile.neighbors)
            {
                TilePath newTilePath = new TilePath(current);
                newTilePath.addTileWithoutCost(t);
                open.Add(newTilePath);
            }
        }
        return closed;
    }

	public static List<Tile> FindArea(Tile originTile, int movementPoints, Tile[] occupied,float maxHeightDiff = 100f) {
			List<Tile> closed = new List<Tile>();
			List<TilePath> open = new List<TilePath>();
			
			TilePath originPath = new TilePath();
			originPath.addTile(originTile);
			
			open.Add(originPath);
			
			while (open.Count > 0) {
                open = open.OrderBy(x => x.Cost).ToList();
				TilePath current = open[0];
				open.Remove(open[0]);
				
				if (closed.Contains(current.lastTile)) {
					continue;
				} 
				if (current.Cost > movementPoints) {
					continue;
				}
				
				closed.Add(current.lastTile);
				
				foreach (Tile t in current.lastTile.neighbors) {	
					if (t.impassible || occupied.Contains(t)) continue;
					TilePath newTilePath = new TilePath(current);
					newTilePath.addTile(t);
					open.Add(newTilePath);
				}
			}
			closed.Remove(originTile);
			closed.Distinct();
			return closed;
		}

    public static List<Tile> FindAreaForAttack(Tile originTile, int movementPoints, Tile[] occupied, float maxHeightDiff = 100f)
    {
        List<Tile> closed = new List<Tile>();
        List<TilePath> open = new List<TilePath>();

        TilePath originPath = new TilePath();
        originPath.addTile(originTile);

        open.Add(originPath);

        while (open.Count > 0)
        {
            open = open.OrderBy(x => x.Cost).ToList();
            TilePath current = open[0];
            open.Remove(open[0]);

            if (closed.Contains(current.lastTile))
            {
                continue;
            }
            if (current.Cost > movementPoints)
            {
                continue;
            }

            closed.Add(current.lastTile);

            foreach (Tile t in current.lastTile.neighbors)
            {
                if (t.impassible || occupied.Contains(t)) continue;
                TilePath newTilePath = new TilePath(current);
                newTilePath.addTileWithoutCost(t);
                open.Add(newTilePath);
            }
        }
        closed.Remove(originTile);
        closed.Distinct();
        return closed;
    }
}