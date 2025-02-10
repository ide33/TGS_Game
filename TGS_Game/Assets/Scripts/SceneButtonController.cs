using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneButtonController : MonoBehaviour
{
    void Start()
    {
        Button button = GetComponent<Button>();
        if(button != null)
        {
            button.onClick.AddListener(OnButtonClick);
        }
    }

    void OnButtonClick()
    {
        // スタートボタンが押されたら
        if(CompareTag("Start"))
        {
            SceneManager.LoadScene("GameScene");
        }

        // クリアボタンが押されたら
        if(CompareTag("Clear"))
        {
            SceneManager.LoadScene("MenuScene");
        }

        // ゲームオーバーボタンが押されたら
        if(CompareTag("GameOver"))
        {
            SceneManager.LoadScene("MenuScene");
        }

        // ノーマルボタンが押されたら
        if(CompareTag("Normal"))
        {
            SceneManager.LoadScene("GameStartScene");
        }
    }
}
