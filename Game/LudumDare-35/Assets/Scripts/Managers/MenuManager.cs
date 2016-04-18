using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuManager : MonoBehaviour {

	void Start () {
	
	}
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Return))
            SceneManager.LoadScene(1);
	}
}
