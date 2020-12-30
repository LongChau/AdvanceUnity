using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace NewEventSystem
{
    [CreateAssetMenu(fileName = "Event_HealthChanged", menuName = "DataContainer/Event_HealthChanged")]
    public class Event_HealthChanged : ScriptableObject
    {
        public event UnityAction<int> action;

        public void RaiseEvent(int health)
        {
            action?.Invoke(health);
        }

        public void Subcribe(UnityAction<int> callback)
        {
            action += callback;
        }

        public void Unsubcribe(UnityAction<int> callback)
        {
            action -= callback;
        }
    }
}