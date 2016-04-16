using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : Entity {

    public List<learntAbilities> learntAbilities = new List<learntAbilities>();
    public List<superheroesMet> superheroesMet = new List<superheroesMet>();

    public bool canMove;

	new void Start () {
        canMove = true;

        foreach(learntAbilities a in learntAbilities)
        {
            Ability ab = CopyComponent(a.ability, gameObject);
            ab.learnAbility(this);
            a.ability = ab;
        }

        base.Start();
	}
	
	void Update () {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (v != 0)
            h = 0;
        if ((v != 0 || h != 0) && canMove)
            Move(h, v);

        if (Input.GetKeyDown(KeyCode.Y))
            learntAbilities[0].ability.active = !learntAbilities[0].ability.active;
	}

    T CopyComponent<T>(T original, GameObject destination) where T : Component
    {
        System.Type type = original.GetType();
        Component copy = destination.AddComponent(type);
        System.Reflection.FieldInfo[] fields = type.GetFields();
        foreach (System.Reflection.FieldInfo field in fields)
        {
            field.SetValue(copy, field.GetValue(original));
        }
        return copy as T;
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
