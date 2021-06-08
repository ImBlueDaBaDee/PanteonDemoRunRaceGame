using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentRespawn : MonoBehaviour
{
    private Vector3 startPos;
    [SerializeField] private float fallHeight;
    void Start()
    {
        startPos = this.transform.position;
        fallHeight = -0.34f;
    }
    private void Update()
    {
        if (this.transform.position.y < fallHeight) { RespawnOpponent(); }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle") { RespawnOpponent(); }
    }
    private void RespawnOpponent() { this.transform.position = startPos; }
}
