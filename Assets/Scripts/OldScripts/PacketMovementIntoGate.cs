using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PacketMovementIntoGate : MonoBehaviour {

    public enum Direction
    {
        left, right
    }

    Vector3 velocity = new Vector3(-1.0f, 0.0f, 0.0f);
    public bool collided = false;
    public  Collision2D recentCollision;
    public  bool ready = false;

    public float[] gatePositions;
    public Sprite movingUpSprite;
    public Direction directionToMove;
    public static Sprite oldSprite;
    bool shouldMoveDown;
    public  int currentGateIndex;

    void Start () {
        oldSprite = movingUpSprite;
        currentGateIndex = 0;
        shouldMoveDown = false;
        
	}

	
    private void FixedUpdate()
    {

       if (ready)
        {
            movePacketIntoGate();
        }

    }
    public bool getCollision()
    {
        return collided;
    }

    public void setReady(bool b)
    {
        ready = b;
    }

    void movePacketIntoGate()
    {
        if (directionToMove == Direction.left)
        {
           
                if (transform.position.x <= gatePositions[currentGateIndex])
                {
                Debug.Log(transform.position.x);
                Debug.Log(gatePositions[currentGateIndex]);
                Debug.Log(currentGateIndex);
                    moveUp();
                }
                else
                {
                    moveLeft();
                }
            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Handle dragons and cats and lava
       
        
         //
        Debug.Log("Collided with " + collision.gameObject.name);
        recentCollision = collision;
        collided = true;
    }

    void moveLeft()
    {
        velocity = new Vector3(-4.0f, 0.0f, 0.0f);
        transform.position += velocity * Time.fixedDeltaTime;
    }
    void moveRight()
    {
        velocity = new Vector3(4.0f, 0.0f, 0.0f);
        transform.position += velocity * Time.fixedDeltaTime;
    }
    void moveUp()
    {
        GetComponent<SpriteRenderer>().sprite = movingUpSprite;
        velocity = new Vector3(0.0f, 4.0f, 0.0f);
        transform.position += velocity * Time.fixedDeltaTime;
    }
    void moveDown()
    {
        GetComponent<SpriteRenderer>().sprite = movingUpSprite;
        velocity = new Vector3(0.0f,- 4.0f, 0.0f);
        transform.position += velocity * Time.fixedDeltaTime;
    }
}
