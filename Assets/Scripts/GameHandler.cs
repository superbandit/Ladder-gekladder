using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    public static GameHandler Instance;

    public Player player;

    private bool removeEnemies = false;
    private GameObject[] enemies; 

	void Awake ()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {

    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (removeEnemies == true)
        {
            RemoveEnemies();
        }
    }


    // Update is called once per frame
    void Update ()
    {
		// check dem cheats
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                removeEnemies = !removeEnemies;
                RemoveEnemies();
            }

            if (Input.GetKeyDown(KeyCode.O))
            {
                player.invincible = !player.invincible;
            }

            if (Input.GetKeyDown(KeyCode.KeypadMinus) || Input.GetKeyDown(KeyCode.Minus))
            {
                if (player.speed > 0)
                    player.speed -= 1;
            }

            if (Input.GetKeyDown(KeyCode.KeypadPlus) || Input.GetKeyDown(KeyCode.Equals))
            {
                if (player.speed < 10)
                    player.speed += 1;
            }
        }
	}

    private void RemoveEnemies()
    {
        foreach(GameObject e in enemies)
        {
            Destroy(e);
        }
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
