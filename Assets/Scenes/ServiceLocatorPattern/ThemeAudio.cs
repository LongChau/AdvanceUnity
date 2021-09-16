using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test_ServiceLocator
{
    public struct ThemeAudio : IAudio
    {
        public void PlaySound(AudioSource source, AudioClip clip)
        {
            source.PlayOneShot(clip);
        }

        public void StopSound(AudioSource source)
        {
            source.Stop();
        }
    }
}
