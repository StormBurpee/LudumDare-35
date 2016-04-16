using UnityEngine;
using System.Collections;

public class Superspeed : Ability {

    private float walkSpeed;
    private float runSpeed;

	// Use this for initialization
	void Start () {
        runSpeed = 20f;
	}
	
	// Update is called once per frame
	void Update () {
        if (active)
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                entity.speed = runSpeed;
            else
                entity.speed = entity.getNormalSpeed();
        }
	}

    public new void learnAbility(Entity e)
    {
        entity = e;
        Debug.Log("Learnt Ability");
    }
}
