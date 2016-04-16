using UnityEngine;
using System.Collections;

public class TimedObjectDestroyer : MonoBehaviour {

    public float Delay;

	void Start () {
        StartCoroutine(waitForTime(Delay));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator waitForTime(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameObject.Destroy(gameObject);
    }
}
