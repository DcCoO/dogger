using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaiseObstacle : Obstacle {

    public GameObject bar;
    public GameObject stone;
    public float speed;
    SpriteRenderer sr;
	// Use this for initialization
	void Start () {
        stone = Instantiate(stone, new Vector2(14f, -3.279999f), Quaternion.identity);
        bar = Instantiate(bar, new Vector2(14f, -4.809998f), Quaternion.identity);
        bar.GetComponent<Rigidbody2D>().velocity = new Vector3(-speed, 0, 0);
        stone.GetComponent<Rigidbody2D>().velocity = new Vector3(-speed, 0, 0);
        sr = bar.GetComponent<SpriteRenderer>();
    }

    bool finish = false;

    // Update is called once per frame
    void Update () {
        if (finish) return;
        if (!stone) { StartCoroutine(turnoff()); return; }
        if (DogBehaviour.instance.transform.position.x > stone.transform.position.x) StartCoroutine(turnoff());
        Vector2 targetScale = new Vector2(bar.transform.localScale.x, Mathf.Max(0.12f, 8 * MicInput.MicLoudness));
        bar.transform.localScale = Vector2.Lerp(
            bar.transform.localScale,
            targetScale,
            bar.transform.localScale.y > targetScale.y ? 1 : 0.003f
        );
        stone.transform.position = new Vector2(bar.transform.position.x, stone.transform.position.y);

        Color c = new Color(bar.transform.localScale.y, 1 - bar.transform.localScale.y, 0);
        sr.color = c;

	}

    IEnumerator turnoff() {
        finish = true;
        yield return new WaitForSeconds(3);
        over = true;
    }
}
