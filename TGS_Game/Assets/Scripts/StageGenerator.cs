using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGenerator : MonoBehaviour
{
    public GameObject groundPrefab;  //地面プレハブ
    public GameObject riverPrefab;  //川プレハブ
    public GameObject scaffoldPrefab;  //足場プレハブ
    public GameObject playerPrefab;  // プレイヤープレハブ
    public int stageWigth = 100;  //ステージの幅
    public float groundHeight = -2.0f;  //地面の高さ
    public float scaffoldHeight = 2.0f;  //足場の高さ
    public float playerSpawnHeight = 3.0f; // プレイヤーのスポーン高さ
    public int safeZoneWidth = 20;   // 最初の安全ゾーンの幅

    private bool lastWasRiver = false;  //直前が川オブジェクトかどうか

    void Start()
    {
        SpawnPlayer();  // プレイヤーをスポーン
        GenerateTerrain(); // 地形を生成
    }

    void SpawnPlayer()
    {
        Instantiate(playerPrefab, new Vector2(0, groundHeight + playerSpawnHeight), Quaternion.identity);
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
                    lastWasRiver = true;  //川オブジェクトを配置したことを記録
                }
                else
                {
                    lastWasRiver = false;  //次のオブジェクトは川ではないことを記録

                    int groundCount = Random.Range(1, 4);  //ランダムな数の地面オブジェクトを連続生成
                    for (int j = 0; j < groundCount; j++)
                    {
                        Instantiate(groundPrefab, new Vector2(i + j * 4, groundHeight), Quaternion.identity);
                    }

                    //ランダムに地面オブジェクトを上に積み上げる
                    if (Random.value > 0.5f)
                    {
                        Instantiate(groundPrefab, new Vector2(i, groundHeight + 1), Quaternion.identity);
                    }
                }
            }
        }
    }
}
