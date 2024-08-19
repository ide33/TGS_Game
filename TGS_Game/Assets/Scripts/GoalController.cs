using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // プレイヤーにタグを設定し、タグを確認する
        if (other.CompareTag("Player"))
        {
            // クリア画面のシーンに移行する
            SceneManager.LoadScene("GameClearScene");
        }
    }
}
