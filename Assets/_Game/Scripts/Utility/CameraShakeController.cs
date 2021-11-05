using System.Collections;
using Cinemachine;
using UnityEngine;

namespace _Game.Scripts.Utility
{
    public class CameraShakeController : MonoBehaviour
    {
        private static CameraShakeController _instance;

        [SerializeField] private float hitShakeDuration;
        [SerializeField] private float hitShakeAmplitude;
        [SerializeField] private float hitShakeFrequency;

        private CinemachineVirtualCamera _virtualCamera;
        private CinemachineBasicMultiChannelPerlin _virtualCameraNoise;

        private bool _isShaking;

        private void Awake()
        {
            _instance = this;
        }

        private void Start()
        {
            _virtualCamera = GetComponent<CinemachineVirtualCamera>();
            _virtualCameraNoise = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }

        private IEnumerator HitShakeCoroutine()
        {
            _virtualCameraNoise.m_AmplitudeGain = hitShakeAmplitude;
            _virtualCameraNoise.m_FrequencyGain = hitShakeFrequency;

            yield return new WaitForSeconds(hitShakeDuration);

            _virtualCameraNoise.m_AmplitudeGain = 0;
            _virtualCameraNoise.m_FrequencyGain = 0;

            _isShaking = false;
        }

        public static void Shake()
        {
            if(_instance._isShaking) return;

            _instance._isShaking = true;
            _instance.StartCoroutine(_instance.HitShakeCoroutine());
        }
    }
}