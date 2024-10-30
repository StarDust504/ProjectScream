using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class CheckAudioVolume : MonoBehaviour
{
    [SerializeField] private int sampleWindow = 64;
    [SerializeField] private AudioClip audioClip;
    // Start is called before the first frame update
    void Start()
    {
        MicrophoneToAudioClip();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void MicrophoneToAudioClip()
    {
        string microphoneName = Microphone.devices[0];
        audioClip = Microphone.Start(microphoneName, true, 20, /*AudioSettings.outputSampleRate*/ 44100);
    }

    public float GetMicrophoneVolume()
    {
        return GetAudioVolume(Microphone.GetPosition(Microphone.devices[0]), audioClip);
    }
    public float GetAudioVolume(int clipPosition, AudioClip clip)
    {
        int startPosition = clipPosition - sampleWindow;

        if (startPosition < 0)
        {
            return 0;
        }

        float[] waveData = new float[sampleWindow];
        clip.GetData(waveData, startPosition);

        float totalVolume = 0;

        for (int i = 0; i < sampleWindow; i++)
        {
            totalVolume += Mathf.Abs(waveData[i]);
        }

        return totalVolume / sampleWindow;
    }
}
