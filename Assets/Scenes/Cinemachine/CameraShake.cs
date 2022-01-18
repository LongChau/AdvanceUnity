using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace UnityAdvance.Cinemachine
{
    public class CameraShake : MonoBehaviour
    {
        [SerializeField]
        private CinemachineVirtualCamera _cam;
        [SerializeField]
        private float _shakeIntensity;
        [SerializeField]
        private float _shakeTime;

        private CinemachineBasicMultiChannelPerlin _cinePerlin;
        private float _shakeTimer;

        [SerializeField]
        private bool _shakeCam;


        private float _startingIntensity;
        private float _totalShakeTime;

        private void OnValidate()
        {
            if (_shakeCam)
            {
                ShakeCamera(_shakeIntensity, _shakeTime);
                _shakeCam = false;
            }
        }

        private void Awake()
        {
            _cinePerlin = _cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        void ShakeCamera(float intensity, float time)
        {
            _cinePerlin.m_AmplitudeGain = intensity;
            _shakeTimer = time;

            _startingIntensity = intensity;
            _shakeTimer = time;
            _totalShakeTime = time;
        }

        private void Update()
        {
            if (_shakeTimer > 0f)
            {
                _shakeTimer -= Time.deltaTime;
                if (_shakeTimer <= 0f)
                {
                    // Time over.
                    //_cinePerlin.m_AmplitudeGain = 0f;
                    _cinePerlin.m_AmplitudeGain = Mathf.Lerp(_startingIntensity, 0f, 1 - (_shakeTimer / _totalShakeTime));
                }
            }
        }
    }
}
