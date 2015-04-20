using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class TileXml {
	[XmlAttribute("id")]
	public int id;
	[XmlAttribute("height")]
	public float height;
	[XmlAttribute("locX")]
	public int locX;

	[XmlAttribute("locY")]
	public int locY;
}

[XmlRoot("MapCollection")]
public class MapXmlContainer {
	[XmlAttribute("size")]
	public int size;

	[XmlArray("Tiles")]
	[XmlArrayItem("Tile")]
	public List<TileXml> tiles = new List<TileXml>();

	[XmlArray("Trees")]
	[XmlArrayItem("Tree")]
	public List<TreeXml> trees = new List<TreeXml>();
	
	[XmlArray("Crates")]
	[XmlArrayItem("Crate")]
	public List<CrateXml> crates = new List<CrateXml>();
}

public class TreeXml {
	[XmlAttribute("locX")]
	public int locX;
	
	[XmlAttribute("locY")]
	public int locY;

	[XmlAttribute("prefabName")]
	public string prefabName;
}

public class CrateXml {
	[XmlAttribute("locX")]
	public int locX;
	
	[XmlAttribute("locY")]
	public int locY;
	
	[XmlAttribute("prefabName")]
	public string prefabName;
}

public class SpawnTileXml {
	[XmlAttribute("locX")]
	public int locX;
	
	[XmlAttribute("locY")]
	public int locY;
}


public class UnitXml
{
    [XmlAttribute("prefabName")]
    public string prefabName;
}

public class PlayerXml
{
    [XmlElement("Unit")]
    public List<UnitXml> units = new List<UnitXml>();

    [XmlAttribute("name")]
    public string name;
}

public class MissionNameXml
{
    [XmlAttribute("name")]
    public string name;
}

public class MissionDescriptionXml
{
    [XmlAttribute("description")]
    public string description;
}

public class MissionTargetXml
{
    [XmlAttribute("target")]
    public string target;
}

public class MissionMapXml
{
    [XmlAttribute("map")]
    public string map;
}

[XmlRoot("MissionDetailsCollection")]
public class MissionDetailsXmlContainer
{
    [XmlElement("MissionName")]
    public MissionNameXml missionName = new MissionNameXml();

    [XmlElement("MissionDescription")]
    public MissionDescriptionXml missionDescription = new MissionDescriptionXml();

    [XmlElement("MissionTarget")]
    public MissionTargetXml missionTarget = new MissionTargetXml();

    [XmlElement("MissionMap")]
    public MissionMapXml missionMap = new MissionMapXml();

	[XmlArray("SpawnArea")]
	[XmlArrayItem("SpawnTile")]
	public List<SpawnTileXml> spawnTiles = new List<SpawnTileXml>();
	
	[XmlArray("Players")]
	[XmlArrayItem("Player")]
	public List<PlayerXml> players = new List<PlayerXml>();
}



public static class MapSaveLoad {
	public static MapXmlContainer CreateMapContainer(List <List<Tile>> map) {

		List<TileXml> tiles = new List<TileXml>();

		for(int i = 0; i < map.Count; i++) {
			for (int j = 0; j < map.Count; j++) {
				tiles.Add(MapSaveLoad.CreateTileXml(map[i][j]));
			}
		}

		return new MapXmlContainer() {
			size = map.Count,
			tiles = tiles
		};
	}

	public static TileXml CreateTileXml(Tile tile) {
		return new TileXml() {
			id = (int)tile.type,
			height = (float)tile.height,
			locX = (int)tile.gridPosition.x,
			locY = (int)tile.gridPosition.y
		};
	}

	public static void Save(MapXmlContainer mapContainer, string filename) {
		var serializer = new XmlSerializer(typeof(MapXmlContainer));
		using(var stream = new FileStream(filename, FileMode.Create))
		{
			serializer.Serialize(stream, mapContainer);
		}
	}

	public static MapXmlContainer Load(string filename) {
		var serializer = new XmlSerializer(typeof(MapXmlContainer));
        using (var stream = new FileStream(Path.Combine(Application.dataPath, filename), FileMode.Open))
		{
			return serializer.Deserialize(stream) as MapXmlContainer;
		}

	}


    public static MissionDetailsXmlContainer LoadMapDetails(string filename)
    {
        var serializer = new XmlSerializer(typeof(MissionDetailsXmlContainer));
        using (var stream = new FileStream(Path.Combine(Application.dataPath, filename), FileMode.Open))
        {
            return serializer.Deserialize(stream) as MissionDetailsXmlContainer;
        }

    }
}
