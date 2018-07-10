using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {


    public GameObject[] obstacles;
    public Obstacle atual;
    public Transform canvas;
    public bool stop = false;
    public Transform dog;

    void Start() {
        dog = DogBehaviour.instance.transform;
        canvas = GameObject.Find("Canvas").transform;
        StartCoroutine(StartSpawn());
    }

    void Update () {
	}

    IEnumerator StartSpawn() {
        while (!stop) {

            GameObject g = Instantiate(obstacles[Random.Range(0, obstacles.Length)], canvas);
            atual = g.GetComponent<Obstacle>();

            yield return new WaitUntil(() => atual.over);
            print("Destruir " + g.name);
            Destroy(g);

            float time = 1f;
            float elapsedTime = 0;
            while (elapsedTime < time) {
                dog.transform.position = Vector2.Lerp(
                    dog.transform.position,
                    new Vector2(dog.transform.position.x, Position.Instance.y[0]),
                    (elapsedTime / time)
                );
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            //yield return new WaitForSeconds(1);
            Time.timeScale += 0.1f;
        }
    }

    
    
}
