using UnityEngine;
using System.Collections;
using UnityStandardAssets.Utility;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]

public class PlayerController : MonoBehaviour {

    [SerializeField] private CurveControlledBob HeadBob = new CurveControlledBob();
    [SerializeField] private float StepInterval;
    [SerializeField] private AudioClip[] FootstepSounds;    // an array of footstep sounds that will be randomly selected from.
    public float speed;
    private Camera Camera;
    private Vector3 OriginalCameraPosition;
    private CharacterController charaCon;		// キャラクターコンポーネント用の変数
    private Vector3 move = Vector3.zero;
    private const float gravity = 9.8f;
    private float rotateSpeed = 90f;
    private int getCoin = 0;
    private float stepCycle;
    private float nextStep;
    private AudioSource audioSource;

    public bool DisplayClearText() {
        //コインを3枚集めたらクリア
        if (getCoin == 3) {
            //クリアテキスト表示
           return  true;
        }
        return false;
    }

    // Use this for initialization
    void Start() {
        charaCon = GetComponent<CharacterController>();
        Camera = Camera.main;
        OriginalCameraPosition = Camera.transform.localPosition;
        HeadBob.Setup(Camera, StepInterval);
        stepCycle = 0f;
        nextStep = stepCycle / 2f;
        audioSource = GetComponent<AudioSource>();

    }

    // ■■■１人称視点の移動■■■
    public void playerMove_1rdParson() {
        // ▼▼▼移動量の取得▼▼▼
        float y = move.y;
        // 上下のキー入力を取得し、移動量に代入.
        move = new Vector3(0f, 0f, Input.GetAxis("Vertical") * speed);
        move = transform.TransformDirection(move);
        //重力を与える
        move.y += y;
        move.y -= gravity * Time.deltaTime;


        // ▼▼▼プレイヤーの向き変更▼▼▼
        Vector3 playerDir = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        playerDir = transform.TransformDirection(playerDir);
        if (playerDir.magnitude > 0.1f) {
            Quaternion q = Quaternion.LookRotation(playerDir);          // 向きたい方角をQuaternionn型に直す .
            // 向きを q に向けてじわ～っと変化させる.
            transform.rotation = Quaternion.RotateTowards(transform.rotation, q, rotateSpeed * Time.deltaTime);

        }

        // ▼▼▼移動処理▼▼▼
        charaCon.Move(move * Time.deltaTime);   // プレイヤー移動.
        ProgressStepCycle(speed);
        UpdateCameraPosition();

    }
    private void ProgressStepCycle(float speed) {
        if (charaCon.velocity.sqrMagnitude > 0 ) {
            stepCycle += 10f * Time.fixedDeltaTime;
        }

        if (!(stepCycle > nextStep)) {
            return;
        }

        nextStep = stepCycle + StepInterval;

        PlayFootStepAudio();
    }

    private void PlayFootStepAudio() {
        if (!charaCon.isGrounded) {
            return;
        }
        // pick & play a random footstep sound from the array,
        // excluding sound at index 0
        int n = Random.Range(1, FootstepSounds.Length);
        audioSource.clip = FootstepSounds[n];
        audioSource.PlayOneShot(audioSource.clip);
        // move picked sound to index 0 so it's not picked next time
        FootstepSounds[n] = FootstepSounds[0];
        FootstepSounds[0] = audioSource.clip;
    }


    void OnTriggerEnter(Collider colobj) {
        Debug.Log("当たってるんやで");
        //円に入ったのがコインならコインゲット
        if (colobj.gameObject.tag == "Coin") {
            Debug.Log("Coinゲット");
            getCoin++;
        }
    }

    void UpdateCameraPosition() {

        Vector3 newCameraPosition;
        if (charaCon.velocity.magnitude > 0 ) {
            Camera.transform.localPosition =
                HeadBob.DoHeadBob(5f);
            newCameraPosition = Camera.transform.localPosition;
            newCameraPosition.y = Camera.transform.localPosition.y;
        }
        else {
            newCameraPosition = Camera.transform.localPosition;
            newCameraPosition.y = 0.8f;

        }
        Camera.transform.localPosition = newCameraPosition;

    }


}