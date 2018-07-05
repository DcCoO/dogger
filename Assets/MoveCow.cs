using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCow : MonoBehaviour {

    bool callback = false;
    Transform t;
    private void Start() {
        Destroy(gameObject, 20);
    }

    void Update() {
        if (callback) {
            if (t.position.x < DogBehaviour.instance.t.position.x) {
                Interface.instance.AddPoint();
                print("Point!");
                callback = false;
            }
        }
    }

    public void Move(float speed) {
        GetComponent<Rigidbody2D>().velocity = new Vector3(-speed, 0, 0);
    }

    public void Represent() {
        callback = true;
        t = transform;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        DogBehaviour.instance.StartPowerUp();
        Destroy(gameObject);
    }


}
