using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public float Delay;
    public float TimeToRespawn;

    private TargetJoint2D targetJoint;
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;

    private Vector3 _defaultPosition;

    void Start()
    {
        targetJoint = GetComponent<TargetJoint2D>();
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();

        _defaultPosition = transform.position;
    }

    IEnumerator Falling()
    {
        yield return new WaitForSeconds(Delay);

        targetJoint.enabled = false;
        boxCollider.enabled = false;

        yield return new WaitForSeconds(TimeToRespawn);

        rb.velocity = Vector3.zero;
        targetJoint.enabled = true;
        boxCollider.enabled = true;

        transform.position = _defaultPosition;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            StartCoroutine(Falling());
        }
    }
}
