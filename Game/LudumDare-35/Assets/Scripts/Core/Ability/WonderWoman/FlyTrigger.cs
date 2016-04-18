using UnityEngine;
using System.Collections;

public class FlyTrigger : MonoBehaviour {

    public Collider2D disCol;

	void Start () {
        disCol = transform.parent.GetComponent<Collider2D>();
	}
	
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<Player>() != null)
        {
            if (col.GetComponent<Player>().GetComponent<Fly>() != null)
            {
                if (col.GetComponent<Fly>().active == true)
                    disCol.enabled = false;
                else
                    disCol.enabled = true;
            }
            else
                disCol.enabled = true;
        }
        else
            disCol.enabled = true;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        disCol.enabled = false;
    }
}
