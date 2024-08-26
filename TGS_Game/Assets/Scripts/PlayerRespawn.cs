using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public Vector3 respawnPosition;  // リスポーン位置
    public int respawnDamage = 20;   // リスポーン時のダメージ量
    private PlayerHealth playerHealth;  // プレイヤーのHP管理スクリプトへの参照
    public Transform respawnPoint;    // リスポーン地点
    private StageGenerator stageGenerator;  // ステージ生成スクリプトの参照
    public Transform player;           // プレイヤーのTransform
    public float updateDistance = 50f; // リスポーン地点を更新する距離
    private Vector3 currentCheckpoint; // 現在のリスポーン地点

    void Start()
    {
        // ゲーム開始時にリスポーン位置を現在の位置に設定
        respawnPosition = transform.position;

        // PlayerHealthスクリプトへの参照を取得
        playerHealth = GetComponent<PlayerHealth>();

        // StageGeneratorスクリプトを探して参照を保持
        stageGenerator = FindObjectOfType<StageGenerator>();

        // 初期リスポーン地点をプレイヤーの開始地点に設定
        currentCheckpoint = player.position;
    }

    // トリガーが他のColliderと接触した際に呼ばれる
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 川に接触した場合の処理
        if (other.gameObject.CompareTag("River"))
        {
            Respawn();
            // 敵をリスポーン
            if (stageGenerator != null)
            {
                stageGenerator.RespawnEnemies();
            }

            RespawnPlayer();


            //カメラの位置をリセット
            Camera.main.GetComponent<CameraController>().ResetCameraPosition();
        }
    }

    void Update()
    {
        // プレイヤーの進行距離を確認
        float distanceTraveled = player.position.x - currentCheckpoint.x;

        // 一定距離進んだらリスポーン地点を更新
        if (distanceTraveled >= updateDistance)
        {
            UpdateCheckpoint();
        }
    }

    // リスポーン地点を更新する関数
    void UpdateCheckpoint()
    {
        currentCheckpoint = player.position;
        Debug.Log("Checkpoint updated to: " + currentCheckpoint);
    }

    // プレイヤーをリスポーンさせる関数
    public void RespawnPlayer()
    {
        player.position = currentCheckpoint;
        Debug.Log("Player respawned at: " + currentCheckpoint);
    }

    // リスポーン位置にプレイヤーを移動させ、HPを減らす
    void Respawn()
    {
        transform.position = respawnPosition;
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(respawnDamage);
        }
    }
}
