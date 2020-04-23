using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle.Aspects
{
    class RestoreHealthFromDamageByStatValueTests
    {
        [Test]
        public void RestoreHealthFromDamageByStat_no_effect() {
            var aspect = new RestoreHealthFromDamageByStatValue(0);
           
            var source = new TestFighter();
            source.getStat = (id) => 0;

            var hillCalls = new List<float>();
            source.onHill = x => hillCalls.Add(x);

            aspect.OnHitHappened(null, source, null, 100);

            Assert.AreEqual(
                new float[] { },
                hillCalls
            );
        }

        [Test]
        public void RestoreHealthFromDamageByStat_hill_by_2()
        {
            var aspect = new RestoreHealthFromDamageByStatValue(0);

            var source = new TestFighter();
            source.getStat = (id) => 20;

            var hillCalls = new List<float>();
            source.onHill = x => hillCalls.Add(x);

            aspect.OnHitHappened(null, source, null, 10);

            Assert.AreEqual(
                new float[] { 2 },
                hillCalls
            );
        }
    }
}
