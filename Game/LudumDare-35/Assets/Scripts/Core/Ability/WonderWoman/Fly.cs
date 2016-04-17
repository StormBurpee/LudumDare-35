using UnityEngine;
using System.Collections;

public class Fly : Ability
{

	void Start () {
	
	}
	
	void Update () {
	
	}

    public new void learnAbility(Entity e)
    {
        entity = e;
    }
}
