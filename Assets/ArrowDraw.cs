using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//画像描画クラス
using UnityEngine.UI;

public class ArrowDraw : MonoBehaviour {

    [SerializeField]
    private Image arrowImage;
    private Vector3 clickPosition;


    // Start is called before the first frame update
    void Start() {

    }



    // Update is called once per frame
    void Update() {
        //`押したときの座標の取得
        if (Input.GetMouseButtonDown(0)) {
            clickPosition = Input.mousePosition;
            // 矢印の画像をクリックした場所に移動
            arrowImage.rectTransform.position = clickPosition;
            arrowImage.gameObject.SetActive(true);
        }

        //離したときの座標
        if (Input.GetMouseButtonUp(0)) {
            arrowImage.gameObject.SetActive(false);
        }

        if (arrowImage.gameObject.activeSelf) {
            Vector3 dist = clickPosition - Input.mousePosition;
            //ベクトルの長さを抽出
            float size = dist.magnitude;
            //ベクトルから角度(弧度法)を算出
            float angleRad = Mathf.Atan2(dist.y, dist.x);
            //矢印の画像をクリック下場所に画像を移動
            arrowImage.rectTransform.position = clickPosition;
            //矢印の画像をベクトルから算出した角度を度数法に変換してZ軸回転
            arrowImage.rectTransform.rotation = Quaternion.Euler(0, 0, angleRad * Mathf.Rad2Deg);
            //矢印の大きさをドラッグした距離に合わせる
            arrowImage.rectTransform.sizeDelta = new Vector2(size, size);

            Debug.Log(dist);
        }


    }






}
