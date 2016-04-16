using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

    public Transform gotoPos;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<Player>())
        {

        }
    }
}
