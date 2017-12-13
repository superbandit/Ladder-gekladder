using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D body;
    int speed = 5;

	void Start ()
    {
        body = GetComponent<Rigidbody2D>();
	}
	
	void FixedUpdate ()
    {
        if(Mathf.Abs(body.velocity.x ) < 2.5f)
        body.AddForce(new Vector2(Input.GetAxisRaw("Horizontal") * 20, 0));
        
        if(Input.GetKeyDown(KeyCode.Space))
        {
            body.AddForce(new Vector2(0, 350));
        }
	}
}
