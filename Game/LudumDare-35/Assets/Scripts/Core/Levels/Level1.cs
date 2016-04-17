using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Level1 : LevelBase {

    public GameObject wonderWoman;
    public GameObject theFlash;
    public GameManager gm;
    public StoryManager sm;
    public Player player;

    public GameObject cisco;
    public Vector2 CiscoPosition;

    public Vector2 starLabsLocation;
    public Vector2 wonderWomanLocation;

    void Start () {
	
	}
	
	void Update () {
	
	}

    public override void StartLevel()
    {
        cisco.transform.position = CiscoPosition;
        theFlash.transform.position = new Vector2(-100, -100);

        levelActive = true;
        List<string> firstDialogue = new List<string>();
        firstDialogue.Add("You: Sorry to bother you, wo...");
        firstDialogue.Add("WonderWoman: Don't be sorry! It's my pleasure! I see you're wearing the Gold Ring from StarLabs.");
        firstDialogue.Add("WonderWoman: Does that mean, The Flash and Cisco finally found someone who could utilise it's powers?");
        firstDialogue.Add("You: Yeah! It's pretty cool actually. However they sent me here, to be able to learn to fly from you.");
        firstDialogue.Add("You: They said that the Arrow was in danger on an island not too far from here.");
        firstDialogue.Add("WonderWoman: Hmm, I'll make a deal with you! If you get Cisco to fix my Lasso, I'll teach you to fly!");
        firstDialogue.Add("WonderWoman: All you have to do is return to Star Labs, and he'll fix it up!");
        firstDialogue.Add("You: Sounds easy! I'll get right too it!");
        gm.StartNarrative(firstDialogue, firstDialogueFinished);
    }

    public void firstDialogueFinished(string cb)
    {
        gm.LocationBasedObjective(starLabsLocation, "Star Labs", arrivedAtStarLabs);
    }

    public void arrivedAtStarLabs(string cb)
    {
        player.canMove = false;
        StartCoroutine(movePlayer());
    }

    public void ciscoNarrative()
    {
        List<string> messages = new List<string>();
        messages.Add("Cisco: Oh hey there again, GOLDRING-MAN what can I do for you today?");
        messages.Add("You: Well, I found WonderWoman, but apparently her Lasso is broken. She said you could fix it?");
        messages.Add("Cisco: My man! I am the man! I'll fix it right up for you.");
        messages.Add("Cisco: 1 and...");
        messages.Add("Cisco: 2 and...");
        messages.Add("Cisco: 3! Bobs your uncle! Well maybe... Is your uncles nam...");
        messages.Add("You: Cisco, please!");
        messages.Add("Cisco: Yes! Sorry, here's one wonderful Lasso ready for you!");
        messages.Add("You: Thanks again Cisco!");
        gm.StartNarrative(messages, ReceivedLasso);
    }

    public void ReceivedLasso(string cb)
    {
        gm.PlaceObjective(wonderWomanLocation);
        gm.ShowUpdatePanel("Received Lasso.. Return to WonderWoman");
        EndLevel();
    }

    IEnumerator movePlayer()
    {
        float i = 0.0f;
        Vector3 pos1 = player.transform.position;
        Vector3 pos2 = new Vector2(CiscoPosition.x, CiscoPosition.y -= 2);

        player.ChangeDirection(0);

        float rate = 1.0f / 1.0f;

        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            player.transform.position = Vector3.Lerp(pos1, pos2, i);
            yield return null;
        }
        ciscoNarrative();
    }

    public override void EndLevel()
    {
        levelActive = false;
        //gm.FinishObjective();
        sm.currentLevel++;
    }
}
