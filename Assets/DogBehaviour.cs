using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DogBehaviour : MonoBehaviour {

    AudioSource source;

    [HideInInspector] public Transform t;
    public static DogBehaviour instance;

    int score;
    float inicio;

    bool imortal = false;
    SpriteRenderer sr;

    void Start () {
        instance = this;
        t = transform;
        sr = GetComponent<SpriteRenderer>();
        source = GetComponent<AudioSource>();
    }

    bool subindo;
    public float speed;
    public Image volume;
    float db, y, time;

    void Update () {

        if (imortal) {
            sr.enabled = !sr.enabled;
        }
        db = Mathf.Max(0, MicInput.MicLoudness);
        db = Mathf.Min(1, MicInput.MicLoudness);
        subindo = db >= 0.075f;

        volume.rectTransform.localScale = new Vector3(volume.rectTransform.localScale.x, db, volume.rectTransform.localScale.z);
        volume.color = new Color(1 - db, db, 0);
        
        y = t.position.y;

        if (subindo) {
            y = t.position.y + Time.deltaTime * (speed * (1 + db));
            y = Mathf.Min(y, Position.Instance.y[0]);
            time = 0;
        }
        else if(time > 0.1f){
            y = t.position.y - Time.deltaTime * speed;
            y = Mathf.Max(y, Position.Instance.y[4]);
        }
        time += Time.deltaTime;

        t.position = new Vector2(t.position.x, y);
        
	}

    public void StartPowerUp() {
        StartCoroutine(powerup());
        source.Play();
    }

    IEnumerator powerup() {
        imortal = true;
        GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(4);
        imortal = false;
        GetComponent<BoxCollider2D>().enabled = true;
        sr.enabled = true;
    }


    private void OnCollisionEnter2D() {
        Interface.instance.GameOver();
    }
}
