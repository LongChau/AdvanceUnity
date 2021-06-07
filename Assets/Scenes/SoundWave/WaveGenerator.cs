using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LC.Ultility
{
    /// Creates a wave form based on an audiosource
    /// Modified version of http://forum.unity3d.com/threads/making-sense-of-spectrum-data.90262
    /// Make sure to plug in the _topLine and _bottomLine line renderers
    public class WaveGenerator : MonoBehaviour
    {
        AudioSource _audioSource;
        const int SpectrumSize = 8192;
        readonly float[] _spectrum = new float[SpectrumSize];
        public LineRenderer _topLine;
        public LineRenderer _bottomLine;

        public float pointDistance = 0.1f;
        public float pointHeightDistance = 34f;

        public void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void ShowWaves()
        {
            // Show these line renderer.
            _topLine.gameObject.SetActive(true);
            _bottomLine.gameObject.SetActive(true);
        }

        public void HideWaves()
        {
            // Hide these line renderer.
            _topLine.gameObject.SetActive(false);
            _bottomLine.gameObject.SetActive(false);
        }

        public void Update()
        {
            _audioSource.GetSpectrumData(_spectrum, 0, FFTWindow.BlackmanHarris);

            var bandSize = 1.1f;
            var crossover = bandSize;
            var viewSpectrum = new List<float>();
            var b = 0f;
            for (var i = 0; i < SpectrumSize; i++)
            {
                var d = _spectrum[i];
                b = Mathf.Max(d, b); // find the max as the peak value in that frequency band.
                if (i > crossover - 3)
                {
                    crossover *= bandSize; // frequency crossover point for each band.
                    viewSpectrum.Add(b);
                    b = 0;
                }
            }

            SetLinePoints(viewSpectrum, _topLine);
            SetLinePoints(viewSpectrum, _bottomLine, -1);
        }

        private void SetLinePoints(List<float> viewSpectrum, LineRenderer lineRenderer, float modifier = 1)
        {
            var width = pointDistance * viewSpectrum.Count;
            lineRenderer.positionCount = viewSpectrum.Count;
            lineRenderer.SetPositions(viewSpectrum.Select((x, i) => transform.position + new Vector3(-width / 2 + i * pointDistance, x * pointHeightDistance * modifier)).ToArray());
        }
    }
}
