using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform Player;
    public Vector3 offset;
    public float smoothSpeed = 0.125f;

    int check = 0;
    void LateUpdate()
    {
        int count = Player.childCount;



        Vector3 desiredPosition = Player.position + offset + new Vector3(0, 0.13f * count, 0);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
        check += 10;

    }
}
