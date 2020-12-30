using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewEventSystem
{
    public class PlayerController : MonoBehaviour
    {
        public int health;
        public Event_HealthChanged event_HealthChanged;

        // Start is called before the first frame update
        void Start()
        {

        }

        [ContextMenu("UpdateHealth")]
        public void UpdateHealth()
        {
            event_HealthChanged.RaiseEvent(health);
        }
    }
}
