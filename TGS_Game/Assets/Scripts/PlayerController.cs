using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float jumpForce = 10f; // ジャンプの強さ
    [SerializeField] private float speed = 5f; // プレイヤーの速度
    [SerializeField] private int maxHealth = 100;  // 最大HP
    [SerializeField] private Slider healthSlider;
    [SerializeField] private AudioClip hitSound;   //ダメージ効果音
    private int jumpCount = 0; // ジャンプの回数
    private Rigidbody2D rb;
    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;

    public int currentHealth { get; private set; }   // 現在のHP

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // spriteRendererの初期化
        jumpCount = 0; // jumpCountの初期化

        audioSource = GetComponent<AudioSource>();

        // ゲーム開始時に最大HPで初期化
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    void Update()
    {
        // 左右移動　Input.GetAxis("Horizontal")は-1.0から1.0までの値を返す
        float horizontal = Input.GetAxis("Horizontal");  // 左右移動の入力を変数horizontalに入れる
        transform.Translate(Vector2.right * horizontal * speed * Time.deltaTime);  // horizontalで取得した方向に速度を掛ける

        if (horizontal > 0)
        {
            spriteRenderer.flipX = false;  //スプライトを通常の向きで表示
        }
        else if (horizontal < 0)
        {
            spriteRenderer.flipX = true;  //スプライトを左右反転した向きで表示
        }

        // スペースキーが押され、ジャンプ回数が2未満の場合ジャンプ
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 2)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0); // 上向きの速度をリセット
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse); // ジャンプの力を加える
            jumpCount++;
        }

        if (healthSlider.value <= 0)
        {
            GameOver();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 地面に着地したらジャンプ回数をリセット
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Scaffold"))
        {
            jumpCount = 0;
        }
    }

    // HPを減らす関数
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        audioSource.PlayOneShot(hitSound);  // ヒット音
        healthSlider.value = currentHealth;  // HPゲージを更新する関数

        if (currentHealth < 0)  // hpが0より下回らないように制限
        {
            currentHealth = 0;
        }

        if (currentHealth == 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        // ゲームオーバーシーンに移行
        SceneManager.LoadScene("GameOverScene");
    }
}