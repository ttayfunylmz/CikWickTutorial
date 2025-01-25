using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance { get; private set; }

    private CinemachineBasicMultiChannelPerlin _cinemachineBasicMultiChannelPerlin;

    [SerializeField] private float _shakeTimer;
    [SerializeField] private float _shakeTimerTotal;
    private float _startingIntensity;

    private void Awake()
    {
        Instance = this;

        _cinemachineBasicMultiChannelPerlin = GetComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private IEnumerator CameraShakeCoroutine(float intensity, float time, float delay)
    {
        yield return new WaitForSeconds(delay);
        _shakeTimer = time;
        _cinemachineBasicMultiChannelPerlin.AmplitudeGain = intensity;
        _shakeTimerTotal = time;
        _startingIntensity = intensity;
    }

    public void ShakeCamera(float intensity, float time, float delay = 0f)
    {
        StartCoroutine(CameraShakeCoroutine(intensity, time, delay));
    }

    private void Update()
    {
        if (_shakeTimer > 0f)
        {
            _shakeTimer -= Time.deltaTime;

            if (_shakeTimer <= 0f)
            {
                _cinemachineBasicMultiChannelPerlin.AmplitudeGain
                    = Mathf.Lerp(_startingIntensity, 0f, 1 - (_shakeTimer / _shakeTimerTotal));
            }
        }
    }
}
