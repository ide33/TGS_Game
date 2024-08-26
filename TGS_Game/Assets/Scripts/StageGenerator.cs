using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGenerator : MonoBehaviour
{
    public GameObject player;  //プレイヤーオブジェクトの参照
    public float playerSpawnHeight = 3.0f;  //スポーンの高さ
    public GameObject groundPrefab;  //地面プレハブ
    public GameObject riverPrefab;  //川プレハブ
    public GameObject scaffoldPrefab;  //足場プレハブ
    public GameObject enemyPrefab;   // 敵プレハブ
    public int stageWigth = 100;  //ステージの幅
    public float groundHeight = -2.0f;  //地面の高さ
    public float riverHeight = 20.0f;  //川の高さ
    public float scaffoldHeight = 2.0f;  //足場の高さ
    public int safeZoneWidth = 10;   // 最初の安全ゾーンの幅
    public int enemyCount = 10;      // 敵の数
    public float enemySpawnHeight = 1.5f; // 敵のスポーン高さ

    private bool lastWasRiver = false;  //直前が川オブジェクトかどうか
    private List<Vector2> spawnPositions = new List<Vector2>(); // 敵のスポーン位置リスト
    private HashSet<int> usedXPositions = new HashSet<int>(); // 使用済みのX位置リスト

    private List<GameObject> activeEnemies = new List<GameObject>(); //アクティブな敵を追跡リスト

    void Start()
    {
        SetPlayerInitialPosition();  //プレイヤーの初期位置を設定
        GenerateTerrain(); // 地形を生成
        SpawnEnemies(); // 敵をスポーン
    }

    void SetPlayerInitialPosition()
    {
        //プレイヤーの位置を直接設定
        player.transform.position = new Vector2(0, playerSpawnHeight);
    }

    void GenerateTerrain()
    {
        for (int i = 0; i < stageWigth; i += 4)
        {
            if (i < safeZoneWidth) // 最初の安全ゾーンでは川を生成しない
            {
                Instantiate(groundPrefab, new Vector2(i, groundHeight), Quaternion.identity);
                if (Random.value > 0.5f)
                {
                    Instantiate(groundPrefab, new Vector2(i, groundHeight + 1), Quaternion.identity);
                }
            }
            else
            {
                if (Random.value > 0.7f && !lastWasRiver)  //ランダムな確率で川を生成
                {
                    Instantiate(riverPrefab, new Vector2(i, groundHeight), Quaternion.identity);
                    Instantiate(scaffoldPrefab, new Vector2(i, groundHeight + scaffoldHeight), Quaternion.identity);  //川上空に足場を配置
                    spawnPositions.Add(new Vector2(i, groundHeight + scaffoldHeight)); // 足場のスポーン位置を追加
                    lastWasRiver = true;  //川オブジェクトを配置したことを記録
                }
                else
                {
                    lastWasRiver = false;  //次のオブジェクトは川ではないことを記録

                    int groundCount = Random.Range(1, 4);  //ランダムな数の地面オブジェクトを連続生成
                    for (int j = 0; j < groundCount; j++)
                    {
                        Vector2 groundPosition = new Vector2(i + j * 4, groundHeight);
                        Instantiate(groundPrefab, groundPosition, Quaternion.identity);
                        spawnPositions.Add(groundPosition); // 地面のスポーン位置を追加
                    }

                    //ランダムに地面オブジェクトを上に積み上げる
                    if (Random.value > 0.5f)
                    {
                        Vector2 elevatedGroundPosition = new Vector2(i, groundHeight + 1);
                        Instantiate(groundPrefab, elevatedGroundPosition, Quaternion.identity);
                        spawnPositions.Add(elevatedGroundPosition); // 上に積み重ねた地面のスポーン位置を追加
                    }
                }
            }
        }
    }

    void SpawnEnemies()
    {
        ClearExistingEnemies(); //新しい敵をスポーンさせる前に既存の敵を消滅させる

        for (int i = 0; i < enemyCount; i++)
        {
            if (spawnPositions.Count == 0) break;

            Vector2 spawnPosition;
            int attempts = 0;
            do
            {
                spawnPosition = spawnPositions[Random.Range(0, spawnPositions.Count)];
                attempts++;
            } while (usedXPositions.Contains((int)spawnPosition.x) && attempts < 10);

            if (!usedXPositions.Contains((int)spawnPosition.x))
            {
                usedXPositions.Add((int)spawnPosition.x);
                Vector2 enemySpawnPosition = new Vector2(spawnPosition.x, spawnPosition.y + enemySpawnHeight);
                Instantiate(enemyPrefab, enemySpawnPosition, Quaternion.identity);
                activeEnemies.Add(enemyPrefab);  //アクティブな敵を追跡
            }
        }
    }

    void ClearExistingEnemies()
    {
        foreach (GameObject enemy in activeEnemies)
        {
            Destroy(enemy);  //現在アクティブな敵を全て破壊
        }
        activeEnemies.Clear();  //リストをクリア
        usedXPositions.Clear();  //使用したポジションをリセット
    }

    // 敵をリスポーンする関数
    public void RespawnEnemies()
    {
        // 既存の敵を全て破棄
        GameObject[] existingEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in existingEnemies)
        {
            Destroy(enemy);
        }

        // 敵を再生成
        SpawnEnemies();
    }
}
