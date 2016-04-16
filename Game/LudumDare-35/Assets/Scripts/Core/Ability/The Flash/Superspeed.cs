using UnityEngine;
using System.Collections;

public class Superspeed : Ability {

    private float walkSpeed;
    private float runSpeed;

	// Use this for initialization
	void Start () {
        runSpeed = 15f;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
            entity.speed = runSpeed;
        else entity.speed = walkSpeed;
	}

    public new void learnAbility(Entity e)
    {
        entity = e;
        walkSpeed = e.speed;
    }
}
