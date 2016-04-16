using UnityEngine;
using System.Collections;

public class SmoothFollow : MonoBehaviour {

    public float dampTime = 0.15f;
    private Vector3 vel = Vector3.zero;
    public Transform target;

	void Start () {
	
	}
	
	void Update () {
        if (target)
        {
            Vector3 point = GetComponent<Camera>().WorldToViewportPoint(target.position);
            Vector3 delta = target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
            Vector3 destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref vel, dampTime);
        }
    }
}
