using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    // set the camera offset values as properties for the editor
    public Vector3 camOffset = new Vector3(1f,1f,-3f);
    //set the target as the Player's Transform property - to access it's location
    private Transform target;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player").transform;
        Debug.Log(target.position);
    }

    // Update is called once per frame
    // LastUpdate is called after update
    // we use LateUpdate to ensure the player has moved before the camera 
    void LateUpdate()
    {
        this.transform.position = target.TransformPoint(camOffset);
        this.transform.LookAt(target);
        Debug.Log(this.transform.position);
    }
}
