using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCow : MonoBehaviour {

    private void Start() {
        Destroy(gameObject, 20);
    }

    public void Move(float speed) {
        GetComponent<Rigidbody2D>().velocity = new Vector3(-speed, 0, 0);
    }
}
