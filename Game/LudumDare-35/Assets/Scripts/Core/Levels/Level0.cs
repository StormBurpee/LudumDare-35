using UnityEngine;
using System.Collections;

public class Level0 : LevelBase {

    public GameObject theFlash;
    public GameManager gm;
    public Player player;

    public Transform StarLabs;

    [Header("Flash's first part")]
    public Vector3 flashStart;
    public Vector3 flashEnd;
    public Vector3 flashThirdPos;

    [Header("")]
    private bool firstStarted;
    private bool firstPart;
    private bool secondPartStarted;

	void Start () {
	    
	}
	
	void Update () {
        if (levelActive)
        {
            if (firstPart == false && !gm.messageShowing && firstStarted)
                firstPart = true;

            if (firstPart && !secondPartStarted)
                SecondPart();

            if (!firstPart)
                player.canMove = false;
        }
	}

    public override void StartLevel()
    {
        player.canMove = false;
        player.ChangeDirection(1);
        firstStarted = false;
        firstPart = false;
        secondPartStarted = false;
        StartCoroutine(moveFlash(flashStart, flashEnd));
        levelActive = true;
    }

    IEnumerator moveFlash(Vector3 pos1, Vector3 pos2)
    {
        float i = 0.0f;

        theFlash.GetComponent<Entity>().ChangeDirection(0);

        float rate = 1.0f / 0.5f;

        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            theFlash.transform.position = Vector3.Lerp(pos1, pos2, i);
            yield return null;
        }

        gm.OpenMessage("The Flash: Hey, you! I need your help, really quickly! Can you take this secret ring to Star Labs? I'll meet you there when I've done fighting crime.");
        firstStarted = true;
        theFlash.GetComponent<Entity>().ChangeDirection(3);
    }

    IEnumerator moveFlashT(Vector3 pos1, Vector3 pos2, int dir)
    {
        float i = 0.0f;

        theFlash.GetComponent<Entity>().ChangeDirection(dir);

        float rate = 1.0f / 0.5f;

        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            theFlash.transform.position = Vector3.Lerp(pos1, pos2, i);
            yield return null;
        }
        theFlash.GetComponent<Entity>().ChangeDirection(dir);
    }

    public void SecondPart()
    {
        secondPartStarted = true;
        gm.ShowUpdatePanel("Gold Ring Acquired");
        StartCoroutine(moveFlashT(flashEnd, flashThirdPos, 2));
        gm.PlaceObjective(StarLabs.position);
    }

    public override void EndLevel()
    {
        levelActive = false;
    }
}
