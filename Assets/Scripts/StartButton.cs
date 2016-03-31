using UnityEngine;
using System.Collections;

public class StartButton : MonoBehaviour {

    public void ButtonPush() {
        Debug.Log("ぼたんぷっしゅ");
        Application.LoadLevel("Opening");
    }
}
