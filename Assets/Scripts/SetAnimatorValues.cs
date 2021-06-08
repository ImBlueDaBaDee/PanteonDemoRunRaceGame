using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAnimatorValues : MonoBehaviour
{
    public void SetAnimatorVariables(float forwardSpeed, float horizontalSpeed, Animator anim)
    {
        anim.SetFloat("speed", forwardSpeed);
        anim.SetFloat("runOrRotate", horizontalSpeed);

    }

}
