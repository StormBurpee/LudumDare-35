using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {

    public float speed = 2.5f;
    public float normalSpeed;
    protected Rigidbody2D _Rigidbody;
    protected int direction = 0;
    protected float health;
    public float maxHealth;

    public Sprite[] north;
    public Sprite[] east;
    public Sprite[] south;
    public Sprite[] west;

    protected void Start () {
        _Rigidbody = gameObject.GetComponent<Rigidbody2D>();
        direction = 0;
        normalSpeed = speed;
    }
	
	void Update () {
        
    }

    public void ChangeDirection(int dir)
    {
        direction = dir;
        updateSprite();
    }

    protected void Move(float h, float v)
    {
        if (h > 0)
            direction = 1;
        if (h < 0)
            direction = 3;
        if (v > 0)
            direction = 0;
        if (v < 0)
            direction = 2;
        updateSprite();

        if(_Rigidbody == null)
            _Rigidbody = gameObject.GetComponent<Rigidbody2D>();
        Move(new Vector2(h, v));
    }

    protected void Move(Vector2 dir)
    {
        Vector2 start = transform.position;
        Vector2 end = start + dir * speed * Time.deltaTime;
        Vector2 endPos = new Vector2(end.x, end.y);
        _Rigidbody.MovePosition(endPos);
    }
    
    public float getNormalSpeed()
    {
        return normalSpeed;
    }

    protected void updateSprite()
    {
        switch(direction)
        {
            case 0:
                GetComponent<SpriteRenderer>().sprite = north[0];
                break;
            case 1:
                GetComponent<SpriteRenderer>().sprite = east[0];
                break;
            case 2:
                GetComponent<SpriteRenderer>().sprite = south[0];
                break;
            case 3:
                GetComponent<SpriteRenderer>().sprite = west[0];
                break;
        }
    }
}
