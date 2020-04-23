using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Fighter
{
    float Stat(int statId);

    void Hit(float damage);

    void Hill(float value);
}
