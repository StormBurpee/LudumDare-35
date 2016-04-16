using UnityEngine;
using System.Collections;

public class Ability : MonoBehaviour {

    public string abilityName;

    protected Entity entity;

	void Start () {
	
	}
	
	void Update () {
	
	}

    public void learnAbility(Entity e)
    {
        this.entity = e;
    }
}
