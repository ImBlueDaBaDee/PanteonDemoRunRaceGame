using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{

    public Transform followThis;
    public Vector3 offset;
    void Start()
    {

    }


    void Update()
    {
        if (followThis != null)
        {
            transform.position = new Vector3(offset.x, followThis.position.y + offset.y, followThis.position.z + offset.z);
        }
    }
}
