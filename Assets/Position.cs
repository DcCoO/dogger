using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position {

    public float[] y;
    public float dif;

    private static Position instance = null;
    public static Position Instance {
        get {
            if (instance == null) instance = new Position();
            return instance;
        }
    }




    public Position() {
        float ymin = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 10)).y;
        float ymax = Camera.main.ScreenToWorldPoint(new Vector3(0, (float) Screen.height, 10)).y;
        //Debug.Log(Screen.height + "--" + ymin + " : " + ymax);
        dif = (ymax - ymin) / 5f;
        y = new float[5];
        y[0] = ymax - (dif / 2f);
        y[1] = y[0] - dif;
        y[2] = y[1] - dif;
        y[3] = y[2] - dif;
        y[4] = y[3] - dif;
    }
}
