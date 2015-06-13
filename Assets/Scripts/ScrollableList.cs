using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEditor;
using System.IO;

public class ScrollableList : MonoBehaviour
{
    public GameObject itemPrefab;
    public int itemCount = 10, columnCount = 1;
    public List<GameObject> Prefabs;
    public Texture2D[] textures;

    void Start()
    {
        itemCount = GM.Prefabs.Prefabs.Count;
        textures = new Texture2D[itemCount];
        Prefabs = new List<GameObject>();
        foreach (GameObject prefab in Resources.LoadAll("Level1/Prefabs", typeof(GameObject)))
        {
            if (prefab.name == "Player")
            {
                continue;
            }
            else
            {
                Prefabs.Add(prefab);
            }
        }

        RectTransform rowRectTransform = itemPrefab.GetComponent<RectTransform>();
        RectTransform containerRectTransform = gameObject.GetComponent<RectTransform>();

        //calculate the width and height of each child item.
        float width = containerRectTransform.rect.width / columnCount;
        float ratio = width / rowRectTransform.rect.width;
        float height = rowRectTransform.rect.height * ratio;
        int rowCount = itemCount / columnCount;
        if (itemCount % rowCount > 0)
            rowCount++;

        //adjust the height of the container so that it will just barely fit all its children
        float scrollHeight = height * rowCount;
        containerRectTransform.offsetMin = new Vector2(containerRectTransform.offsetMin.x, -scrollHeight / 2);
        containerRectTransform.offsetMax = new Vector2(containerRectTransform.offsetMax.x, scrollHeight / 2);

        int j = 0;
        for (int i = 0; i < itemCount; i++)
        {
            //this is used instead of a double for loop because itemCount may not fit perfectly into the rows/columns
            if (i % columnCount == 0)
                j++;

            //create a new item, name it, and set the parent
            GameObject newItem = Instantiate(itemPrefab) as GameObject;
            Text textFiled = newItem.GetComponentInChildren<Text>();
            Image imageField = newItem.transform.Find("PrefabImage").GetComponent<Image>();
            Button button = newItem.GetComponentInChildren<Button>();
            newItem.name = Prefabs[i].name + " item at (" + i + "," + j + ")";
            textFiled.text = Prefabs[i].name;
            Rect rec = new Rect(0, 0, 50, 50);
            Vector2 vec = new Vector2(0, 0);
            /*
             * Part of code to create prefab prewiew and save on disk
             * 
            int counter = 0;
            Texture2D tex = null;
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
                    textures[i] = tex;
                }
            }
            */

            byte[] fileData;
            int counter = 0;
            //if (File.Exists(Application.dataPath + "/preview/imgs/" + Prefabs[i].name + ".png"))
            //{
                //fileData = null;
                while (textures[i] == null && counter < 15)
                {
                    //fileData = File.ReadAllBytes(Application.dataPath + "/preview/imgs/" + Prefabs[i].name + ".png");
                    counter++;
                    //textures[i] = new Texture2D(2, 2);            <-- part to load from disc and convert to sprite
                    textures[i] = AssetPreview.GetAssetPreview(Prefabs[i]);
                    //textures[i].LoadImage(fileData);            <-- part to load from disc and convert to sprite
                    System.Threading.Thread.Sleep(150);
                }

                //textures[i].LoadImage(fileData);            <-- part to load from disc and convert to sprite

                Sprite newsprite = new Sprite();
                newsprite = Sprite.Create(textures[i], new Rect(0, 0, textures[i].width, textures[i].height), new Vector2(textures[i].width / 2, textures[i].height / 2),
                 100f);
                imageField.sprite = newsprite;

                button.onClick.RemoveAllListeners();
                Debug.Log(Prefabs[i].name);
                //Add your new event
                string name = Prefabs[i].name;
                button.onClick.AddListener(() => MapCreatorManager.instance.handleButton(name));
            //}

            
            newItem.transform.SetParent(gameObject.transform);
            //move and size the new item
            RectTransform rectTransform = newItem.GetComponent<RectTransform>();

            float x = -containerRectTransform.rect.width / 2 + width * (i % columnCount);
            float y = containerRectTransform.rect.height / 2 - height * j;
            rectTransform.offsetMin = new Vector2(x, y);

            x = rectTransform.offsetMin.x + width;
            y = rectTransform.offsetMin.y + height;
            rectTransform.offsetMax = new Vector2(x, y);
        }


    }


}
