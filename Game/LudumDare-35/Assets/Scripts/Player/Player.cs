using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : Entity {

    public List<learntAbilities> learntAbilities = new List<learntAbilities>();
    public List<superheroesMet> superheroesMet = new List<superheroesMet>();

    private GameManager gm;

    public bool canMove;

	new void Start () {
        canMove = true;

        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

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
            if (a.hasCreated == false)
            {
                Ability ab = CopyComponent(a.ability, gameObject);
                ab.learnAbility(this);
                a.ability = ab;
                a.hasCreated = true;
            }
        }
    }

    public void LearnAbility(Superhero super, Ability a)
    {
        global::learntAbilities la = new global::learntAbilities();
        la.ability = a;
        la.learntFrom = super;
        this.learntAbilities.Add(la);
        updateLearntAbilities();

        bool heroUnlocked = gm.isHeroUnlocked(super);

        if (!heroUnlocked)
        {
            gm.ShowUpdatePanel("Met " + super.superheroName + ", learnt " + a.abilityName);
            MeetSuperhero(super);
        }
        else
            gm.ShowUpdatePanel("Learnt " + a.abilityName);
    }
    public void MeetSuperhero(Superhero super)
    {
        superheroesMet sm = new global::superheroesMet();
        sm.currentLevel = 0;
        sm.superhero = super;
        superheroesMet.Add(sm);
    }
	
	void Update () {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (v != 0)
            h = 0;
        if ((v != 0 || h != 0) && canMove)
            Move(h, v);
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
        DisableAllAbilities();
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
    public bool hasCreated = false;
}

[System.Serializable]
public class superheroesMet
{
    public Superhero superhero;
    public int currentLevel;
}
