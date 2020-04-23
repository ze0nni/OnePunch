using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct OnBeforeHitResult {
    public float currentDamage;
    public bool abort;

    public OnBeforeHitResult(float currentDamage, bool abort)
    {
        this.currentDamage = currentDamage;
        this.abort = abort;
    }
}
public interface BattleAspect {
    OnBeforeHitResult OnBeforeHit(Fighter source, Fighter consumer, float baseDamage, OnBeforeHitResult current);
    void OnHitHappened(Fighter source, Fighter consumer, float baseDamage);
}
sealed public class BattleArea
{
    public void Attack(
        Fighter source,
        Fighter consumer
    ) {
        var damage = source.Damage();

        float releasedDamage;
        consumer.Hit(damage, out releasedDamage);

        source.ConsumeMeat(releasedDamage);
    }
}
