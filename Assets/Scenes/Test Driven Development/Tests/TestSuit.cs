using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityAdvance.TDD {

    public class TestSuit {
        [Test]
        public void Test_TakeDamage() {
            HpComp health = new HpComp(200);
            health.TakeDamage(-10);
            Assert.AreEqual(190, health.CurrentHp);
        }

        [TestCase(10, 190)]
        [TestCase(200, 0)]
        [TestCase(300, 0)]
        public void Test_TakeDamage_WithInput(int damage, int expected) {
            HpComp health = new HpComp(200);
            health.TakeDamage(-damage);
            Assert.AreEqual(expected, health.CurrentHp);
        }
    }
}
