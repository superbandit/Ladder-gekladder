using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public AudioClip jump;
    public GameObject bullet;
    public GameObject eButton;
    Rigidbody2D body;

    GameObject toPickUp;
    GameObject pickedUp;
    float pickedUpHeight;

    bool nextLvlActive = false;

    int facingSide = 1;
    int speed = 5;

	void Start ()
    {
        body = GetComponent<Rigidbody2D>();
	}

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if (pickedUp != null)
            {
                StopCoroutine(MovePickedUp());
                pickedUp.GetComponent<Rigidbody2D>().simulated = true;
                pickedUp = null;
            }
            else if (pickedUp == null && toPickUp != null)
            {
                pickedUp = toPickUp;
                pickedUp.GetComponent<Rigidbody2D>().simulated = false;
                pickedUpHeight = pickedUp.GetComponent<SpriteRenderer>().bounds.size.y / 2 - 0.48f;
                StartCoroutine(MovePickedUp());
            }
            else if (nextLvlActive)
            {
                GameHandler.Instance.NextLevel();
            }
        }
        if(Input.GetAxisRaw("Horizontal") > 0)
        {
            facingSide = 1;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            facingSide = -1;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject newBullet = Instantiate(bullet, new Vector2(transform.position.x + facingSide / 1.7f , transform.position.y), Quaternion.identity);
            newBullet.GetComponent<Bullet>().side = facingSide;
        }
    }

    void FixedUpdate ()
    {
        body.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, body.velocity.y);

        RaycastHit2D hitA = Physics2D.Raycast(new Vector2(transform.position.x - 0.4f, transform.position.y -0.51f), Vector2.down);
        RaycastHit2D hitB = Physics2D.Raycast(new Vector2(transform.position.x + 0.4f, transform.position.y - 0.51f), Vector2.down);
        if (hitA.distance < 0.03f || hitB.distance < 0.03f)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                SoundHandler.Instance.PlaySound(jump);
                body.AddForce(new Vector2(0, 350));
            }
        }
	}
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Movable")
        {
            toPickUp = col.gameObject;
            eButton.SetActive(true);
        }
        if (col.tag == "NextLvl")
        {
            nextLvlActive = true;
            eButton.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        toPickUp = null;
        nextLvlActive = false;
        eButton.SetActive(false);
    }

    IEnumerator MovePickedUp()
    {
        while(pickedUp != null)
        {
            pickedUp.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + pickedUpHeight);
            yield return null;
        }
    }
}
