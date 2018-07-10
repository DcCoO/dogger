using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class JumpObstacle : Obstacle {

    public GameObject stonePrefab;
    public Image bar;
    public UILayout marker;
    public bool subindo = true;
    public float speed;
    public float stoneSpeed;

    public UILayout[] numbers;

   

    GameObject dog;

    GameObject stoneRef;
    int espacoLivre;

    void Start () {
        dog = DogBehaviour.instance.gameObject;
        Time.timeScale = 1;
        //posicionando numeros
        int[] p = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        for(int i = 0; i < 4; ) {
            int r = Random.Range(0, 10);
            if(p[r] == 1) {
                numbers[i].position = new Vector2(numbers[i].position.x, 0.05f + ((float) r) * 0.09f);
                p[r] = 0;
                i++;
            }
        }

        //posicionando pedras
        int[] q = { 1, 1, 1, 1, 1 };
        int size = Random.Range(1, 4);
        for (int i = 0; i < size;) {
            int r = Random.Range(0, 5);
            if (q[r] == 1) {
                GameObject g = Instantiate(stonePrefab, new Vector3(13.6f, Position.Instance.y[r], 0), Quaternion.identity);
                g.GetComponent<Rigidbody2D>().velocity = new Vector3(-stoneSpeed, 0, 0);
                q[r] = 0;
                if (i == 0) stoneRef = g;
                i++;                
            }
        }
        
	}

    bool fala() {
        return MicInput.MicLoudness * 1000f > 10f;
    }

    bool pulou = false;
    int n = -1;
    bool stopUpdate = false;

	void Update () {
        if (stopUpdate) return;

        if (stoneRef.transform.position.x < dog.transform.position.x) {
            Destroy(bar.gameObject);
            Destroy(marker.gameObject);
            for (int i = 0; i < 4; i++) Destroy(numbers[i].gameObject);
            print("saiu");
            over = true;
            stopUpdate = true;
            return;
        }

        if (subindo) {
            marker.position += new Vector2(0, speed);
            if (marker.position.y > 0.95) subindo = false;
        }
        else {
            marker.position -= new Vector2(0, speed);
            if (marker.position.y < 0.05) subindo = true;
        }

        if (!pulou && fala()) {
            pulou = true;
            
            float minDist = 9999;
            int minIndex = 0;
            for (int i = 0; i < 4; i++) {
                float dist = Mathf.Abs(marker.position.y - numbers[i].position.y);
                print("dist(marker, n" + (i + 1) + ") = " + dist);
                if(dist < minDist) {
                    minIndex = i;
                    minDist = dist;
                }
            }

            if (minDist < 0.025f) {
                n = minIndex;
            }
            if (n != -1) {
                
                numbers[n].gameObject.GetComponent<Image>().color = Color.yellow;
                StartCoroutine(pular());
                stopUpdate = true;
                for (int i = 0; i < 4; i++)
                    if (i != n) numbers[i].gameObject.SetActive(false);
            }
            else {
                for(int i = 0; i < 4; i++) numbers[i].gameObject.SetActive(false);
            }
            bar.gameObject.SetActive(false);
            marker.gameObject.SetActive(false);
        }

    }

    bool distToRef() {
        print("distancia esta " + Mathf.Abs(dog.transform.position.x - stoneRef.transform.position.x));
        return Mathf.Abs(dog.transform.position.x - stoneRef.transform.position.x) < 5;
    }

    IEnumerator pular() {
        yield return new WaitUntil( () => distToRef());

        Vector2 origin = dog.transform.position;
        float time = 1f;
        float elapsedTime = 0;
        while (elapsedTime < time) {
            dog.transform.position = Vector2.Lerp(
                dog.transform.position,
                new Vector2(dog.transform.position.x, Position.Instance.y[1 + n]),
                (elapsedTime/time)
            );
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(1.5f);

        elapsedTime = 0;
        while (elapsedTime < time) {
            dog.transform.position = Vector2.Lerp(
                dog.transform.position,
                origin,
                (elapsedTime / time)
            );
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(bar.gameObject);
        Destroy(marker.gameObject);
        for (int i = 0; i < 4; i++) Destroy(numbers[i].gameObject);
        print("saiu");
        over = true;

    }

}
