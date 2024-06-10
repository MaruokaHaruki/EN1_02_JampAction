using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullingJump : MonoBehaviour {

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start() {
        rb = gameObject.GetComponent<Rigidbody>();

        //NOTE:以下のコードで重力をいじることができる
        //Physics.gravity = new Vector3(0, -9.8f, 0);
    }


    private Vector3 clickPosition;
    [SerializeField]
    private float jumpPower = 10.0f;
    //ジャンプ可能か?
    bool isCanJamp = false;

    // Update is called once per frame
    void Update() {
        //`押したときの座標の取得
        if (Input.GetMouseButtonDown(0)) {
            clickPosition = Input.mousePosition;
        }

        //離したときの座標
        if (isCanJamp && Input.GetMouseButtonUp(0)) {
            //クリックした座標と話な座標の差分を取得
            Vector3 dist = clickPosition - Input.mousePosition;
            //クリックとリリースが同じ座標ならば無視
            if (dist.sqrMagnitude == 0) { return; }
            //差分を標準化し、JumpPowerをかけ合わせた値を移動量とする
            rb.velocity = dist.normalized * jumpPower;
        }

    }


    private void OnCollisionEnter(Collision collision) {
        Debug.Log("衝突した");
    }


    private void OnCollisionStay(Collision collision) {
        Debug.Log("接触中");

        //衝突している点の情報が複数収納されている
        ContactPoint[] contscts = collision.contacts;
        //0番目の衝突情報から、衝突している点の法線を取得
        Vector3 othernormal = contscts[0].normal;
        //上方向をしますベクトル
        Vector3 upVector = new Vector3(0, 1, 0);
        //上方向と法線の内積。２つのベクトルはともに長さが1なので、Cosθの結果があdotUN変数に入る。
        float dotUN = Vector3.Dot(upVector, othernormal);
        //内積値に逆三角arccosをかけて角度を算出。それを度数法へと変換する。これで角度が算出でできた。
        float dotDeg = Mathf.Acos(dotUN) * Mathf.Deg2Rad;
        //2つのベクトルがなす角度が45度より小さければ再びジャンプ可能とする
        if(dotDeg <= 45) {
            isCanJamp=true;
        }

    }


    private void OnCollisionExit(Collision collision) {
        Debug.Log("離脱した");
        isCanJamp = false;
    }






}
