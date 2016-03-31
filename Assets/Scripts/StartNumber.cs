using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartNumber : MonoBehaviour {

    public Text NumText;
    private int num;

    void Start() {
        // コルーチンを設定
        StartCoroutine(loop());
    }

    private IEnumerator loop() {
        // ループ
        while (true) {
            // 1秒毎にループします
            yield return new WaitForSeconds(1f);
            onTimer();
        }
    }

    private void onTimer() {
        num = Random.Range(0, 9);
        // 1秒毎に呼ばれます
        NumText.text = num.ToString();
    }
}