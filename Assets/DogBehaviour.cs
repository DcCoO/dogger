using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogBehaviour : MonoBehaviour {


    float Ly0, Ly1;
    float y0 = 0, y1 = 2f;  //limite db
    Transform t;
    Vector2 initialPos;
    public Spawner spawner;

    int score;
    float inicio;

    void Start () {
        t = transform;
        Ly0 = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 10)).y;
        Ly1 = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 10)).y;
        initialPos = t.position;
    }

    //A <= 627.27
    //B <= 558.84
    //C <= 527.47
    //D <= 469.92
    //E <= 418.65
    //F <= 395.16
    //G <= 352.04
    //A <= 313.64
    //B <= 279.42
    //C <= 263.74
    //D <= 234.96
    int i = 0;
    float[] dbs = new float[20];
    int limit = 20;

    // Update is called once per frame
    void Update () {

        dbs[i++] = MicInput.MicLoudness;

        i = (i + 1) % limit;
        t.position = new Vector2(t.position.x, position(media()));
        
        
	}

    float media() {
        float m = 0;
        for (int i = 0; i < dbs.Length; i++) m = Mathf.Max(m, dbs[i]);
        return m;
    }

    float position(float db) {
        return Ly0 - (Ly0 - Ly1) * (db);
            
    }

    public void Begin() {
        inicio = Time.time;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().angularVelocity = 0;
        t.position = initialPos;
        t.eulerAngles = Vector3.zero;
        GameObject[] gs = GameObject.FindGameObjectsWithTag("cow");
        for(int i = 0; i < gs.Length; i++) {
            Destroy(gs[i]);
        }
        spawner.StartSpawn();
    }
    

    private void OnCollisionEnter2D(Collision2D collision) {
        print("MORREU!");
        score = (int)(Time.time - inicio);
        if(score > PlayerPrefs.GetInt("HighScore")) {
            PlayerPrefs.SetInt("HighScore", score);
        }
        spawner.StopSpawn();
        Interface.instance.GameOver(score);
       
    }
}
