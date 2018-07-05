using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject cow;
    public GameObject powerup;
    Vector3 initialPos;
    float time = 4;
    float slice = 0.1f;
    float speed = 4;
    float x;

    public bool stop = false;
    bool canSpawn = false;

    

    private void Start() {
        initialPos = transform.position;
        x = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width + 50, 0, 10)).x;
    }

    void Update () {
        if (!stop) {
            if (canSpawn) {
                int qte = Random.Range(1, 5);
                for (int i = 0; i < qte; i++) {
                    GameObject g = Instantiate(cow, new Vector3(x, Position.Instance.y[4 - i]), Quaternion.identity);
                    g.GetComponent<MoveCow>().Move(speed);
                    if (i == 0) g.GetComponent<MoveCow>().Represent();
                }
                int power = Random.Range(1, 20);
                if(power == 1) {
                    GameObject g = Instantiate(powerup, new Vector3(x, Position.Instance.y[4 - qte]), Quaternion.identity);
                    g.GetComponent<MoveCow>().Move(speed);
                }
                canSpawn = false;
                time -= slice;
                time = Mathf.Max(time, 0.7f);
                if(time < 0.8f) {
                    speed = Mathf.Min(7, speed + slice);
                }
                StartCoroutine(wait());
            }
        }
	}

    public void StartSpawn() {
        time = 4;
        speed = 4;
        transform.position = initialPos;
        transform.eulerAngles = Vector3.zero;
        StopAllCoroutines();
        stop = false;
        canSpawn = true;
    }

    public void StopSpawn() {
        stop = true;
        canSpawn = false;
    }

    IEnumerator wait() {
        yield return new WaitForSeconds(time);
        canSpawn = true;        
    }
}
