using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestUniRx
{
    public class TestAudio : MonoBehaviour
    {
        [SerializeField]
        private AudioSource _source;
        [SerializeField]
        private AudioClip _themeClip;
        [SerializeField]
        private AudioClip _effectClip;
        [SerializeField]
        private AudioClip _backgroundClip;

        // Start is called before the first frame update
        void Start()
        {

        }

        [ContextMenu("PlayTheme")]
        void PlayTheme()
        {
            // Init the theme audio.
            ThemeAudio themeAudio = new ThemeAudio();
            // Provide it to the service locator.
            AudioServiceLocator.Provide(themeAudio); // Should do this on startup.
            // Now the service locator has that audio as current. Play it.
            //AudioServiceLocator.GetAudio().PlaySound(_source, _themeClip);  // with this you can use it anywhere.
            //themeAudio.PlaySound(_source, _themeClip);
        }

        [ContextMenu("PlayEffect")]
        void PlayEffect()
        {
            EffectAudio effectAudio = new EffectAudio();
            AudioServiceLocator.Provide(effectAudio);
            IAudio audio = AudioServiceLocator.GetAudio();
            audio.PlaySound(_source, _effectClip);
        }

        [ContextMenu("PlayBackground")]
        void PlayBackground()
        {
            BackgroundAudio bgAudio = new BackgroundAudio();
            AudioServiceLocator.Provide(bgAudio);
            IAudio audio = AudioServiceLocator.GetAudio();
            audio.PlaySound(_source, _backgroundClip);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
