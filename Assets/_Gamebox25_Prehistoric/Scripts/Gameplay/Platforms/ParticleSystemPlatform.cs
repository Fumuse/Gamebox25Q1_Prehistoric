using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ParticleSystemPlatform : MonoBehaviour
{
    [SerializeField] private bool controlScale = true;
    [SerializeField] private Vector3 platformScale = Vector3.one;
    [SerializeField] private float density = 66.6667f;
    [SerializeField] private ParticleSystem gasBody;
    [SerializeField] private Collider gasCollider;

    private void OnValidate()
    {
        gasCollider ??= GetComponent<Collider>();
        gasBody ??= GetComponentInChildren<ParticleSystem>();
        if (gasBody != null && controlScale)
        {
            ChangeGasScale();
            ChangeGasDensity();
        }
    }

    private void ChangeGasScale()
    {
        ParticleSystem.ShapeModule shape = gasBody.shape;
        shape.scale = platformScale;
    }

    private void ChangeGasDensity()
    {
        float constRateOverTime = Mathf.Floor(density * (platformScale.x * platformScale.y * platformScale.z));
        ParticleSystem.EmissionModule emission = gasBody.emission;
        emission.rateOverTime = new ParticleSystem.MinMaxCurve(constRateOverTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.matrix = transform.localToWorldMatrix;

        Vector3 size = default;
        Vector3 center = default;
        if (gasCollider is BoxCollider boxCollider)
        {
            size = boxCollider.size;
            center = boxCollider.center;
        }
        else if (gasCollider is CapsuleCollider capsuleCollider)
        {
            size = capsuleCollider.GetSize();
            center = capsuleCollider.center;
        }

        Gizmos.DrawWireCube(center, size);
    }
}