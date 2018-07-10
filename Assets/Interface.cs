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

    public GameObject spawner;
    GameObject spawnerOnline;
    int atualScore;
	// Use this for initialization
	void Start () {
        instance = this;
        Time.timeScale = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    bool playing = false;
    IEnumerator setScore() {
        playing = true;
        float time = Time.time;
        yield return new WaitUntil(() => !playing);

        time = Time.time - time;
        score.text = "Score: " + ((int) time).ToString();
        int high = PlayerPrefs.GetInt("HighScore", 0);
        PlayerPrefs.SetInt("HighScore", Mathf.Max(high, atualScore));

    }

    public void StartGame() {
        StartCoroutine(setScore());
        atualScore = 0;
        GameObject[] gs = GameObject.FindGameObjectsWithTag("kill");
        GameObject[] gx = GameObject.FindGameObjectsWithTag("obstacle");
        for (int i = 0; i < gs.Length; i++) {
            Destroy(gs[i]);
        }
        for (int i = 0; i < gx.Length; i++) {
            Destroy(gx[i]);
        }
        spawnerOnline = Instantiate(spawner, Vector3.zero, Quaternion.identity);
        menu.SetActive(false);
        Time.timeScale = 1;
        print("Started game!");
        DogBehaviour.instance.transform.position += new Vector3(0, -DogBehaviour.instance.transform.position.y + Position.Instance.y[0]);
    }

    public void AddPoint() {
        atualScore++;
    }

    public void GameOver() {
        print("Game over!");
        playing = false;

        GameObject[] gs = GameObject.FindGameObjectsWithTag("kill");
        for (int i = 0; i < gs.Length; i++) Destroy(gs[i]);

        Time.timeScale = 0;
        gameOver.SetActive(true);
        Destroy(spawnerOnline);
        atualScore = 0;
    }

    public void Menu() {
        highscore.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0).ToString();
        gameOver.SetActive(false);
        menu.SetActive(true);
    }
}
