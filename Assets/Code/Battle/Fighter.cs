using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Fighter
{
    float Damage();

    void Hit(float damage, out float releasedDamage);

    void ConsumeMeat(float releasedDamage);
}
