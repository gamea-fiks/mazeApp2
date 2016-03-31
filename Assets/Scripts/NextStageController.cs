using UnityEngine;
using System.Collections;

public class NextStageController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //2秒後に次ステージへ
        Invoke("NextStage", 2.0f);
    }

    void NextStage() {
        Application.LoadLevel("Stage2");
    }


}
