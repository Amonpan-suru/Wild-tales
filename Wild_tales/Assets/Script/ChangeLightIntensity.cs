using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLightIntensity : MonoBehaviour
{
    [SerializeField]
    Light CandleLight;

    [SerializeField]
    float minIntensity, maxIntensity, minVariationFactor, maxVariationFactor, Speed;

    float variationFactor;
    float random;
    
    void Start()
    {
        random = Random.Range(0.05f, 0.1f);
        CandleLight = GetComponent<Light>();

        FixedUpdate();
    }

    private void FixedUpdate()
    {
        variationFactor = Random.Range(minVariationFactor, maxVariationFactor);
    }
    private void Update()
    {
        float noise = Mathf.PerlinNoise(random, Time.time);
        CandleLight.intensity = Mathf.Lerp(minIntensity, maxIntensity, noise * variationFactor * Speed);

        //CandleLight.intensity = (minIntensity + Mathf.PingPong(Time.time * variationSpeed, maxIntensity))
        //                         * variationFactor;
    }
}
