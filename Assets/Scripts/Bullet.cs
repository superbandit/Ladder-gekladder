using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int side = 0;
    private int speed = 20;

	void Start ()
    {

	}
	
	void Update ()
    {
        transform.Translate(new Vector2(speed * side, 0) * Time.deltaTime);
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

     void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            Destroy(col.gameObject);
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag != "Movable")
        {
            Destroy(this.gameObject);
        }
    }
}
