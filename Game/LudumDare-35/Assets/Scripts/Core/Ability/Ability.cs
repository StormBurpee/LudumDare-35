using UnityEngine;
using System.Collections;

public class Ability : MonoBehaviour {

    public string abilityName;

    protected Entity entity;

    public bool active;

	void Start () {
        active = false;
	}
	
	void Update () {
	
	}

    public void learnAbility(Entity e)
    {
        this.entity = e;
    }
}
