using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingControl : MonoBehaviour
{
    public List<Light> lights; // List of lights to control
    public float flashDuration = 2f; // Total duration of the flashing
    public float flashInterval = 0.2f; // Time between flashes

    public void FlashLights()
    {
        StartCoroutine(FlashLightsCoroutine());
    }

    private IEnumerator FlashLightsCoroutine()
    {
        float elapsedTime = 0f;
        bool lightsOn = false;

        while (elapsedTime < flashDuration)
        {
            lightsOn = !lightsOn;
            SetLightsState(lightsOn);
            yield return new WaitForSeconds(flashInterval);
            elapsedTime += flashInterval;
        }
        SetLightsStateRandom(); // Ensure lights are off at the end
    }

    private void SetLightsState(bool state)
    {
        foreach (Light light in lights)
        {
            if (light != null)
            {
                light.enabled = state;
            }
        }
    }

    private void SetLightsStateRandom()
    {
        foreach (Light light in lights)
        {
            if (light != null)
            {
                bool randomBool = UnityEngine.Random.value > 0.5f;
                light.enabled = randomBool;
            }
        }
    }
}
