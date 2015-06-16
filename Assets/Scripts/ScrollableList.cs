using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEditor;
using System.IO;

public class ScrollableList : MonoBehaviour
{
    public GameObject itemPrefab;
    public int itemCount = 10;
    public List<GameObject> Prefabs;
    public Texture2D[] textures;

    void Start()
    {
        
        
        Prefabs = new List<GameObject>();
        foreach (GameObject prefab in Resources.LoadAll("Level1/Prefabs", typeof(GameObject)))
        {
            if (prefab.name == "Player" || prefab.gameObject.GetComponent("MiscObject") as MiscObject ==null)
            {
                continue;
            }
            else
            {
                Prefabs.Add(prefab);
            }
        }
        itemCount = Prefabs.Count;
        textures = new Texture2D[itemCount];
        
        int j = 0;
        for (int i = 0; i < itemCount; i++)
        {
            GameObject newItem = Instantiate(itemPrefab) as GameObject;
            Text textFiled = newItem.GetComponentInChildren<Text>();
            Image imageField = newItem.transform.Find("PrefabImage").GetComponent<Image>();
            Button button = newItem.GetComponentInChildren<Button>();
            newItem.name = Prefabs[i].name + " item at (" + i + "," + j + ")";
            textFiled.text = Prefabs[i].name;
            Rect rec = new Rect(0, 0, 50, 50);
            Vector2 vec = new Vector2(0, 0);
            
             //Part of code to create prefab prewiew and save on disk
              
            int counter = 0;
            Texture2D tex = null;
            if (File.Exists(Application.dataPath + "/preview/imgs/" + Prefabs[i].name + ".png") == false)
            {
            while (tex == null && counter < 20)
            {
                tex = AssetPreview.GetAssetPreview(Prefabs[i]);
                bool load = AssetPreview.IsLoadingAssetPreview(Prefabs[i].GetInstanceID());
                Debug.Log(load);
                counter++;
                System.Threading.Thread.Sleep(15);
                if (tex == null)
                {
                    continue;
                }
                else
                {
                    byte[] bytes = tex.EncodeToPNG();
                    File.WriteAllBytes(Application.dataPath + "/preview/imgs/" + Prefabs[i].name + ".png", bytes);
                    //textures[i] = tex;
                }
            }
            }
            

            byte[] fileData;
            counter = 0;
            if (File.Exists(Application.dataPath + "/preview/imgs/" + Prefabs[i].name + ".png"))
            {
                fileData = null;
                while (fileData == null && counter < 10)
                {
                    fileData = File.ReadAllBytes(Application.dataPath + "/preview/imgs/" + Prefabs[i].name + ".png");
                    counter++;
                    textures[i] = new Texture2D(2, 2);            //<-- part to load from disc and convert to sprite
                    //textures[i] = AssetPreview.GetAssetPreview(Prefabs[i]);
                    textures[i].LoadImage(fileData);            //<-- part to load from disc and convert to sprite
                    System.Threading.Thread.Sleep(50);
                }

                textures[i].LoadImage(fileData);           // <-- part to load from disc and convert to sprite

                Sprite newsprite = new Sprite();
                newsprite = Sprite.Create(textures[i], new Rect(0, 0, textures[i].width, textures[i].height), new Vector2(textures[i].width / 2, textures[i].height / 2),
                 100f);
                imageField.sprite = newsprite;

                button.onClick.RemoveAllListeners();
                Debug.Log(Prefabs[i].name);
                //Add your new event
                string name = Prefabs[i].name;
                button.onClick.AddListener(() => MapCreatorManager.instance.handleButton(name));
            }

            
            newItem.transform.SetParent(gameObject.transform);
        }


    }


}
