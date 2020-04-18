using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
