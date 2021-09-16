﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestUniRx
{
    public class BackgroundAudio : IAudio
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
