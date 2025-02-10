using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;  // 最大HP
    [SerializeField] private int currentHealth;   // 現在のHP
    [SerializeField] private Slider healthSlider;
    [SerializeField] private AudioClip hitSound;   //ダメージ効果音
    private AudioSource audioSource;   

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
        // ゲーム開始時に最大HPで初期化
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
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
        PlayHitSound();
    }

    void PlayHitSound()
    {
        audioSource.PlayOneShot(hitSound);
    }

    // HPゲージを更新する関数
    void UpdateHealthUI()
    {
        healthSlider.value = currentHealth;
    }

    void GameOver()
    {
        // ゲームオーバーシーンに移行
        SceneManager.LoadScene("GameOverScene");
    }
}
