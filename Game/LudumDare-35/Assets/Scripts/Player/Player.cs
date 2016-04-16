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

    void updateLearntAbilities()
    {
        foreach (learntAbilities a in learntAbilities)
        {
            Ability ab = CopyComponent(a.ability, gameObject);
            ab.learnAbility(this);
            a.ability = ab;
        }
    }

    public void LearnAbility(Superhero super, Ability a)
    {
        global::learntAbilities la = new global::learntAbilities();
        la.ability = a;
        la.learntFrom = super;
        this.learntAbilities.Add(la);
        updateLearntAbilities();
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

    public void DisableAllAbilities()
    {
        foreach(learntAbilities la in learntAbilities)
        {
            la.ability.active = false;
        }
    }
    public void EnableHeroAbilities(Superhero s)
    {
        foreach(learntAbilities la in learntAbilities)
        {
            Debug.Log(la.learntFrom.superheroName + " " + s.superheroName);
            if (la.learntFrom.superheroName == s.superheroName)
                la.ability.active = true;
            else
                la.ability.active = false;
        }
    }

    public void Shapeshift(Entity e)
    {
        DisableAllAbilities();
        north = e.north;
        east = e.east;
        south = e.south;
        west = e.west;
        updateSprite();
    }

    public void Shapeshift(Superhero s)
    {
        EnableHeroAbilities(s);
        north = s.north;
        east = s.east;
        south = s.south;
        west = s.west;
        updateSprite();
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
    public int currentLevel;
}
