using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle.Aspects
{
    class AbsorbDamageByStatValueTests
    {

        [Test]
        public void AbsorbDamageByStatValue_no_effect() {
            var aspect = new AbsorbDamageByStatValue(0);

            var consumer = new TestFighter();
            consumer.getStat = (id) => 0;

            var result = aspect.OnBeforeHit(null, consumer, new OnBeforeHitResult(100, false));

            Assert.AreEqual(
                new OnBeforeHitResult(100, false),
                result
            );
        }

        [Test]
        public void AbsorbDamageByStatValue_absorb_30_percents_of_damage()
        {
            var aspect = new AbsorbDamageByStatValue(0);

            var consumer = new TestFighter();
            consumer.getStat = (id) => 30;

            var result = aspect.OnBeforeHit(null, consumer, new OnBeforeHitResult(100, false));

            Assert.AreEqual(
                new OnBeforeHitResult(70, false),
                result
            );
        }
    }
}
