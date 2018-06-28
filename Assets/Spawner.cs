using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject cow;
    Vector3 initialPos;
    float time = 5;
    float slice = 0.005f;
    float speed = 4;
    float x;
    public bool stop = false;
    bool canSpawn = false;
    private void Start() {
        initialPos = transform.position;
        x = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width + 50, 0, 10)).x;
    }
    // Update is called once per frame
    void Update () {
        if (!stop && canSpawn) {
            StartCoroutine(spawn());
        }
	}

    public void StartSpawn() {
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

    IEnumerator spawn() {
        print("ENTROU\n");
        if (canSpawn) {
            canSpawn = false;
            if (time > 0.5f) time -= slice;
            GameObject g = Instantiate(cow, new Vector3(x, Position.Instance.y[Random.Range(0, 5)]), Quaternion.identity);
            g.GetComponent<MoveCow>().Move(speed);
            speed += slice;
            yield return new WaitForSeconds(time);
            canSpawn = true;
        }
        
    }
}
