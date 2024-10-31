using System.Collections;
using UnityEngine;

/// <summary>
/// Creates a screen-shake effect by jittering the camera for a configured duration and magnitude
/// </summary>
public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;

    [Tooltip("How long the camera will shake for")]
    [SerializeField] private float duration = 0.5f;

    [Tooltip("How violently the camera will shake (how far off-axis the camera will move)")]
    [SerializeField] private float magnitude = 0.1f;

    // Records the original position to revert back to after the shake.
    // If this were a moving camera, we'd need to store this dynamically on shake invocation.
    private Vector3 _originalPos;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);

        _originalPos = transform.localPosition;
        Debug.Log(_originalPos);
    }

    public void Play() => StartCoroutine(Shake());

    private IEnumerator Shake()
    {
        var elapsed = 0f;

        while (elapsed < duration)
        {
            // We achieve the shake by randomly 'jittering' the camera's transform position by the magnitude configured
            transform.localPosition = _originalPos + new Vector3(
                x: Random.Range(-1f, 1f) * magnitude,
                y: Random.Range(-1f, 1f) * magnitude,
                z: _originalPos.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Put the camera back to its original position after the shake is complete
        transform.localPosition = _originalPos;
    }
}