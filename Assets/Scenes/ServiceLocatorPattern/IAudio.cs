using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test_ServiceLocator
{
    /// <summary>
    /// Interface of an audio system.
    /// </summary>
    public interface IAudio
    {
        void PlaySound(AudioSource source, AudioClip clip = null);
        void StopSound(AudioSource source);
    }
}
