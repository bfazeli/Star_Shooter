using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {

    public GameObject explosion;
    public GameObject playerExplosion;
    public int scoreValue;

    private GameObject theBomb;
    private GameController gameController;

	private void Start()
	{
        // Find and hook up a reference to a game controller
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (!gameControllerObject)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }

	private void OnTriggerEnter(Collider other)
	{
        if (other.tag == "Boundary")
        {
            return;
        }

        theBomb = Instantiate(explosion, transform.position, transform.rotation) as GameObject;

        if(other.tag == "Player") {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
        }

        gameController.AddScore(scoreValue);
        Destroy(other.gameObject);
        Destroy(gameObject);
	}
}
