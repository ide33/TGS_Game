using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private int attackDamage = 10;
    [SerializeField] private float delayTime = 1.0f;  // 待機時間
    private float lastAttackTime;  // 最後の攻撃の時間
    private bool isAttackIdle = false;  // 攻撃待機かどうか
    public bool IsAttacking { get; private set; } // 攻撃中かどうかを判定するフラグ

    void Update()
    {
        // isAttackIdleがtrueの場合、一定時間後に
        if(isAttackIdle && Time.time - lastAttackTime >= delayTime)
        {
            
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // プレイヤーを検知し、攻撃準備を開始
            isAttackIdle = true;
            lastAttackTime = Time.time;
        }
    }

    // private void AttackAction()
    // {
    //     if()
    //     {
            
    //     }
    // }
    public void CancelAttack()
    {
        // 攻撃をキャンセルする処理
        IsAttacking = false;
    }
}
