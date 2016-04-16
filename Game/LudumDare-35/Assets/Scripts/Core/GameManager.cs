using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public Player player;
    public List<Superhero> superheroes = new List<Superhero>();

    private bool shapeShiftMenuOpen;
    public GameObject shapeShiftMenu;

	void Start () {
	    
	}
	
	void Update () {

        if (shapeShiftMenuOpen)
            handleShapeShift();

        if(Input.GetKeyDown(KeyCode.X))
        {
            if (shapeShiftMenuOpen)
                closeShapeShiftMenu();
            else
                openShapeShiftMenu();
        }

	}

    void openShapeShiftMenu()
    {
        shapeShiftMenuOpen = true;

    }

    void closeShapeShiftMenu()
    {
        shapeShiftMenuOpen = false;
    }

    void handleShapeShift()
    {

    }
}
