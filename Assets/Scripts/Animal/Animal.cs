using UnityEngine;

public interface Animal{
    void Spawn();

    void TakeDamage(float amount);
    void DetectPlayer();

    void WalkTo(Vector3 targetPosition);
    void RunTo(Vector3 targetPosition);
    void Idle(); //平静动作
    void Attack(GameObject target);

    void Die(); //这里要掉落肉


    // TODO: 有时间再做
    // void ReceiveHitFrom(); //收到来自这个方向的力，产生位移
}
