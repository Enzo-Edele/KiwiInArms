using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float duration = 1.0f;
    public AnimationCurve curve;


    void Update()
    {
        
    }

    IEnumerator Shaking()
    {
        Vector2 startPosition = transform.position;
        float elapsedTime = 0.0f;

        while(elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime / duration);
            transform.position = new Vector3(startPosition.x + Random.insideUnitCircle.x * strength,
                                             startPosition.y + Random.insideUnitCircle.y * strength, -10);
            yield return null;
        }

        transform.position = new Vector3(startPosition.x, startPosition.y, -10);
    }

    public void Shake()
    {
        StartCoroutine("Shaking");
    }
}
