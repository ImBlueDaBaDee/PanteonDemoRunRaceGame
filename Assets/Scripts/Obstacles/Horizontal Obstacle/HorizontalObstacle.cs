using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalObstacle : MonoBehaviour
{
    private int numberOfRays;
    private float dirChange;
    [SerializeField] private int angle; //Smaller the angle get, obstacle move closer to the corners.
    [SerializeField] private float obstacleSpeed = 0.7f;
    private int layerMask = 1 << 9;


    void Start()
    {
        numberOfRays = 2;
        dirChange = 1;

    }

    void Update()
    {
        LookForObstacles();
        transform.Translate(dirChange * obstacleSpeed * Time.deltaTime, 0, 0);

    }
    private void LookForObstacles()
    {
        for (int i = 0; i < numberOfRays; i++)
        {
            Quaternion rotation = Quaternion.AngleAxis((-angle / 2) + (angle * i / (numberOfRays - 1)), this.transform.forward);
            Vector3 direction = rotation * Vector3.down;

            Ray ray = new Ray(this.transform.position, direction);
            RaycastHit hit;
            if (!Physics.Raycast(ray, out hit, 2f)) // Change direction if obstacle is too close to a corner
            {
                ChangeDirection(direction);
            }
            else if (Physics.Raycast(ray, out hit, 2f, layerMask)) // Change direction if obstacle is too close to another obstacle
            {
                ChangeDirection(direction);
            }
        }
    }
    private void ChangeDirection(Vector3 dir)
    {
        if (dir.x > 0) { dirChange = -1; }  //If the x value of the vector is + then obstacle should go -x direction
        else if (dir.x < 0) { dirChange = 1; }
        //Could not use the direction variable directly, because it would change speed of obstacle
        //before the obstacle reach any corner. 
    }
}
