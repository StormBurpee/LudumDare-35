using UnityEngine;
using System.Collections;

public class LevelTrigger : MonoBehaviour {

    [Header("Should move player")]
    public bool movePlayer;
    public Vector2 toPos;
    public int playerDir;

    [Header("")]
    public StoryManager sm;
    public int levelToStart;

	void Start () {
	
	}
	
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<Player>() != null)
        {
            if (sm.currentLevel == levelToStart)
            {
                if (!movePlayer)
                {
                    sm.StartLevel(levelToStart);

                    Destroy(gameObject);
                }
                else
                    StartCoroutine(MovePlayer(col.transform));

            }
        }
    }

    IEnumerator MovePlayer(Transform p)
    {
        float i = 0.0f;
        Vector3 pos1 = p.position;
        Vector3 pos2 = toPos;

        p.GetComponent<Entity>().ChangeDirection(playerDir);

        float rate = 1.0f / 1.0f;

        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            p.position = Vector3.Lerp(pos1, pos2, i);
            yield return null;
        }
        sm.StartLevel(levelToStart);

        Destroy(gameObject);
    }
}
