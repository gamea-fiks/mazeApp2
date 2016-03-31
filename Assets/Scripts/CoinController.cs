using UnityEngine;
using System.Collections;

public class CoinController : MonoBehaviour {

    void OnTriggerEnter(Collider colobj) {
        Debug.Log("当たってるんやで");

        //円に入ったのがPlayerのタグがついているオブジェクトなら
        if (colobj.gameObject.tag == "Player") {
            Debug.Log("playerに当たっとるよ");

            Destroy(gameObject);
        }

        //コインが壁と接触してたらもう一度ランダムに配置
        if (colobj.gameObject.tag == "Wall") {
            Debug.Log("壁にハイッチャッテルーので生成しなおすべ");
            Destroy(gameObject);
            Instantiate(this, new Vector3(Random.Range(-25.0f, 25.0f), 1.8f, Random.Range(-25.0f, 25.0f)), Quaternion.identity);
        }

    }
/*
    //注意：こちらは静止していても中にいる間は呼ばれ続ける 
    void OnTriggerStay(Collider col){
    }
*/

}
