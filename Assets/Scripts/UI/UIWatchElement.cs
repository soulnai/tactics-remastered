using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIWatchElement : MonoBehaviour
{
    public Player PlayerToWatch;
    private Unit _unitToWatch;

    public Unit UnitToWatch
    {
        get { return _unitToWatch; }
        set { _unitToWatch = value;
            if (value != null) StartWatch();
        }
    }

    public virtual void StartWatch()
    {
        
    }

    public void Init(Player player = null,Unit unit = null)
    {
        PlayerToWatch = player;
        UnitToWatch = unit;
        StartWatch();
    }
}
