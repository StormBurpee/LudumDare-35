using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

    public Transform gotoPos;
    public string title;
    private GameManager gm;
    public bool isLocked;

	// Use this for initialization
	void Start () {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<Player>())
        {

            if (!isLocked)
            {
                if (gm != null)
                {
                    gm.ShowUpdatePanel(title);
                    gm.currentLocation = title;
                }
                col.transform.position = gotoPos.position;
            }
            else
                if (gm != null) gm.ShowUpdatePanel("The door is locked.");
        }
    }
}
