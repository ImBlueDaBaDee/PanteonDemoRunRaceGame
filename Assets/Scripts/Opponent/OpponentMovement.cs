using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentMovement : MonoBehaviour
{

    [SerializeField] private float dodgeSpeed;
    private int numberOfRays;
    private float angle;
    private float rayLength;
    [SerializeField] private float heightOfRays;
    [SerializeField] private float runSpeed;
    private Vector3 deltaPosition = Vector3.zero;
    private Animator anim;
    private SetAnimatorValues animatorScript;

    private bool isFinished;
    private void Start()
    {
        numberOfRays = 18;
        angle = -90;
        rayLength = 0.3f; runSpeed = 0.3f; dodgeSpeed = 1; 

        animatorScript = GameObject.Find("Manager").GetComponent<SetAnimatorValues>();
        anim = this.GetComponent<Animator>();
    }

    void Update()
    {

        ObstacleAvoidanceInputs();
        StayOnGroundInputs();
        deltaPosition = deltaPosition.normalized;
        GirlMovement(isFinished);
        animatorScript.SetAnimatorVariables(runSpeed, deltaPosition.x, anim);

    }
    private void ObstacleAvoidanceInputs()
    {
        if (!isFinished)
        {
            for (int i = 0; i < numberOfRays; i++)
            {
                int layerMask = 1 << 9;
                Quaternion rotation = Quaternion.AngleAxis(angle + (180 * i / (numberOfRays - 1)), this.transform.up);
                Vector3 direction = rotation * Vector3.forward;

                Ray ray = new Ray(this.transform.position + new Vector3(0, heightOfRays, 0), direction);
                RaycastHit hit;
                Debug.DrawRay(this.transform.position + new Vector3(0, heightOfRays, 0), direction * rayLength);
                if (Physics.Raycast(ray, out hit, rayLength, layerMask))
                {
                    deltaPosition -= (1f / numberOfRays) * dodgeSpeed * direction;
                }
                else
                {
                    deltaPosition += (1f / numberOfRays) * dodgeSpeed * direction;
                }
            }
        }
        else { deltaPosition = Vector3.zero; }
    }
    private void StayOnGroundInputs()
    {
        if (!isFinished)
        {
            int layerMask = 1 << 8;
            Quaternion leftRayRotation = Quaternion.AngleAxis(-45, this.transform.forward);
            Vector3 leftRayDirection = leftRayRotation * Vector3.down;
            Ray leftRay = new Ray(this.transform.position + new Vector3(0, 0.05f, 0), leftRayDirection);
            Ray rightRay = new Ray(this.transform.position + new Vector3(0, 0.05f, 0), new Vector3(-leftRayDirection.x, leftRayDirection.y, leftRayDirection.z));
            Debug.DrawRay(this.transform.position + new Vector3(0, 0.05f, 0), leftRayDirection * 2);
            Debug.DrawRay(this.transform.position + new Vector3(0, 0.05f, 0), new Vector3(-leftRayDirection.x, leftRayDirection.y, leftRayDirection.z * 2));
            RaycastHit hitR, hitL;

            if (!Physics.Raycast(rightRay, out hitR, 2f, layerMask))
            {
                deltaPosition += Vector3.left;
            }
            else if (!Physics.Raycast(leftRay, out hitL, 2f, layerMask))
            {
                deltaPosition += Vector3.right;
            }
        }
        else { deltaPosition = Vector3.zero; }
    }
    private void GirlMovement(bool finished)
    {
        if (!finished)
        {
            transform.Translate(deltaPosition.x * Time.deltaTime, 0, runSpeed * Time.deltaTime);
        }
        else { runSpeed = 0; }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == ("FinishLine"))
        {
            isFinished = true;
        }
    }

}

