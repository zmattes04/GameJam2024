using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextShake : MonoBehaviour
{
    public Dictionary<TMP_Text, Vector3> originalPositions = new Dictionary<TMP_Text, Vector3>();

    public void StartShake(TMP_Text text, float duration, float magnitude)
    {
        if (!originalPositions.ContainsKey(text))
        {
            originalPositions[text] = text.transform.localPosition;
        }
        StartCoroutine(ShakeText(text, duration, magnitude));
    }

    private IEnumerator ShakeText(TMP_Text text, float duration, float magnitude)
    {
        float elapsed = 0f;
        Vector3 originalPosition = originalPositions[text];

        while (elapsed < duration)
        {
            Vector3 randomOffset = new Vector3(
                Random.Range(-magnitude, magnitude),
                Random.Range(-magnitude, magnitude),
                0f
            );

            text.transform.localPosition = originalPosition + randomOffset;

            elapsed += Time.deltaTime;
            yield return null;
        }

        text.transform.localPosition = originalPosition;
    }
}