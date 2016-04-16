using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class CreateWorld : MonoBehaviour {

    public TextAsset layer1;
    public TextAsset layer2;
    public GameObject layer1go;
    public GameObject layer2go;

    public List<TileAndID> tiles = new List<TileAndID>();

	void Start () {
        string[] layer1Array = layer1.text.Split(","[0]);
        string[] layer2Array = layer2.text.Split(","[0]);
        Debug.Log(layer1Array.Length);
        Debug.Log(layer2Array.Length);
    }
	
	void Update () {
	
	}
}

[System.Serializable]
public class TileAndID
{
    public int tileID;
    public GameObject tile;
}