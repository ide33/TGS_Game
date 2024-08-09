using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;  // 最大HP
    public int currentHealth;    // 現在のHP
    public GameObject healthSliderPrefab; // プレハブ化されたスライダー
    private Slider healthSlider;       // 実際にインスタンス化されたスライダー

    void Start()
    {
       // ゲーム開始時に最大HPで初期化
        currentHealth = maxHealth;

        // スライダーのインスタンスを生成し、プレイヤーに関連付け
        GameObject sliderInstance = Instantiate(healthSliderPrefab, transform.position, Quaternion.identity);
        sliderInstance.transform.SetParent(transform, false); // プレイヤーオブジェクトの子オブジェクトに設定
        healthSlider = sliderInstance.GetComponent<Slider>(); // スライダーコンポーネントを取得

        // 初期状態のHPをスライダーに反映
        UpdateHealthUI();
    }

    void Update()
    {
        if (healthSlider.value <= 0)
        {
            GameOver();
        }
    }

    // HPを減らす関数
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        UpdateHealthUI();
    }

    // HPゲージを更新する関数
    void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            healthSlider.value = (float)currentHealth / maxHealth;
        }
    }

    void GameOver()
    {
        // ゲームオーバーシーンに移行
        SceneManager.LoadScene("GameOverScene");
    }
}
