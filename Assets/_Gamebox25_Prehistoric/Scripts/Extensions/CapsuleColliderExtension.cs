using UnityEngine;

public static class CapsuleColliderExtension
{
    public static Vector3 GetSize(this CapsuleCollider collider)
    {
        Vector3 size;
        switch (collider.direction)
        {
            case 0: // X
                size = new Vector3(collider.height, collider.radius * 2, collider.radius * 2);
                break;
            case 2: // Z
                size = new Vector3(collider.radius * 2, collider.radius * 2, collider.height);
                break;
            default: // Y
                size = new Vector3(collider.radius * 2, collider.height, collider.radius * 2);
                break;
        }

        return size;
    }
}