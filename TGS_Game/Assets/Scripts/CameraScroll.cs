using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScroll : MonoBehaviour
{
    public float scrollSpeed = 2.0f; // カメラのスクロール速度を公開変数として定義し、初期値を2.0fに設定

    void Update() // 毎フレーム呼び出されるUpdateメソッドを定義
    {
        transform.Translate(Vector2.right * scrollSpeed * Time.deltaTime); // カメラの位置を右方向にscrollSpeedの速度で移動させる
    }
}
