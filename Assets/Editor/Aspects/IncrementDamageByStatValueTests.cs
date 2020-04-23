using Battle.Aspects;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle.Aspects
{

    class IncrementDamageByStatValueTests
    {
        [Test]
        public void IncrementDamageByStatValue_no_affect()
        {
            var aspect = new IncrementDamageByStatValue(0);

            var source = new TestFighter();
            source.getStat = (id) => 0;

            var result = aspect.OnBeforeHit(source, null, new OnBeforeHitResult(0, false));

            Assert.AreEqual(
                new OnBeforeHitResult(0, false),
                result
            );
        }

        [Test]
        public void IncrementDamageByStatValue_has_affect_from_stat()
        {
            var aspect = new IncrementDamageByStatValue(0);

            var source = new TestFighter();
            source.getStat = (id) => 0 == id ? 5 : 0;

            var result = aspect.OnBeforeHit(source, null, new OnBeforeHitResult(0, false));

            Assert.AreEqual(
                new OnBeforeHitResult(5, false),
                result
            );
        }

        [Test]
        public void IncrementDamageByStatValue_has_affect_from_other_stat()
        {
            var aspect = new IncrementDamageByStatValue(0);

            var source = new TestFighter();
            source.getStat = (id) => 1 == id ? 5 : 0;

            var result = aspect.OnBeforeHit(source, null, new OnBeforeHitResult(0, false));

            Assert.AreEqual(
                new OnBeforeHitResult(0, false),
                result
            );
        }
    }
}