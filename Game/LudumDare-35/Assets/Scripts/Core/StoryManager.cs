using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StoryManager : MonoBehaviour {

    public int currentLevel;
    public List<LevelBase> allLevels = new List<LevelBase>();
    private LevelBase clb;
    
	void Start () {
        StartLevel(currentLevel);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void StartLevel(int level)
    {
        if (allLevels[level] != null)
        {
            allLevels[level].StartLevel();
        }
    }

    public void LevelEnd()
    {
        if (allLevels[currentLevel] != null)
            allLevels[currentLevel].EndLevel();
        currentLevel++;
    }
}

[System.Serializable]
public class AllLevels
{
    public GameObject levelGO;
    public LevelBase level;
}