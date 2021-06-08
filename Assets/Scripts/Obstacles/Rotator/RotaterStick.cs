using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotaterStick : MonoBehaviour
{
    [SerializeField] private float force;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player" || collision.gameObject.tag== "Opponent")
        {
            GameObject go = collision.gameObject;
            Rigidbody rb = go.GetComponent<Rigidbody>();

            Vector3 direction = collision.contacts[0].point - go.transform.position;
            rb.AddForce(-direction.normalized * force, ForceMode.Impulse);
            HitEffect(collision.contacts[0].point);

        }
    }
    private void HitEffect(Vector3 pos)
    {
        GameObject hitParticle = Instantiate(Resources.Load("HitParticle") as GameObject, pos, Quaternion.identity);
        hitParticle.GetComponent<ParticleSystem>().Play();
    }
}
