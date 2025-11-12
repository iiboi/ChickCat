using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance { get; private set; }
    private CinemachineBasicMultiChannelPerlin CinemachineBasicMultiChannelPerlin;

    private float ShakeTimer;
    private float ShakeTimerTotal;
    private float StartingIntencity;

    private void Awake()
    {
        Instance = this;
        CinemachineBasicMultiChannelPerlin = GetComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private IEnumerator CameraShakeCouritine(float intencity, float time, float delay)
    {
        yield return new WaitForSeconds(delay);
        CinemachineBasicMultiChannelPerlin.AmplitudeGain = intencity;
        ShakeTimer = time;
        ShakeTimerTotal = time;
        StartingIntencity = intencity;
    }

    public void ShakeCamera(float intencity, float time, float delay = 0f)
    {
        StartCoroutine(CameraShakeCouritine(intencity, time, delay));
    }

    private void Update() 
    {
        if(ShakeTimer > 0)
        {
            ShakeTimer -= Time.deltaTime;
            if(ShakeTimer < 0f)
            {
                CinemachineBasicMultiChannelPerlin.AmplitudeGain
                = Mathf.Lerp(StartingIntencity, 0f, 1 - (ShakeTimer / ShakeTimerTotal));
            }
        }
    }
}
