using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public string playerName = "Sam";

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
    private List<string> nextMessages = new List<string>();
    public bool messageShowing;

    public GameObject updatePanel;
    public Text updateText;

    public float messageTime;
    bool typingMessage = false;
    string fullMessage;

    public string currentLocation;

    public GameObject mapObjective;
    private GameObject mapObjectiveG;
    public GameObject miniMap;

    [HideInInspector]
    public bool narrativeFinished;

	void Start () {
        shapeShiftMenu.SetActive(shapeShiftMenuOpen);
        updatePanel.SetActive(false);
        //OpenMessage("Hey there, Ronny! I'm the flash. The fastest man alive.");
        ShowUpdatePanel("One hour ago...");
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

        if (currentLocation == "Central City" && !shapeShiftMenuOpen && !messageShowing)
            miniMap.SetActive(true);
        else
            miniMap.SetActive(false);
	}

    public void ShowUpdatePanel(string message)
    {
        updatePanel.SetActive(true);
        updateText.text = message;
        StartCoroutine(ClosePanelAfterTime());
    }

    IEnumerator ClosePanelAfterTime()
    {
        yield return new WaitForSeconds(3.5f);
        CloseUpdatePanel();
    }

    public void CloseUpdatePanel()
    {
        updatePanel.SetActive(false);
        updateText.text = "";
    }

    public void handleMessage()
    {
        if (Input.GetKeyDown(KeyCode.Return) && !typingMessage)
        {
            if (nextMessages.Count > 0)
                ShowNextMessage();
            else
                CloseMessage();
        }
        else if (Input.GetKeyDown(KeyCode.Return) && typingMessage)
            GetOnWithIt();
    }

    public void ShowNextMessage()
    {
        OpenMessage(nextMessages[0]);
        nextMessages.RemoveAt(0);
    }

    public void StartNarrative(List<string> messagesToShow)
    {
        narrativeFinished = false;
        nextMessages.Clear();
        nextMessages = messagesToShow;
        ShowNextMessage();
    }

    public void OpenMessage(string message)
    {
        messageShowing = true;
        player.canMove = false;

        this.message.text = "";
        fullMessage = message;
        StartCoroutine(ShowMessage(message));
        messagePanel.SetActive(true);
    }
    void GetOnWithIt()
    {
        typingMessage = false;
        this.message.text = fullMessage;
    }

    IEnumerator ShowMessage(string message)
    {
        string text = "";
        typingMessage = true;
        foreach(char letter in message.ToCharArray())
        {
            if (typingMessage)
            {
                text += letter;
                this.message.text = text;
                yield return new WaitForSeconds(messageTime);
            }
            else
                break;
        }
        typingMessage = false;
    }

    void CloseMessage()
    {
        messageShowing = false;
        player.canMove = true;
        messagePanel.SetActive(false);
        this.message.text = "";
        narrativeFinished = true;
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

    public void PlaceObjective(Vector2 pos)
    {
        mapObjectiveG = Instantiate(mapObjective, pos, Quaternion.identity) as GameObject;
    }

    public void FinishObjective()
    {
        Destroy(mapObjectiveG);
    }
}
