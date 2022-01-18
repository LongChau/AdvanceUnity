using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityAdvance.TDD {
    public class HpComp {
        private float _currentHp;

        public float CurrentHp { get => _currentHp; set => _currentHp = value; }

        public HpComp(float currentHp) {
            _currentHp = currentHp;
        }

        public void TakeDamage(float damage) {
            this._currentHp += damage;
            if (_currentHp < 0) _currentHp = 0;
        }
    }
}