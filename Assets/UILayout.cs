using UnityEngine.UI;
using UnityEngine;

[ExecuteInEditMode]
public class UILayout : MonoBehaviour {

    RectTransform rt;

    //proporcao do objeto em relacao a tela
    public Vector2 position;
    public Vector2 sizeRatio;

    void Start() {
        rt = GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(Screen.width * sizeRatio.x, Screen.height * sizeRatio.y);
        rt.anchoredPosition = new Vector2(
            (-Screen.width / 2) + (position.x * Screen.width),
            (-Screen.height / 2) + (position.y * Screen.height)
        );
    }
	
	void Update () {
        rt.sizeDelta = new Vector2(Screen.width * sizeRatio.x, Screen.height * sizeRatio.y);
        rt.anchoredPosition = new Vector2(
            (-Screen.width / 2) + (position.x * Screen.width),
            (-Screen.height / 2) + (position.y * Screen.height)
        );
    }
}
