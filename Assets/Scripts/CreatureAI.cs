using UnityEngine;
using System.Collections;

public class CreatureAI : MonoBehaviour {

    public Transform target; //プレイヤーの位置
    public float goalRange;
    public float playerRange;
    static Vector3 pos;
    NavMeshAgent agent;
    private Animator anim;

    float agentToPatroldistance;
    float agentToTargetdistance;

    //   void Awake() {
    //       agent = GetComponent<NavMeshAgent>();
    //   }

    // Use this for initialization
    void Start() {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        anim.SetBool("Run", false);
        DoPatrol();

    }
        
    public void EnemyMove() {
        //Agentと目的地の距離
        agentToPatroldistance = Vector3.Distance(this.agent.transform.position, pos);
        // Debug.Log(agentToPatroldistance + "目的地：" + pos + "敵位置" + this.agent.transform.position);

        //Agentとプレイヤーの距離
        agentToTargetdistance = Vector3.Distance(this.agent.transform.position, target.transform.position);


        //agentと目的地の距離が15f以下になると次の目的地をランダム指定
        if (agentToPatroldistance < goalRange) {
            DoPatrol();
        }

        //プレイヤーとagentの距離が15f以下になると次の目的地をランダム指定
        if (agentToTargetdistance < playerRange) {
            //            Debug.Log("プレイヤーを追いかけとるよ" + agentToTargetdistance);
            anim.SetBool("run", true);
            agent.speed = 15f;
            DoTracking();
        }
        else {
            anim.SetBool("run", false);
            agent.speed = 4f;
        }
    }


    //エージェントが向かう先をランダムに指定するメソッド
    public void DoPatrol() {
        float x = Random.Range(-10.0f, 10.0f);
        float z = Random.Range(-10.0f, 10.0f);
        pos = new Vector3(x, 1.1f, z);
        agent.SetDestination(pos);

    }

    //targetに指定したplayerを追いかけるメソッド
    public void DoTracking() {
        pos = target.position;
        agent.SetDestination(pos);

    }

    //クリア時に破壊する
    public void Destroy() {
        Destroy(gameObject);
    }

    //エネミーに衝突したら終了
    void OnTriggerEnter(Collider colobj) {
        if (colobj.gameObject.tag == "Player") {
            Application.LoadLevel("GameOver");
        }
    }



    /**
 * 円の範囲に入ったときのハンドラ
    
    void OnTriggerEnter(Collider colobj) {
        //円に入ったのがPlayerのタグがついているオブジェクトなら
        if (colobj.tag == "Player") {
            Debug.Log("範囲に入った！！");
            DoTracking();
        }

    }

    /**
     * 円の範囲外に出た時のハンドラ
    
    void OnTriggerExit(Collider colobj) {
        //出たのがPlayerタグがついているオブジェクトなら
        if (colobj.tag == "Player") {
            Debug.Log("範囲から出た！！");

            DoPatrol();
        }
    }
    */
}
