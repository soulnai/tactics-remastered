using UnityEngine;
using System.Collections;

namespace EnumSpace {

    public enum ModType
    {
        Sum = 0,
        Percent = 1,
        Change = 2
    }

    public enum BuffType
    {
        Buff = 0,
        Permanent = 1
    }

    public enum ChangeType
    {
        Once = 0,
        EachTurn = 1
    }

    public enum SelectionType
    {
        Single = 0,
        AllInRadius = 1,
        All = 2,
        Self = 3
    }
    [HideInInspector]
    public enum TargetOwner
    {
        Ally = 0,
        Enemy = 1,
        Any = 2
    }
    public enum TargetType
    {
        Unit = 0,
        Tile = 1
    }

    public enum UnitListPanelType
    {
        AvailableUnitsPanel = 0,
        PartyUnitsPanel = 1
    }

    public enum ScenesEnum
    {
        mainMenu = 0,
        Tavern = 1,
        BattleScene = 2,
        MapCreatorScene = 3
    };

	public enum unitStates{
        none = 0,
		normal = 1,
		stunned = 2,
		poisoned = 3,
		burning = 4,
		freezed = 5,
		dead = 6
	}

	public enum unitClass{
        none = 0,
		squire = 1,
		warrior = 2,
		palladin = 3,
		knigth = 4,
		mage = 5,
		archer = 6,
        priest = 7
	}

	public enum unitActions{
        none = 0,
		idle = 1,
		readyToMove = 2,
		moving = 3,
		readyToAttack = 4,
		attacking = 5,
		casting = 6
	}

	public enum unitAttributes{
        none = 0,

		//Stats
		HP = 1,
		MP =2,
		AP = 3,
		HPmax = 4,
		MPmax = 5,
		APmax = 6,

		//Base Attributes
		strenght = 7,
		dexterity = 8,
		magic = 9,
		
	
		//Def
		PhysicalDef = 10,
		magicDef = 11,
		poisonDef = 12,
		fireDef = 13,
		iceDef = 14,

        //Additional parameters
        movementDistance = 15
	}

	public enum damageTypes{
        none = 0,
		poison = 1,
		fire = 2,
		water = 3,
		ice = 4,
		electricity = 5,
		earth = 6,
		darkness = 7,
		light = 8,
		//-------------
		slashing = 9,
		pearcing = 10,
		blunt = 11
	}

	public enum attackTypes{
        none = 0,
		melee = 1,
		ranged = 2,
		magic = 3,
		heal = 4
	}

	public enum areaPatterns{
        none = 0,
		line = 1,
		circle = 2,
		cross = 3,
		standart = 4
	}

	public enum playerTurnStates{
        none = 0,
		start = 1,
		end = 2,
		nextPlayerTurn = 3
	}

	public enum unitTurnStates{
        none = 0,
		canInteract = 1,
		blockInteract = 2
	}

	public enum editorStates{
        none = 0,
		setType = 1,
		setHeight = 2,
        setSpawnZone = 3,
        spawnObject = 4
	}

	public enum tooltipTypes{
        none = 0,
		effect = 1,
		ability = 2,
		unit = 3
	}

	public enum matchStates{
        none = 0,
		selectUnits = 1,
		placeUnits = 2,
		battle = 3,
		victory = 4
	}

	public enum playerType{
        none = 0,
		player = 1,
		ai = 2,
		spectr = 3
	}

    public enum TileType
    {
        Normal,
        Difficult,
        VeryDifficult,
        Impassible
    }
}
