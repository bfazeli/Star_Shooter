using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public GameObject[] hazards;
    public Vector3 spawnValue;
    public Text scoreText, restartText, gameOverText;

    public int hazardCount;
    public float startWait, spawnWait, waveWait;

    private int score;
    private bool gameOver, restart;

	private void Start()
	{
        score = 0;
        restart = gameOver = false;
        gameOverText.text = restartText.text = "";
        UpdateScore();
        StartCoroutine(SpawnWaves());
	}

	private void Update()
	{
        // If player chooses to restart the game, check to confirm
        // that the user did indeed press the R key
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(0);
            }
        }
    }

	/*
     * SpawnWaves:  
     *      Creates as many tumbling Asteroids based on the hazardCount
     *      from a random horizontal pt that enters the scene 
     *      outside of the game area.
     *
     */
	IEnumerator SpawnWaves() {
        
        yield return new WaitForSeconds(startWait);
        while (!gameOver)
        {
            for (int i = 0; i < hazardCount; ++i)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValue.x, spawnValue.x), spawnValue.y, spawnValue.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
        }

        // After the game is over, give the player an option to restart.
        restartText.text = "Press 'R' for Restart";
        restart = true;
    }

    void UpdateScore() {
        scoreText.text = "Score: " + score;
    }

    public void AddScore(int newScoreValue) {
        score += newScoreValue;
        UpdateScore();
    }

    public void GameOver() {
        gameOverText.text = "Game Over";
        gameOver = true;
    }
}
