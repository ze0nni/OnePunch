using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle {

    public interface Fighter
    {
        float Stat(int statId);

        void Hit(float damage);

        void Hill(float value);
    }

}