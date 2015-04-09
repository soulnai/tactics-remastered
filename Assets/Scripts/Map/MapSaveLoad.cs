﻿using UnityEngine;
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
}

public class TreeXml {
	[XmlAttribute("locX")]
	public int locX;
	
	[XmlAttribute("locY")]
	public int locY;

	[XmlAttribute("type")]
	public int type;
}

public class CrateXml {
	[XmlAttribute("locX")]
	public int locX;
	
	[XmlAttribute("locY")]
	public int locY;
	
	[XmlAttribute("type")]
	public int type;
}

[XmlRoot("MapStuffCollection")]
public class MapStuffXmlContainer {
	[XmlAttribute("size")]
	public int size;
	
	[XmlArray("Trees")]
	[XmlArrayItem("Tree")]
	public List<TreeXml> trees = new List<TreeXml>();

	[XmlArray("Crates")]
	[XmlArrayItem("Crate")]
	public List<CrateXml> crates = new List<CrateXml>();
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
		TextAsset rawData;
		rawData = (TextAsset) Resources.Load("map");
		using(var stream = new XmlTextReader(new StringReader(rawData.text)))
		{
			return serializer.Deserialize(stream) as MapXmlContainer;
		}

	}

	public static MapStuffXmlContainer LoadStuff(string filename) {
		var serializer = new XmlSerializer(typeof(MapStuffXmlContainer));
		TextAsset rawData;
		rawData = (TextAsset) Resources.Load("stuff");
		using(var stream = new XmlTextReader(new StringReader(rawData.text)))
		{
			return serializer.Deserialize(stream) as MapStuffXmlContainer;
		}
		
	}
}
