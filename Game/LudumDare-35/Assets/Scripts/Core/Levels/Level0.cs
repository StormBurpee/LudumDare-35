using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level0 : LevelBase {

    public GameObject theFlash;
    public GameManager gm;
    public StoryManager sm;
    public Player player;

    public Transform StarLabs;

    [Header("Flash's first part")]
    public Vector3 flashStart;
    public Vector3 flashEnd;
    public Vector3 flashThirdPos;

    [Header("Third Part of Mission")]
    public GameObject Cisco;
    public Vector3 movePlayerTo;
    public Vector3 CiscoPosition;
    public Vector3 flashTFPos;
    public Vector3 flashTEPos;
    public bool firstNarrativeFinished;
    public Ability newAbility;

    [Header("Variables - Don't Touch")]
    public bool firstStarted;
    public bool firstPart;
    public bool secondPartStarted;
    public bool secondPart;
    public bool thirdPartStarted;
    public bool thirdPart;
    public bool thirdPartEnded;
    public bool handleThird;
    public bool hasMoved;
    public bool talk;

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

            if (!secondPart && secondPartStarted && gm.currentLocation == "Star Labs")
                secondPart = true;
            if (secondPart && !thirdPartStarted)
                ThirdPart();

            if (thirdPartStarted && hasMoved && !talk)
                ThirdPartNarrative();

            if (!thirdPartEnded && secondPart && hasMoved && talk && !handleThird && gm.narrativeFinished)
                handleThird = true;
            if (!thirdPartEnded && handleThird)
                HandleThirdPart();
        }
	}

    public override void StartLevel()
    {
        player.canMove = false;
        player.ChangeDirection(1);
        firstStarted = false;
        firstPart = false;
        talk = false;
        secondPartStarted = false;
        StartCoroutine(moveFlash(flashStart, flashEnd));
        levelActive = true;
        Cisco.transform.position = CiscoPosition;
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

        //gm.OpenMessage("The Flash: David has a nice face.");
        List<string> msg = new List<string>();
        msg.Add("The Flash: Hey, you! I need your help, really quickly! Can you take this secret ring to Star Labs? I'll meet you there when I've done fighting crime.");
        gm.StartNarrative(msg);
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

    public void ThirdPart()
    {
        thirdPartStarted = true;
        player.canMove = false;
        player.ChangeDirection(0);
        StartCoroutine(movePlayerToPosition());
    }

    IEnumerator movePlayerToPosition()
    {
        float i = 0.0f;
        Vector3 pos1 = player.transform.position;
        Vector3 pos2 = movePlayerTo;

        float rate = 1.0f / 1.0f;

        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            player.transform.position = Vector3.Lerp(pos1, pos2, i);
            yield return null;
        }
        hasMoved = true;
    }

    public void ThirdPartNarrative()
    {
        talk = true;
        List<string> messages = new List<string>();
        messages.Add("Cisco: Hey there! Flash told us to be expecting someone...");
        messages.Add("You: Yeah, he told me to bring this gold ring to you?");
        messages.Add("Cisco: Oh good! Thanks for doing that for us.. Hold on a sec..");
        gm.StartNarrative(messages);
    }

    private bool flashArrived = false;
    private bool cfy = false;
    private bool sn = false;
    private bool snf = false;
    private bool fmst = false;
    private bool fmft = false;
    public void HandleThirdPart()
    {
        if(flashArrived && !sn)
        {
            List<string> newMessages = new List<string>();
            newMessages.Add("Cisco: Hey Flash, I uh... Just had a vibe. You know the gold ring... It's connected to him.");
            newMessages.Add("The Flash: How so Cisco? What does that mean?");
            newMessages.Add("Cisco: I think, he's the one we've been looking for. To help us with Zoom...");
            newMessages.Add("You: Ahh guys! I can hear what you're saying. What does this mean?");
            newMessages.Add("Cisco: " + gm.playerName + " you're connected to that ring. The ring, it enables you to shapeshift.");
            newMessages.Add("The Flash: It's a bit more complicated than that. You can shapeshift into any superhero you meet.");
            newMessages.Add("The Flash: When you shapeshift into that superhero, you can use some of their basic powers, and eventually...");
            newMessages.Add("The Flash: you can learn even more of their advanced powers. Here let's try this out...");
            gm.StartNarrative(newMessages);

            sn = true;
        }
        if(!flashArrived && !cfy)
        {
            cfy = true;
            StartCoroutine(BringFlashBack());
        }
        if (sn && !snf && gm.narrativeFinished)
            snf = true;

        if(snf)
        {
            if(!fmst)
            {
                fmst = true;
                StartCoroutine(MoveFlashAgain());
            }
            if(fmft)
            {
                fmft = false;
                player.LearnAbility(theFlash.GetComponent<Superhero>(), newAbility);
                List<string> evenMoreMessages = new List<string>();
                evenMoreMessages.Add("The Flash: Okay, the ring remembers me now. You've also learnt my superspeed ability.");
                evenMoreMessages.Add("The Flash: It's easy to shapeshift, all you have to do is press X, use up/down arrow keys then press enter!");
                evenMoreMessages.Add("The Flash: Remember, that you can only use an ability if it belongs to that superhero.");
                evenMoreMessages.Add("Cisco: Hey! We should call you GoldRing-Man! Another great name by yours tru...");
                evenMoreMessages.Add("Cisco: Hold up guys, the fun and games is now over... It looks like zoom has banished Arrow to an island.");
                evenMoreMessages.Add("The Flash: Hey, " + gm.playerName + " do you think you could go find WonderWoman, and learn how to fly?");
                evenMoreMessages.Add("Cisco: I hear that she hangs out in the bottom right of central city - It's a dirt road to her, surrounded by trees and flowers");
                evenMoreMessages.Add("The Flash: Once you've learnt how to fly, you'll be able to fly across and help out Arrow.");
                evenMoreMessages.Add("Cisco: He might even teach you how to shoot an arrow!");
                gm.StartNarrative(evenMoreMessages);
                EndLevel();
            }
        }
    }

    IEnumerator MoveFlashAgain()
    {
        float i = 0.0f;
        Vector3 pos1 = flashTEPos;
        Vector3 pos2 = new Vector3(movePlayerTo.x, movePlayerTo.y++);
        theFlash.GetComponent<Entity>().ChangeDirection(3);
        float rate = 1.0f / 0.5f;

        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            theFlash.transform.position = Vector3.Lerp(pos1, pos2, i);
            yield return null;
        }
        theFlash.GetComponent<Entity>().ChangeDirection(2);
        fmft = true;
    }

    IEnumerator BringFlashBack()
    {
        float i = 0.0f;
        Vector3 pos1 = flashTFPos;
        Vector3 pos2 = flashTEPos;

        float rate = 1.0f / 0.5f;

        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            theFlash.transform.position = Vector3.Lerp(pos1, pos2, i);
            yield return null;
        }
        flashArrived = true;
    }

    public override void EndLevel()
    {
        levelActive = false;
        gm.FinishObjective();
        sm.currentLevel++;
        //theFlash.transform.position = new Vector2(-100, -100);
    }
}
