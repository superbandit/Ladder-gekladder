using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public AudioClip jump;

    Rigidbody2D body;

    int speed = 5;

	void Start ()
    {
        body = GetComponent<Rigidbody2D>();
	}
	
	void FixedUpdate ()
    {
        body.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, body.velocity.y);

        RaycastHit2D hitA = Physics2D.Raycast(new Vector2(transform.position.x - 0.4f, transform.position.y -0.51f), Vector2.down);
        RaycastHit2D hitB = Physics2D.Raycast(new Vector2(transform.position.x + 0.4f, transform.position.y - 0.51f), Vector2.down);
        if (hitA.distance < 0.03f || hitB.distance < 0.03f)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SoundHandler.Instance.PlaySound(jump);
                body.AddForce(new Vector2(0, 350));
            }
        }
	}
    private void OnTriggerStay2D(Collider2D col)
    {
        // interact
        if (Input.GetKey(KeyCode.E))
        {
            if (col.tag == "Movable")
            {
                col.GetComponent<Rigidbody2D>().simulated = false;
                float height = col.GetComponent<SpriteRenderer>().bounds.size.y / 2;

                col.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + height);
                //verder met onthouden obj
            }
        }
    }
}
