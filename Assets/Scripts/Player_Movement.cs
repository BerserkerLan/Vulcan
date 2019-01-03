using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour {

    string state = "";

    

	// Use this for initialization
	void Start () {
        state = "right";
	}

    private void FixedUpdate()
    {
        gameObject.GetComponent<Animator>().SetFloat("Speed", 0.0f);
        if (Input.GetKey(KeyCode.W)) //Up
        {
            gameObject.transform.position += new Vector3(0, 4.0f, 0) * Time.fixedDeltaTime;
        }

        if (Input.GetKey(KeyCode.D)) //Right
        {
            if (state == "left")
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
            state = "right";
            gameObject.transform.position += new Vector3(4.0f, 0, 0) * Time.fixedDeltaTime;
            gameObject.GetComponent<Animator>().SetFloat("Speed", 0.02f);
        }


        if (Input.GetKey(KeyCode.S)) //Down
        {
            gameObject.transform.position += new Vector3(0, -4.0f, 0) * Time.fixedDeltaTime;
        }

        if (Input.GetKey(KeyCode.A)) //Left
        {
        
            if (state == "right")
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
            state = "left";
            gameObject.transform.position += new Vector3(-4.0f, 0, 0) * Time.fixedDeltaTime;
            gameObject.GetComponent<Animator>().SetFloat("Speed", 0.02f);
        }
        
    }

 


    // Update is called once per frame
    void Update() {

    }
}
