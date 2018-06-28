using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interface : MonoBehaviour {
    
    public static Interface instance;

    public Image[] notes;
    public GameObject player;
    public GameObject menu;
    public GameObject gameOver;
    public Text highscore;
    public Text score;

    public int highScore;
	// Use this for initialization
	void Start () {
        instance = this;
        Time.timeScale = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartGame() {
        Time.timeScale = 1;
        player.GetComponent<DogBehaviour>().Begin();
        menu.SetActive(false);
    }

    public void GameOver(int score) {
        Time.timeScale = 0;
        gameOver.SetActive(true);
        this.score.text = "Score: " + score.ToString();
    }

    public void Menu() {
        highscore.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0).ToString();
        gameOver.SetActive(false);
        menu.SetActive(true);
    }
}
