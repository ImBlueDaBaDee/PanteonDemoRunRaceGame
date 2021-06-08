using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float rotateSpeed;
    [SerializeField] private bool clockWise;
    private int clockWiseInt;
    void Start()
    {
        if (clockWise) { clockWiseInt = 1; }
        else { clockWiseInt = -1; }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed * clockWiseInt);
    }
}
