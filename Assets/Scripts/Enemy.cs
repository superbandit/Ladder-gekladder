using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D body;
    float speed = 1.5f;
    float nextMoveTimer = 1;
    string currentAction = null;

	void Start ()
    {
        body = GetComponent<Rigidbody2D>();
        InvokeRepeating("ChooseAction", 0, nextMoveTimer);
    }
	
	void Update ()
    {
        switch (currentAction)
        {
            case "lockOn":
                MoveTowardsPlayer();
                break;
            case "idle":
                Moveidle();
                break;

        }
	}

    private void ChooseAction()
    {
        if (GameHandler.Instance.player != null && Vector2.Distance(transform.position, GameHandler.Instance.player.transform.position) < 4)
        {
            currentAction = "lockOn";
        }
        else
        {
            currentAction = "idle";
        }
    }

    private void MoveTowardsPlayer()
    {
        float side = (Mathf.Clamp(GameHandler.Instance.player.transform.position.x - transform.position.x, -1, 1));
        body.velocity = new Vector2(side * speed, body.velocity.y);
    }

    private void Moveidle()
    {
        transform.Translate(Mathf.Sin(Time.time) * Time.deltaTime * speed, 0.0f, 0.0f) ;
    }
}
