using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndGameManager : MonoBehaviour {

	void Start () {
	
	}
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Return))
            Application.Quit();
    }
}
