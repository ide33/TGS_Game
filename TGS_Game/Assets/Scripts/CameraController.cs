using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float scrollSpeed = 2.0f; // カメラのスクロール速度を公開変数として定義し、初期値を2.0fに設定

    private Vector3 initialPosition;  //カメラの初期位置を保存

    void Start()
    {
        //カメラの初期位置を記録
        initialPosition = transform.position;
    }

    void Update() // 毎フレーム呼び出されるUpdateメソッドを定義
    {
        transform.Translate(Vector2.right * scrollSpeed * Time.deltaTime); // カメラの位置を右方向にscrollSpeedの速度で移動させる
    }
    
    //プレイヤーがリスポーンしたときに呼び出すメソッド
    public void ResetCameraPosition()
    {
        //カメラを初期位置に戻す
        transform.position = initialPosition;
    }
}
