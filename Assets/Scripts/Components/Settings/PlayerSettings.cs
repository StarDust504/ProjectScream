using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSettings : MonoBehaviour
{
    [SerializeField] private SettingsJumpControl jumpControl;
    [SerializeField] private Slider slider;
    private float volumeSensitivity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        volumeSensitivity = slider.value;

        jumpControl.SetTestSensitivity(volumeSensitivity);
    }

    public void SavePrefs()
    {
        PlayerPrefs.SetFloat("VolumeSensitivity", volumeSensitivity);
    }
}
