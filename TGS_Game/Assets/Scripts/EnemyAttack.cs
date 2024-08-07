using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int attackDamage = 10;

    public bool IsAttacking { get; private set; } // 攻撃中かどうかを判定するフラグ

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // IsAttacking = true;
            // PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            // if (playerHealth != null)
            // {
            //     playerHealth.TakeDamage(attackDamage);
            // }
            // IsAttacking = false; // 攻撃が終了したらフラグをリセット
        }
    }
    public void CancelAttack()
    {
        // 攻撃をキャンセルする処理
        IsAttacking = false;
    }
}
