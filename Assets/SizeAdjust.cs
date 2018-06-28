using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeAdjust : MonoBehaviour {

    public float scale;
	// Use this for initialization
	void Start () {
        

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        float ratio = Position.Instance.dif / sr.bounds.size.y;
        transform.localScale *= scale * ratio;
	}
    

}
