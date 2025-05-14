using UnityEngine;
using System.Collections.Generic;

public class SmoothCameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;
    public Vector3 minValues, maxValues;
    public float smoothFactor = 0.1f;

    void LateUpdate()
    {
        Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
    // void FixedUpdate()
    // {
    //     Vector3 targetPosition = target.position + offset;

    //     Vector3 clampedPosition = new Vector3(
    //     Mathf.Clamp(targetPosition.x, minValues.x, maxValues.x),
    //     Mathf.Clamp(targetPosition.y, minValues.y, maxValues.y),
    //     Mathf.Clamp(targetPosition.z, minValues.z, maxValues.z)
    // );

    //     // Interpolate to the clamped position instead
    //     transform.position = Vector3.Lerp(transform.position, clampedPosition, smoothFactor * Time.deltaTime);
    // }
}

//i want to add this code into the pre-existing camera script
// public Vector3 minValues, maxValues;

// Vector3 targetPosition = target.position + offset;

// Vector3 clampedPosition = new Vector3(
//     Mathf.Clamp(targetPosition.x, minValues.x, maxValues.x),
//     Mathf.Clamp(targetPosition.y, minValues.y, maxValues.y),
//     Math.Clamp(targetPosition.z, minValues.z, maxValues.z)
// );

// transform.position = Vector3.SmoothDamp(transform.position, clampedPosition, ref velocity, smoothTime);
