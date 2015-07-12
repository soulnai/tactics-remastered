using System;
using UnityEngine;
using System.Collections;
using EnumSpace;
using UnityEngine.Events;

/*-----------------------------------------------------

    Обрабатывает все управление, входящие клики, нажатия и пр.

-----------------------------------------------------*/
public class InputController : Singleton<InputController>
{
    protected InputController() { } // guarantee this will be always a singleton only - can't use the constructor!

    public Action<Tile> OnTileClick;
	public Action<Unit> OnUnitClick;
    public Action<Ability> OnAbilityClick;
    public Action<Unit> OnUnitDropToSlot;
    public Action OnBattleLoad;


    // Use this for initialization
    void Awake()
    {

    }

    public void AbilityClicked(Ability a)
    {
        if (OnAbilityClick != null)
            OnAbilityClick(a);
    }

    public void OnTileClicked(Tile t)
    {
		if (Application.loadedLevelName == "MapCreatorScene") {
            Debug.Log(t.gridPosition.x + "tile clicked");
            MapCreatorManager.instance.tileSelected = t;
            if (MapCreatorManager.instance.editorState == editorStates.setType)
                t.setType(MapCreatorManager.instance.palletSelection);
            else if (MapCreatorManager.instance.editorState == editorStates.setHeight)
                t.changeHeight(MapCreatorManager.instance.up);
            else if (MapCreatorManager.instance.editorState == editorStates.setSpawnZone)
            {
                MapCreatorManager.instance.tileSelected = t;
                GameObject sp = new GameObject();
                sp.AddComponent<MiscObject>();
                MiscObject spawn = sp.GetComponent<MiscObject>();
                spawn.locX = (int)MapCreatorManager.instance.tileSelected.gridPosition.x;
                spawn.locY = (int)MapCreatorManager.instance.tileSelected.gridPosition.y;
                MapCreatorManager.instance.spawnTiles.Add(spawn);
            }
            else if (MapCreatorManager.instance.editorState == editorStates.spawnObject)
            {
                MapCreatorManager.instance.tileSelected = t;
                MapCreatorManager.instance.spawnmiscobject(MapCreatorManager.instance.miscObjectToSpawnName);
            }
            else if (MapCreatorManager.instance.editorState == editorStates.deleteObject)
            {
                MapCreatorManager.instance.tileSelected = t;
                MiscObject obj = t.GetComponentInChildren<MiscObject>();
                if ((t.GetComponentInChildren<Unit>()) != null)
                {
                    MapCreatorManager.instance.AIUnits.Remove(obj);
                    GameObject destr = t.transform.Find(obj.name).gameObject;
                    Destroy(destr);
                }
                else
                {
                    MapCreatorManager.instance.miscObjects.Remove(obj);
                    GameObject destr = t.transform.Find(obj.name).gameObject;
                    Destroy(destr);
                }
                t.occupied = false;
                t.impassible = false;
            }
            
		} else
        if (OnTileClick != null)
            OnTileClick(t);
    }

	public void OnUnitClicked(Unit u)
	{
		if (OnUnitClick != null)
			OnUnitClick(u);
	}

    public void OnUnitDroppedToSlot(Unit u)
    {
        if (OnUnitDropToSlot != null)
            OnUnitDropToSlot(u);
    }

    public void OnBattleLoadPressed()
    {
        if (OnBattleLoad != null)
        {
            OnBattleLoad();
        }
    }
}
