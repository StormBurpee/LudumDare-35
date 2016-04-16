using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

public class CreateWorld : MonoBehaviour {

    public List<Layer> layers = new List<Layer>();

    public int mapWidth;

    public List<TileAndID> tiles = new List<TileAndID>();

    void Start() {

        int orderLayer = 0;

        foreach(Layer layer in layers)
        {
            string[] lt = layer.layerText.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);

            generateMap(lt, orderLayer);

            orderLayer++;
        }
    }

    void generateMap(String[] layerArray, int orderLayer)
    {
        int ycount = 0;
        int y = 0 + (int)(mapWidth / 2);
        int x = 0 - (int)(mapWidth / 2);

        GameObject go = new GameObject();

        go.transform.SetParent(transform);

        for (int i = 0; i < layerArray.Length - 1; i++)
        //for (int i = 0; i < 150; i++)
        {
            if (ycount >= mapWidth)
            {
                ycount = 0;
                y--;
                x = 0 - (int)(mapWidth / 2);
            }

            GameObject tile = GetTileByID(int.Parse(layerArray[i]));

            if (tile != null)
            {
                Vector2 pos = new Vector2(x, y);
                GameObject t = Instantiate(tile, pos, Quaternion.identity) as GameObject;
                t.transform.SetParent(go.transform);
                t.GetComponent<SpriteRenderer>().sortingOrder = orderLayer;
            }

            x++;
            ycount++;
        }
    }
	
	void Update () {
	
	}

    public GameObject GetTileByID(int tileID)
    {
        foreach(TileAndID tai in tiles)
        {
            if (tai.tileID == tileID)
                return tai.tile;
        }
        return null;
    }
}

[System.Serializable]
public class TileAndID
{
    public int tileID;
    public GameObject tile;
}

[System.Serializable]
public class Layer
{
    public TextAsset layerText;
}