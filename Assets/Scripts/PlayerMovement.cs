using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float lastFrameFingerPosX;
    private float moveFactorX;
    [SerializeField] private float swerveSpeed, runSpeed;
    Vector3 moveInputs;
    private Animator anim;
    SetAnimatorValues animatorScript;

    public bool isFinished, isPlayerDead;
    void Start()
    {
        anim = this.GetComponent<Animator>();
        animatorScript = GameObject.Find("Manager").GetComponent<SetAnimatorValues>();
        isFinished = false;
        isPlayerDead = false;
        runSpeed = 0.3f; swerveSpeed = 0.05f;
    }

    void Update()
    {
        GetInputs();
        BoyMovement(isFinished, moveInputs);
        if (this.transform.position.y < -0.3f) { PlayerDead(); }

        animatorScript.SetAnimatorVariables(runSpeed, moveFactorX, anim);
    }

    private void GetInputs()
    {
        if (Input.GetMouseButtonDown(0)) { lastFrameFingerPosX = Input.mousePosition.x; }
        else if (Input.GetMouseButton(0))
        {
            moveFactorX = Input.mousePosition.x - lastFrameFingerPosX;
            lastFrameFingerPosX = Input.mousePosition.x;
        }
        else if (Input.GetMouseButtonUp(0)) { moveFactorX = 0; }
        moveInputs = new Vector3(moveFactorX * swerveSpeed, 0, runSpeed);
    }
    private void BoyMovement(bool finished, Vector3 inputs)
    {
        if (!finished)
        {
            transform.Translate((inputs * Time.deltaTime));
        }
        else
        {
            Vector3 endPos = GameObject.FindGameObjectWithTag("Wall").transform.GetChild(0).transform.position;
            runSpeed = 0; moveFactorX = 0;
            this.transform.position = endPos;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == ("FinishLine"))
        {
            isFinished = true;
        }
    }
    private void PlayerDead()
    {
        isPlayerDead = true;
        FindObjectOfType<GameManager>().Restart();

    }

}
