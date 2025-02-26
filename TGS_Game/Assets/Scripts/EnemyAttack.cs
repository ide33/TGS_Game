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
    private bool isAttackIdle = false;  // 攻撃待機かどうか

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
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

    public void AttackAction()
    {
        
    }
}
