using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Floater : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    private void OnValidate()
    {
        rb ??= GetComponent<Rigidbody>();
    }

    public void WaveForce(float waveHeight, float depthBeforeSubmerged, float displacementAmount)
    {
        float displacementMultiplier =
            Mathf.Clamp01((waveHeight - transform.position.y) / depthBeforeSubmerged) * displacementAmount;
        
        rb.AddForceAtPosition(
            new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMultiplier, 0f),
            transform.position,
            ForceMode.Acceleration
        );
    }
}