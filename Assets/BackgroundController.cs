using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour {

    SpriteRenderer sr;
	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();
	}

    bool acendendo = true;
	
	// Update is called once per frame
	void Update () {
        if (acendendo) {
            if(sr.color.a < 1) {
                Color c = sr.color;
                c.a += 0.001f;
                sr.color = c;
            }
            else {
                acendendo = false;
            }
        }
        else {
            if (sr.color.a > 0) {
                Color c = sr.color;
                c.a -= 0.001f;
                sr.color = c;
            }
            else {
                acendendo = true;
            }
        }
	}
}
