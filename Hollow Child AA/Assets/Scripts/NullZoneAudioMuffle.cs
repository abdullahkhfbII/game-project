using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class NullZoneAudioMuffle : MonoBehaviour
{
    
    public AudioLowPassFilter lowPass;
    public float muffledCutoff = 700f;      // muffled sound
    public float normalCutoff = 22000f;     // normal sound

    
    public AudioDistortionFilter distortion;
    public float maxDistortion = 0.45f;     // how noisy it gets
    public float normalDistortion = 0f;
    public float distortionLerpSpeed = 1.5f;

    
    public float transitionSpeed = 2f;

    private float targetCutoff;
    private float targetDistortion;

    void Start()
    {
        if (lowPass == null) lowPass = GetComponent<AudioLowPassFilter>();
        if (distortion == null) distortion = GetComponent<AudioDistortionFilter>();

        targetCutoff = normalCutoff;
        targetDistortion = normalDistortion;
    }

    void Update()
    {
        // Smooth Low Pass transition
        lowPass.cutoffFrequency = Mathf.MoveTowards(
            lowPass.cutoffFrequency,
            targetCutoff,
            Time.deltaTime * transitionSpeed * 1000f
        );

        // Smooth Distortion transition
        distortion.distortionLevel = Mathf.MoveTowards(
            distortion.distortionLevel,
            targetDistortion,
            Time.deltaTime * distortionLerpSpeed
        );
    }

    public void EnterNullZone()
    {
        targetCutoff = muffledCutoff;         // deeper, darker sound
        targetDistortion = maxDistortion;     // add noise crackling
    }

    public void ExitNullZone()
    {
        targetCutoff = normalCutoff;
        targetDistortion = normalDistortion;
    }
}
