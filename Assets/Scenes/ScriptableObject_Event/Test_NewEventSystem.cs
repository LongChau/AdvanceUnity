using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewEventSystem
{
    public class Test_NewEventSystem : MonoBehaviour
    {
        public Event_HealthChanged event_HealthChanged;

        private void Awake()
        {
            //event_HealthChanged.action += Handle_Event_HealthChanged;
            //event_HealthChanged.Subcribe(Handle_Event_HealthChanged);
        }

        private void OnEnable()
        {
            event_HealthChanged.Subcribe(Handle_Event_HealthChanged);
        }

        private void OnDisable()
        {
            event_HealthChanged.Unsubcribe(Handle_Event_HealthChanged);
        }

        private void Handle_Event_HealthChanged(int health)
        {
            Debug.Log($"Test_NewEventSystem.Handle_Event_HealthChanged({health})");
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        private void OnDestroy()
        {
            //event_HealthChanged.action -= Handle_Event_HealthChanged;
            //event_HealthChanged.Unsubcribe(Handle_Event_HealthChanged);
        }
    }
}