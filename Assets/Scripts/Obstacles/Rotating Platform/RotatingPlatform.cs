using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    [SerializeField] private float rotateSpeed;
    [SerializeField] private bool clockWise;
    private int clockWiseInt;


    void Start()
    {
        if (clockWise) { clockWiseInt = 1; }
        else { clockWiseInt = -1; }
    }

    void Update()
    {
        transform.Rotate(Vector3.forward * Time.deltaTime * rotateSpeed * clockWiseInt);
    }
    private void OnCollisionEnter(Collision collision)  //Make the collided object, child of Rotating Platform
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Opponent")
        {
            GameObject go = collision.gameObject;
            go.transform.parent = transform;
        }
    }
    private void OnCollisionStay(Collision collision)  //Make sure collided object don't rotate around its own axis
    {
        if (collision.gameObject.tag == "Player"|| collision.gameObject.tag == "Opponent")
        {
            GameObject go = collision.gameObject;
            StayStraight(go);

        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "Player" || collision.gameObject.tag == "Opponent")
        {
            GameObject go = collision.gameObject;
            go.transform.parent = null;
        }
    }
    private void StayStraight(GameObject go)
    {
        while (go.transform.rotation.z > 0) { go.transform.Rotate(Vector3.back); }
        while (go.transform.rotation.z < 0) { go.transform.Rotate(Vector3.forward); }
        while (go.transform.rotation.x > 0) { go.transform.Rotate(Vector3.left); }
        while (go.transform.rotation.x < 0) { go.transform.Rotate(Vector3.right); }
    }
}
