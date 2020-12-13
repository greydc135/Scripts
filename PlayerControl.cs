using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Animator animator;
    public GameObject crosshair;

    public bool canMove;

    public GameObject fireballPrefab;
    float fireElapsedTime = 0;
    public float fireDelay = 0.5f;

    private Camera cam;

    // called before the first frame of the game
    void Start()
    {
        cam = Camera.main;
        canMove = true;
    }

    // called every frame
    void Update()
    {
        Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0.0f);

        // Variables for Mouse input
        Vector3 mouse = Input.mousePosition;
        Vector3 screenPoint = cam.WorldToScreenPoint(transform.localPosition);
        Vector2 offset = new Vector2(mouse.x - screenPoint.x, mouse.y - screenPoint.y);

        offset.Normalize();
        // if player presses left mouse, fire!
        fireElapsedTime += Time.deltaTime;
        if(Input.GetButtonDown("Fire1"))
        {
            canMove = false;
        }
        if(Input.GetButtonUp("Fire1"))
        {
            if(fireElapsedTime >= fireDelay)
            {
                GameObject fireball = Instantiate(fireballPrefab, transform.position, Quaternion.identity);
                fireball.GetComponent<Rigidbody2D>().velocity = offset * 3.0f;
                fireball.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg);
                Destroy(fireball, 2.0f);
                fireElapsedTime = 0;
            }
            canMove = true;
        }

        // Variables for Player movement
        if(canMove)
        {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            transform.position = transform.position + movement * Time.deltaTime;
        }

        // Variables for Aim
        animator.SetFloat("AimX", offset.x);
        animator.SetFloat("AimY", offset.y);
        animator.SetBool("Aim", Input.GetButton("Fire1"));

        if(Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            animator.SetFloat("LastMoveX", Input.GetAxisRaw("Horizontal"));
            animator.SetFloat("LastMoveY", Input.GetAxisRaw("Vertical"));
        }
    }
}
