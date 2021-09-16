using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Test
{
    public class FakeViewModel : IDisposable
    {
        // Usage: Tracking the changes of a property value.
        public readonly ReactiveProperty<int> HealthValue = new ReactiveProperty<int>();
        // Usage: Work as a callback execute.
        public readonly ReactiveCommand<int> Sacrifice = new ReactiveCommand<int>();
        // Usage: tracking something changes.
        public readonly Subject<bool> CheckingAlive = new Subject<bool>();

        public void Dispose()
        {
            HealthValue?.Dispose();
        }
    }
}
