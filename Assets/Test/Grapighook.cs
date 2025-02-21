using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapighook : MonoBehaviour
{
    public LayerMask grappleLayer;
    public Transform firePoint;
    public LineRenderer lineRenderer;
    public float grappleSpeed = 20f;
    public float grappleRange = 10f;
    public Rigidbody2D rb;
    private Vector2 grapplePoint;
    private bool isGrappling = false;
    private Animator animator;
    public float cooldownDuration = 2f;
    private bool isCooldown = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isCooldown) 
        {
            FireGrapple();
            animator.SetTrigger("Jump");
            StartCoroutine(CooldownRoutine()); 
        }

        if (isGrappling)
        {
            DrawRope();
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrappling)
        {
            JumpFromWall();
            animator.SetTrigger("Take");
        }
    }

    void FireGrapple()
    {
        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, (MouseWorldPosition() - (Vector2)firePoint.position).normalized, grappleRange, grappleLayer);

        if (hit.collider != null)
        {
            grapplePoint = hit.point;
            isGrappling = true;
            StartCoroutine(PullToTarget());
        }
    }

    IEnumerator PullToTarget()
    {
        while (Vector2.Distance(transform.position, grapplePoint) > 0.5f && isGrappling)
        {
            Vector2 direction = (grapplePoint - (Vector2)transform.position).normalized;
            rb.velocity = direction * grappleSpeed;
            yield return null;
        }

        rb.velocity = Vector2.zero;
        isGrappling = false;
        lineRenderer.enabled = false;
    }

    void DrawRope()
    {
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, grapplePoint);
    }

    void JumpFromWall()
    {
        Vector2 jumpDirection = (transform.position - (Vector3)grapplePoint).normalized;
        rb.velocity = Vector2.zero;
        rb.AddForce(jumpDirection * 10f, ForceMode2D.Impulse);
        isGrappling = false;
        lineRenderer.enabled = false;
    }

    Vector2 MouseWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    IEnumerator CooldownRoutine()
    {
        isCooldown = true;
        yield return new WaitForSeconds(cooldownDuration); 
        isCooldown = false;
    }
}
