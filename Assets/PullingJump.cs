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


    private void InCollisionEnter(Collision collision) {
        Debug.Log("衝突した");
    }


    private void OnCollisionStay(Collision collision) {
        Debug.Log("接触中");
        isCanJamp = true;
    }


    private void OnCollisionExit(Collision collision) {
        Debug.Log("離脱した");
        isCanJamp = false;
    }






}
