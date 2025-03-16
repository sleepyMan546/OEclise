using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Animator anim;

    public float jumpForce = 10f;
    private bool isGrounded;
    public bool facingRight = true;
    public SpriteRenderer bodyRenderer;
    private BoxCollider2D boxCollider2D;
    [SerializeField] private LayerMask grounlayer;
    [SerializeField] private LayerMask walllayer;
    private bool faceRight = true;
    public float dashSpeed = 1f;
    public float dashDuration = 1f;
    public bool isDashing = false;
    public float dashCooldown = 0.5f;
    private bool canDash = true;
    private float direction;
    public float moveDirection;
    public Transform shootPoint;
    public WeaponSwitchDop weaponSwitchDop;
    private int airJumpCount = 0;
    public int airJumpsAllowed = 1;
    public GameObject ghostPrefab;
    public float ghostSpawnRate = 0.1f;
    private float nextGhostTime = 0f;
    public float wallSlideSpeed = 0.5f;
    private bool isWallSliding;
    public float wallStickTime = 1f;
    private float wallStickCounter;
    private bool isParent;

    [SerializeField] private AudioSource dashSoundSource;
    [SerializeField] private AudioSource walkSoundSource; // เพิ่ม AudioSource สำหรับเสียงเดิน
    [SerializeField] private AudioSource jumpSoundSource; // เพิ่ม AudioSource สำหรับเสียงกระโดด

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();


        if (dashSoundSource == null)
        {
            Debug.LogError("No audio sourc");
        }
    }


    void Update()
    {
        float move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(move * moveSpeed, rb.velocity.y);


        if (facingRight)
        {
            bodyRenderer.flipX = false;
        }
        else
        {
            bodyRenderer.flipX = true;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (IsGrounded())
            {
                Jump();
                airJumpCount = 0;
                Debug.Log("airJump = " + airJumpCount);
            }
            else if (CanAirJump())
            {
                AirJump();
            }
            else if (isWallSliding)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                anim.SetTrigger("Jump");
                if (jumpSoundSource != null) // เล่นเสียงกระโดดเมื่อกระโดดจากกำแพง
                {
                    jumpSoundSource.Play();
                }
            }
        }

        anim.SetBool("Walk", move != 0);

        // ควบคุมเสียงเดิน
        if (move != 0)
        {
            if (walkSoundSource != null && !walkSoundSource.isPlaying)
            {
                walkSoundSource.Play();
            }
        }
        else
        {
            if (walkSoundSource != null && walkSoundSource.isPlaying)
            {
                walkSoundSource.Stop();
            }
        }


        if (Input.GetKey(KeyCode.Mouse1) && CanDash() && !isDashing && canDash)
        {
            Debug.Log("Dash");
            StartCoroutine(DashRoutine());
        }
        if (Input.GetKey(KeyCode.E))
        {
            Debug.Log("Can Dash" + CanDash());
        }

        isWallSliding = onWall() && rb.velocity.x < 0;

        if (isWallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -wallSlideSpeed));
        }

        if (onWall() && Input.GetAxisRaw("Horizontal") == transform.localScale.x)
        {
            if (wallStickCounter > 0)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
                wallStickCounter -= Time.deltaTime;

            }
        }
        else
        {
            wallStickCounter = wallStickTime;
        }
        anim.SetBool("Wall", isWallSliding);

    }

    bool CanDash()
    {
        return weaponSwitchDop != null && weaponSwitchDop.GetCurrentWeapon() == "pistol";
    }
    bool CanAirJump()
    {
        return airJumpCount < airJumpsAllowed && weaponSwitchDop != null && weaponSwitchDop.GetCurrentWeapon() == "shotgun";
    }
    public void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        anim.SetTrigger("Jump");
        SpawnGhost();
        if (jumpSoundSource != null) // เล่นเสียงกระโดดเมื่อกระโดดจากพื้น
        {
            jumpSoundSource.Play();
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, 0.1f, LayerMask.GetMask("Ground"));
        return raycastHit.collider != null;
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, new Vector2(transform.localScale.x, 0), 0.1f, walllayer);
        return raycastHit.collider != null;
    }


    private IEnumerator DashRoutine()
    {
        canDash = false;
        isDashing = true;
        anim.SetTrigger("Dash");


        if (dashSoundSource != null)
        {
            dashSoundSource.Play();
        }

        float startTime = Time.time;

        while (Time.time < startTime + dashDuration && Input.GetKey(KeyCode.Mouse1))
        {
            Vector2 dashDirection = shootPoint.right.normalized;
            rb.velocity = new Vector2(dashDirection.x * dashSpeed, 0f);

            rb.AddForce(new Vector2(dashDirection.x * dashSpeed, 0f), ForceMode2D.Impulse);
            if (Time.time >= nextGhostTime)
            {
                SpawnGhost();
                nextGhostTime = Time.time + ghostSpawnRate;
            }

            yield return null;
        }

        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
    void SpawnGhost()
    {
        GameObject ghost = Instantiate(ghostPrefab, transform.position, Quaternion.identity);
        Ghost ghostScript = ghost.GetComponent<Ghost>();

        StartCoroutine(SetGhostSpriteDelayed(ghostScript));
    }

    IEnumerator SetGhostSpriteDelayed(Ghost ghostScript)
    {
        yield return null;
        ghostScript.SetSprite(bodyRenderer.sprite, bodyRenderer.color);
    }
    public void Flip()
    {
        faceRight = !faceRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void AirJump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        anim.SetTrigger("Jump");
        airJumpCount++;
        if (jumpSoundSource != null) // เล่นเสียงกระโดดเมื่อกระโดดกลางอากาศ
        {
            jumpSoundSource.Play();
        }
    }
}