using UnityEngine;
using System.Collections;

public class ButtonEvents : MonoBehaviour {
    public void OnEndTurnClick()
    {
        GM.BattleLogic.EndPlayerTurn(GM.BattleData.currentPlayer);
    }
}
