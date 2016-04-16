using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : Entity {

    public List<learntAbilities> learntAbilities = new List<learntAbilities>();
    public List<superheroesMet> superheroesMet = new List<superheroesMet>();

    public bool canMove;

	void Start () {
        
	}
	
	void Update () {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (v != 0)
            h = 0;
        if ((v != 0 || h != 0) && canMove)
            Move(h, v);
	}
}

[System.Serializable]
public class learntAbilities
{
    public Ability ability;
    public Superhero learntFrom;
}

[System.Serializable]
public class superheroesMet
{
    public Superhero superhero;
}
