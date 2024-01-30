using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D body;

    public Transform playerSpawn;

    public GameObject feetPosition;
    public LayerMask floorMask;

    public float maxSpeed;
    public float acceleration;

    private float inputX;

    public float jumpForce;
    public float coyoteTime;
    public float airMomentumPenalization;
    private float airTime;

    private bool ascending;
    [Range(0, 1)] public float airStopCoefficient;

    private bool jetpack;
    private bool dynamite;

    public float stunDuration;
    public float respawnCooldown;
    private float disableTimer;
    private bool isEnabled;

    public GameObject throwable;
    public Transform throwPoint;
    public float throwForce;

    public GameObject flashlight;

    public UnityEvent onDeath = new UnityEvent();

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        transform.position = playerSpawn.position;
        isEnabled = true;

        onDeath.AddListener(Respawn);
        onDeath.AddListener(GameObject.Find("Level Manager").GetComponent<LevelManager>().ClearProjectiles);
        onDeath.AddListener(GameObject.Find("Game Manager").GetComponent<GameManager>().SubstractLife);
    }

    private void Update()
    {
        airTime += Time.deltaTime;

        foreach (Collider2D collider in FindColliders())
        {
            if (collider.tag == "Ground" || collider.tag == "Box")
                airTime = 0;
        }
        if (isEnabled)
        {
            inputX = Input.GetAxisRaw("Horizontal");

            if (inputX < 0)
                transform.rotation = Quaternion.Euler(0, 180, 0);
            else if (inputX > 0)
                transform.rotation = Quaternion.Euler(0, 0, 0);

            if (Input.GetKey(KeyCode.W))
            {
                if (airTime < coyoteTime || (jetpack && !ascending))
                    Jump();
            }
            else if (body.velocity.y > 0 && ascending)
            {
                body.AddForce(new Vector2(0, -body.velocity.y * airStopCoefficient), ForceMode2D.Impulse);
                ascending = false;
            }

            if (body.velocity.y < 0 && !Input.GetKey(KeyCode.W))
            {
                ascending = false;
            }
        }
        else
        {
            if (disableTimer > 0)
            {
                disableTimer -= Time.deltaTime;
            }
            else
                Recover();
        }

        if (Input.GetKeyDown(KeyCode.F) && dynamite)
        {
            Throw();
        }

        flashlight.transform.rotation = Quaternion.LookRotation(Vector3.forward, Camera.main.ScreenToWorldPoint(Input.mousePosition) - flashlight.transform.position);
    }

    private void FixedUpdate()
    {
        float speedModifier = 0;

        foreach (Collider2D foundCollider in FindColliders())
        {
            SurfaceEffector2D foundEffector = foundCollider.GetComponent<SurfaceEffector2D>();
            speedModifier += foundEffector != null ? foundEffector.speed : foundCollider.tag == "Box" ? foundCollider.attachedRigidbody.velocity.x : 0;
        }

        if (isEnabled)
        {
            float desiredSpeed = inputX * maxSpeed + speedModifier;
            float deltaSpeed = Mathf.Clamp(desiredSpeed - body.velocity.x, -acceleration, acceleration);
            float forceX = body.mass * deltaSpeed;
            if (airTime > 0)
                forceX /= airMomentumPenalization;
            body.AddForce(new Vector2(forceX, 0f), ForceMode2D.Impulse);
        }
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, jumpForce);
        airTime = coyoteTime;
        ascending = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Bomb":
                Destroy(collision.gameObject);
                dynamite = true;
                break;

            case "Jetpack":
                Destroy(collision.gameObject);
                jetpack = true;
                break;

            case "Flashlight":
                Destroy(collision.gameObject);
                flashlight.SetActive(true);
                break;

            case "Enemy":
                onDeath.Invoke();
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        switch (collision.gameObject.tag)
        {
            case "Ball":
                Disable(stunDuration);
                break;
            case "Box":
                if (collision.rigidbody.velocity.y < -2 && body.velocity.y > 0)
                    Disable(stunDuration);
                break;
            case "Enemy":
                onDeath.Invoke();
                break;
        }
    }

    Collider2D[] FindColliders()
    {
        return Physics2D.OverlapBoxAll(feetPosition.transform.position, new Vector2(0.65f, 0.0f), 0f, floorMask);
    }

    public void Disable(float disableDuration)
    {
        body.freezeRotation = false;
        isEnabled = false;
        disableTimer = disableDuration;
    }

    private void Recover()
    {
        body.freezeRotation = true;
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0);
        isEnabled = true;
    }

    private void Throw()
    {
        Instantiate(throwable, throwPoint.position, throwPoint.rotation).GetComponent<Rigidbody2D>().AddForce(transform.right * throwForce, ForceMode2D.Impulse);
    }

    private void Respawn()
    {
        Disable(respawnCooldown);
        transform.position = playerSpawn.position;
        body.velocity = Vector2.zero;
        jetpack = false;
        dynamite = false;
        flashlight.SetActive(false);
    }
}