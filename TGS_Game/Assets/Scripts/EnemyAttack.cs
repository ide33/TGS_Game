using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private int attackDamage = 10;  // 攻撃力
    [SerializeField] private float attackRange =1.5f;  // 攻撃範囲
    [SerializeField] private float delayTime = 1.0f;  // 待機時間
    [SerializeField] private float attackCooldown = 2.0f;  // クールタイム

    private Transform target;  // プレイヤーのTransformを保持
    private float lastAttackTime;  // 最後の攻撃の時間
    // private bool isAttackIdle = false;  // 攻撃待機かどうか
    private PlayerAttackController playerAttack;
    private PlayerController playerHealth;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");  // プレイヤーオブジェクトを取得
        if(player != null)
        {
            target = player.transform;  // プレイヤーのTransformを取得
        }
    }

    void Update()
    {

        if(target == null) return; // プレイヤー見つからない場合はなにもしない
        {
            float distanceToPlayer = Vector2.Distance(transform.position, target.position);

            if(distanceToPlayer <= attackRange && Time.time - lastAttackTime >= attackCooldown)
            {
                AttackAction();
            }
        }
    }

    void AttackAction()
    {
        if(target == null) return;  // プレイヤーが見つからなければ何もしない
        playerAttack = GetComponent<PlayerAttackController>();  // インスタンスを取得
        if(playerAttack != null && playerAttack.IsAttacking)  // プレイヤーが攻撃中かどうか
        {
            return;
        }
        else
        {
            playerHealth = target.GetComponent<PlayerController>();
            if(playerHealth != null)
            {
                playerHealth.currentHealth -= attackDamage;  // プレイヤーのhpを減らす
            }
        }
    }
}
