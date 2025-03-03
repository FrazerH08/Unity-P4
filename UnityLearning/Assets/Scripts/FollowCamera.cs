using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    // Set the camera offset values as properties for the editor
    public Vector3 camOffset = new Vector3(0f, 2f, -5f); // Adjust the offset as needed
    // Set the target as the Player's Transform property - to access its location
    private Transform target;

    // Smoothing factor for the camera follow
    public float smoothSpeed = 0.125f;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player").transform;
        Debug.Log("Player position: " + target.position);
    }

    // LateUpdate is called after Update, used to ensure the camera follows after the player moves
    void LateUpdate()
    {
        // Calculate the desired position with the offset
        Vector3 desiredPosition = target.position + camOffset;

        // Smoothly move the camera toward the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Set the camera position to the smoothed position
        transform.position = smoothedPosition;

        // Make the camera look at the player
        transform.LookAt(target);

        Debug.Log("Camera position: " + transform.position);
    }
}
