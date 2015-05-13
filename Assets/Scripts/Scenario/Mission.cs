using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class Mission : MonoBehaviour
{
    public string MissionName;

    public string Description;

    public MissionGoal Goal;

    public string MissionMap;

    public List<Vector2> spawnTiles = new List<Vector2>();

    public List<Player> players = new List<Player>();
}

public class MissionGoal
{

}
