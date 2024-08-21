using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    public Transform attackPoint; // 攻撃の発生地点
    public float attackRange = 2.0f; // 攻撃の範囲
    public LayerMask enemyLayers; // 攻撃が当たる敵のレイヤー
    public int attackDamage = 10; // 攻撃力
    public AudioClip attackSound;  //攻撃時の効果音
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Kキーが押されたときに攻撃を実行
        if (Input.GetKeyDown(KeyCode.K))
        {
            Attack();
        }
    }

    void Attack()
    {
        //攻撃するときに呼び出されるメソッド
        PlayAttackSound();

        // 攻撃の範囲内にいる敵を検出
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, 2.0f);

        // 検出された敵にダメージを与える
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                Enemy enemyHealth = enemy.GetComponent<Enemy>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(attackDamage);

                    // 攻撃判定を相殺する処理
                    EnemyAttack enemyAttack = enemy.GetComponent<EnemyAttack>();
                    if (enemyAttack != null && enemyAttack.IsAttacking)
                    {
                        enemyAttack.CancelAttack();
                    }
                }
            }
        }
    }

    void PlayAttackSound()
    {
        audioSource.PlayOneShot(attackSound);
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
