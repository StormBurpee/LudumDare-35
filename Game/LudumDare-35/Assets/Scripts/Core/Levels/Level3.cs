using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Level3 : LevelBase {

    public GameManager gm;
    public StoryManager sm;
    public Player player;

    public GameObject theFlash;
    public GameObject cisco;
    public GameObject arrow;
    public Vector2 flashPos;
    public Vector2 ciscoPos;
    public Vector2 arrowPos;
    public Vector2 StarLabsPos;

    public Ability ability;

    public Vector2 ZoomPos;

    void Start () {
	
	}
	
	void Update () {
	
	}
    
    public override void StartLevel()
    {
        levelActive = true;
        gm.FinishObjective();
        List<string> narrative = new List<string>();
        narrative.Add("You: Arrow? I was sent here by The Flash to come and bring you back to Central City!");
        narrative.Add("Arrow: Thankyou, I appreciate that a lot. We should all meet at Star Labs, I've made a discovery");
        narrative.Add("Arrow: About zoom that can help us defeat him finally once and for all.");
        gm.StartNarrative(narrative, finishFirstNarrative);
    }

    public void finishFirstNarrative(string cb)
    {
        theFlash.transform.position = flashPos;
        arrow.transform.position = arrowPos;
        cisco.transform.position = ciscoPos;

        gm.LocationBasedObjective(StarLabsPos, "Star Labs", EnterStarLabs);
    }

    public void EnterStarLabs(string cb)
    {
        StartCoroutine(moveToScene());
    }

    IEnumerator moveToScene()
    {
        float i = 0.0f;
        Vector3 pos1 = player.transform.position;
        Vector3 pos2 = new Vector2(ciscoPos.x, ciscoPos.y -= 2);

        player.ChangeDirection(0);

        float rate = 1.0f / 1.0f;

        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            player.transform.position = Vector3.Lerp(pos1, pos2, i);
            yield return null;
        }

        StartNewNarrative();
    }

    public void StartNewNarrative()
    {
        List<string> messages = new List<string>();
        messages.Add("Cisco: Oh! You saved Arrow! I'm so glad!");
        messages.Add("The Flash: Yes, thank you " + gm.playerName + "...");
        messages.Add("Cisco: His name is GOLDRING-MAN not " + gm.playerName+"!");
        messages.Add("The Flash: Sorry Cisco. Arrow, GoldRing-Man said that you had some news regarding Zoom?");
        messages.Add("Arrow: During the fight that Zoom and I had, I was able to locate his weakness.");
        messages.Add("Arrow: In order to take Zoom's powers away from him, all he needs is an Arrow to the heart, and a super-fast punch.");
        messages.Add("You: Guys, I think I'm up to the task at hand. I've learnt all your guys' abilities so far!");
        messages.Add("The Flash: You will be a crucial part of this plan, GoldRing-Man.");
        messages.Add("Arrow: Flash, he's just a kid. I don't thi..");
        messages.Add("The Flash: This 'kid' just saved you. He's more than capable of doing the task at hand.");
        messages.Add("Cisco: Guys, I've just picked Zoom up on our Zoom-Radar 2000, I've marked it on all of your maps.");
        gm.StartNarrative(messages, EndStarLabsNarrative);
    }

    public void EndStarLabsNarrative(string cb)
    {
        player.LearnAbility(arrow.GetComponent<Superhero>(), ability);
        player.transform.position = ZoomPos;
        theFlash.transform.position = new Vector2(ZoomPos.x - 1, ZoomPos.y);
        arrow.transform.position = new Vector2(ZoomPos.x - 2, ZoomPos.y);
        theFlash.GetComponent<Entity>().ChangeDirection(1);
        arrow.GetComponent<Entity>().ChangeDirection(1);
        player.ChangeDirection(1);
        List<string> nMsg = new List<string>();
        nMsg.Add("Arrow: Alright Kid, you got this. Go get him...");
        gm.StartNarrative(nMsg, TriggerEndScene);
    }

    public void TriggerEndScene(string cb)
    {
        gm.EndGame();
    }

    public override void EndLevel()
    {
        levelActive = false;
        //gm.FinishObjective();
        sm.currentLevel++;
    }
}
