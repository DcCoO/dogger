using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityObstacle : Obstacle {

    public GameObject stone;
    public float stoneSpeed;
    int qteStone = 0;

    bool subindo = true;
    Transform dog;
    int estado = 0;
    //0 = esperando
    //1 = captando inicio
    //2 = capitando fim

    float volume;
    float begin;
    float end;

	// Use this for initialization
	void Start () {
        dog = DogBehaviour.instance.transform;
        qteStone = Random.Range(6, 12);
        StartCoroutine(spawn());
	}
	
	// Update is called once per frame
	void Update () {
        volume = 100 * MicInput.MicLoudness;
        //print(volume * 10);
        if (isTalking() && estado == 0) {
            StartCoroutine(passagem());
        }

    }


    bool isTalking() {
        return volume > 15f;
    }

    IEnumerator passagem() {
        if(estado == 0) {
            begin = 0; end = 100;
            estado = 1;
            yield return new WaitUntil(() => volume > 15f);
            begin = volume;
            print("\tBegin: " + begin);
            estado = 2;

            float wait = 0.3f;
            float elapsedWait = 0;

            while(elapsedWait < wait) {
                end = volume;
                if (subindo && end > begin) break;
                if (!subindo && end < begin) break;
                elapsedWait += Time.deltaTime;
                yield return null;
            }

            print("\tEnd:   " + end);
            estado = 3;

            if ((subindo && begin < end) || (!subindo && begin > end)) {
                int pos = subindo ? 4 : 0;
                float time = 1f;
                float elapsedTime = 0;
                while (elapsedTime < time) {
                    dog.transform.position = Vector2.Lerp(
                        dog.transform.position,
                        new Vector2(dog.transform.position.x, Position.Instance.y[pos]),
                        (elapsedTime / time)
                    );
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }
            }

            subindo = !subindo;
            estado = 0;
        }
    }

    IEnumerator spawn() {
        GameObject last = new GameObject();
        for(int i = 0; i < qteStone; i++) {
            int r = Random.Range(0, 2) == 1 ? 0 : 4;
            GameObject g = Instantiate(stone, new Vector3(13.6f, Position.Instance.y[r], 0), Quaternion.identity);
            g.GetComponent<Rigidbody2D>().velocity = new Vector3(-stoneSpeed, 0, 0);
            Destroy(g, 20);

            if(i != qteStone - 1) yield return new WaitForSeconds(1.5f);
            else { last = g; }
        }
        yield return new WaitUntil(() => last.transform.position.x < dog.position.x);
        over = true;
    }
}
