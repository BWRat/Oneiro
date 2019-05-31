using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Experimental.Rendering.HDPipeline;

[RequireComponent(typeof(Volume))]
public class Desaturation : MonoBehaviour
{
    [SerializeField] Transform object1;
    [SerializeField] Transform object2;
    [SerializeField] VolumeProfile profile;
    [SerializeField] AnimationCurve blackOutCurve;
    [SerializeField] float maxDistance;

    float originalSaturation;
    ColorAdjustments colorAdjustment;

    private void OnEnable()
    {
        profile.TryGet(out colorAdjustment);
        if (colorAdjustment == null)
        {
            profile.Add<ColorAdjustments>();
            profile.TryGet(out colorAdjustment);
            colorAdjustment.saturation.overrideState = true;
        }

        originalSaturation = colorAdjustment.saturation.value;
    }

    private void OnDisable()
    {
        colorAdjustment.saturation.value = originalSaturation;
    }

    private void Update()
    {
        var distance = Vector3.Distance(object1.position, object2.position);
        colorAdjustment.saturation.value = blackOutCurve.Evaluate(Mathf.Lerp(1, 0, distance / maxDistance)) * -100;
    }
}