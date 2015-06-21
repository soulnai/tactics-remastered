using UnityEngine;
using System.Collections;

public static class GM
{
    public static MapUtils Map { get { return MapUtils.Instance; } }
    public static BattleLogicController BattleLogic { get { return BattleLogicController.Instance; } }
    public static BattleDataController  BattleData  { get { return BattleDataController.Instance; } }
    public static InputController       Input       { get { return InputController.Instance; } }
    public static GlobalGameController  GlobalGame  { get { return GlobalGameController.Instance; } }
    public static ScenarioController    Scenario    { get { return ScenarioController.Instance; } }
    public static UIController          UI          { get { return UIController.Instance; } }
    public static ScenesController      Scenes      { get { return ScenesController.Instance; } }
    public static GlobalPrefabHolder    Prefabs     { get { return GlobalPrefabHolder.Instance; } }
    public static EventManager Events { get { return EventManager.Instance; } }

}
