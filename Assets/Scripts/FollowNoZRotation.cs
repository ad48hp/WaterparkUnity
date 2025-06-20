using UnityEngine;

public class FollowNoZRotation : MonoBehaviour
{
    public Transform target;

    [Header("Enable Follow Options")]
    public bool followPosition = true;
    public bool followRotation = true;

    void LateUpdate()
    {
        if (target == null) return;

        if (followPosition)
        {
            transform.position = target.position;
        }

        if (followRotation)
        {
            Vector3 targetEuler = target.rotation.eulerAngles;
            targetEuler.z = 0f;
            transform.rotation = Quaternion.Euler(targetEuler);
        }
    }
}
