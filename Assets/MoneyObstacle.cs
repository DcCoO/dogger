using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyObstacle : Obstacle {

    public UILayout marker;
    public float speed;
    Transform dog;

    public GameObject money;
    int qteMoney;
    public float moneySpeed;

    void Start () {
        qteMoney = Random.Range(10, 18);
        dog = DogBehaviour.instance.transform;
        StartCoroutine(spawn());
	}

    bool subindo = true;
    bool pulou = false;

    bool fala() {
        return MicInput.MicLoudness * 1000f > 30f;
    }

    void Update() {

        if (subindo) {
            marker.position += new Vector2(0, speed);
            if (marker.position.y > 0.95) subindo = false;
        }
        else {
            marker.position -= new Vector2(0, speed);
            if (marker.position.y < 0.05) subindo = true;
        }

        if (!pulou && fala()) {
            StartCoroutine(pular());
        }

    }

    IEnumerator pular() {
        if (!pulou) {
            pulou = true;
            print(marker.position.y);
            yield return null;            

            float y = Position.Instance.y[0] + marker.position.y * (Position.Instance.y[4] - Position.Instance.y[0]);
            
            float time = Mathf.Abs(dog.position.y - y)/(Position.Instance.y[4] - Position.Instance.y[0]);
            float elapsedTime = 0;
            while (elapsedTime < time) {
                dog.position = Vector2.Lerp(dog.position, new Vector2(dog.position.x, y), (elapsedTime / time));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            pulou = false;
        }
    }

    IEnumerator spawn() {
        GameObject last = new GameObject();
        for (int i = 0; i < qteMoney; i++) {
            GameObject g = Instantiate(
                money, 
                new Vector3(
                    13.6f, 
                    Random.Range(Position.Instance.y[0], Position.Instance.y[4]), 
                    0
                ), 
                Quaternion.identity);
            g.GetComponent<Rigidbody2D>().velocity = new Vector3(-moneySpeed, 0, 0);
            Destroy(g, 20);
            if(i != qteMoney - 1) yield return new WaitForSeconds(1.5f);
            if(i == qteMoney - 1) last = g;
        }
        yield return new WaitUntil(() => last == null);
        over = true;
    }
}
