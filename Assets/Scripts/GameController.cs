using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class GameController : MonoBehaviour {

    public PlayerController player;
    public CreatureAI creature;
    public Text clearText;
    public Text startText;
    public GameObject coins;

    // Use this for initialization
    void Start () {
        //クリア時のテキストを取得
        clearText.text = GameObject.FindGameObjectWithTag("Clear").GetComponent<Text>().text;
        //スタート時のテキストを取得
        startText.text = GameObject.FindGameObjectWithTag("Text").GetComponent<Text>().text;
        //テキストはクリアするまで非表示
        clearText.enabled = false;
//        startText.enabled = false;
        //ステージ内にランダムにコインを3枚配置
        for (int i = 0; i < 3; i++) {
            Instantiate(this.coins, new Vector3(Random.Range(-25.0f, 25.0f), 1.8f, Random.Range(-25.0f, 25.0f)), Quaternion.identity);
        }
        StartCoroutine("coRoutine");
    }

    //スタート時のみ、説明のテキストを表示。2秒で消える。
    IEnumerator coRoutine() {
        enabled = false;
        startText.enabled = true;
        yield return new WaitForSeconds(2f);
        startText.enabled = false;
        enabled = true;
    }

    // Update is called once per frame
    void Update() {
        player.playerMove_1rdParson();

        creature.EnemyMove();

        if (player.DisplayClearText()) {
            creature.Destroy();
            clearText.enabled = true;
            enabled = false;
            //2秒後にreturnToTitleを呼び出す
            Invoke("NextStage", 2.0f);
        }        
    }

    void NextStage() {
        Application.LoadLevel("NextStage");
    }
}
