using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level2 : LevelBase {

    public GameObject wonderWoman;
    public GameManager gm;
    public StoryManager sm;
    public Player player;

    public Vector2 arrowLocation;

    public Ability abilityToLearn;

    void Start () {
	
	}
	
	void Update () {
	
	}

    public override void StartLevel()
    {
        levelActive = true;
        List<string> firstDialogue = new List<string>();
        firstDialogue.Add("WonderWoman: Ahh! Pefect! Thank you so much, " + gm.playerName);
        firstDialogue.Add("WonderWoman: I guess it's now turn to keep up my end of the deal!");
        firstDialogue.Add("WonderWoman: In order to ensure you don't get lost at sea, I've made sure...");
        firstDialogue.Add("WonderWoman: That you can't traverse all ocean locations.");
        firstDialogue.Add("WonderWoman: I've put a waypoint on your map, for where you can cross the ocean!");
        firstDialogue.Add("WonderWoman: Goodluck on brining Arrow back!");
        gm.StartNarrative(firstDialogue, dialogueFinished);
    }

    public void dialogueFinished(string cb)
    {
        player.LearnAbility(wonderWoman.GetComponent<Superhero>(), abilityToLearn);
        gm.PlaceObjective(arrowLocation);
        EndLevel();
    }

    public override void EndLevel()
    {
        levelActive = false;
        //gm.FinishObjective();
        sm.currentLevel++;
    }
}
