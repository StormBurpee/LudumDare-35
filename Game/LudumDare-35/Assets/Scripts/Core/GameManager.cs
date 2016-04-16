using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public Player player;
    public List<Superhero> superheroes = new List<Superhero>();
    public List<learntAbilities> allAbilities = new List<learntAbilities>();

    private bool shapeShiftMenuOpen;
    public GameObject shapeShiftMenu;

    public GameObject[] shapeshiftPanels;
    private List<GameObject> unlockedShapeshiftPanels = new List<GameObject>();
    public Color normal;
    public Color selected;
    private int selectedPanel;

    public GameObject shapeshiftEffect;

    public GameObject messagePanel;
    public Text message;
    public bool messageShowing;

	void Start () {
        shapeShiftMenu.SetActive(shapeShiftMenuOpen);
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

        if (messageShowing)
            handleMessage();

	}

    public void handleMessage()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            CloseMessage();
    }

    void OpenMessage(string message)
    {
        messageShowing = true;
        player.canMove = false;

        this.message.text = message;

        messagePanel.SetActive(true);
    }

    void CloseMessage()
    {
        messageShowing = false;
        player.canMove = true;
        messagePanel.SetActive(false);
        this.message.text = "";
    }

    void openShapeShiftMenu()
    {
        shapeShiftMenuOpen = true;
        shapeShiftMenu.SetActive(shapeShiftMenuOpen);
        player.canMove = false;
        unlockedShapeshiftPanels.Clear();
        foreach(GameObject go in shapeshiftPanels)
        {
            ShapeshiftPanel p = go.GetComponent<ShapeshiftPanel>();

            if (p.shapeshiftTo != null)
            {
                if (p.shapeshiftTo.GetComponent<Player>() != null)
                    unlockedShapeshiftPanels.Add(go);
                else if (isHeroUnlocked(p.shapeshiftTo.GetComponent<Superhero>()))
                {
                    unlockedShapeshiftPanels.Add(go);
                    go.SetActive(true);
                }
                else
                    go.SetActive(false);
            }
            else
                go.SetActive(false);
        }

        selectedPanel = 0;
    }

    void closeShapeShiftMenu()
    {
        shapeShiftMenuOpen = false;
        shapeShiftMenu.SetActive(shapeShiftMenuOpen);
        player.canMove = true;
    }

    void handleShapeShift()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            selectedPanel++;
        if (Input.GetKeyDown(KeyCode.DownArrow))
            selectedPanel--;
        if (selectedPanel < 0)
            selectedPanel = unlockedShapeshiftPanels.Count-1;
        if (selectedPanel >= unlockedShapeshiftPanels.Count)
            selectedPanel = 0;

        for(int i = 0; i < unlockedShapeshiftPanels.Count; i++)
        {
            if (i == selectedPanel)
                unlockedShapeshiftPanels[i].GetComponent<Image>().color = selected;
            else
                unlockedShapeshiftPanels[i].GetComponent<Image>().color = normal;
        }

        if(Input.GetKeyDown(KeyCode.Return))
        {
            if (unlockedShapeshiftPanels[selectedPanel].GetComponent<ShapeshiftPanel>().shapeshiftTo.GetComponent<Player>() != null)
                player.Shapeshift(unlockedShapeshiftPanels[selectedPanel].GetComponent<ShapeshiftPanel>().shapeshiftTo.GetComponent<Entity>());
            else
                player.Shapeshift(unlockedShapeshiftPanels[selectedPanel].GetComponent<ShapeshiftPanel>().shapeshiftTo.GetComponent<Superhero>());
            Instantiate(shapeshiftEffect, player.transform.position, Quaternion.identity);
            closeShapeShiftMenu();
        }
    }

    public bool isHeroUnlocked(Superhero s)
    {
        foreach(superheroesMet sm in player.superheroesMet)
        {
            if (sm.superhero.superheroName == s.superheroName)
                return true;
        }
        return false;
    }
}
