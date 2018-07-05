using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interface : MonoBehaviour {
    
    public static Interface instance;

    public GameObject menu;
    public GameObject gameOver;

    public Text highscore;
    public Text score;

    public Spawner spawner;
    int atualScore;
	// Use this for initialization
	void Start () {
        instance = this;
        Time.timeScale = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartGame() {
        atualScore = 0;
        GameObject[] gs = GameObject.FindGameObjectsWithTag("cow");
        for (int i = 0; i < gs.Length; i++) {
            Destroy(gs[i]);
        }
        menu.SetActive(false);
        Time.timeScale = 1;
        spawner.StartSpawn();
        print("Started game!");
    }

    public void AddPoint() {
        atualScore++;
    }

    public void GameOver() {
        print("Game over!");
        Time.timeScale = 0;
        gameOver.SetActive(true);
        this.score.text = "Score: " + atualScore.ToString();
        int high = PlayerPrefs.GetInt("HighScore", 0);
        PlayerPrefs.SetInt("HighScore", Mathf.Max(high, atualScore));
        atualScore = 0;
    }

    public void Menu() {
        highscore.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0).ToString();
        gameOver.SetActive(false);
        menu.SetActive(true);
    }
}
