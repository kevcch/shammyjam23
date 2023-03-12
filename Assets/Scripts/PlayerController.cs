using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movement_speed = 2;

    Rigidbody2D rb;
    Animator animator;
    GameObject playerView;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerView = transform.Find("PlayerView").gameObject;
        animator = playerView.GetComponent<Animator>();
    }

    void Update()
    {
        float horizontal_input = Input.GetAxisRaw("Horizontal");
        float vertical_input = Input.GetAxisRaw("Vertical");
        animator.SetFloat("horizontal_input", horizontal_input);
        animator.SetFloat("vertical_input", vertical_input);
        rb.velocity = new Vector2(horizontal_input, vertical_input) * movement_speed;
        if(horizontal_input != 0 || vertical_input != 0) {
            animator.SetBool("Moving", true);
        }
        else {
            animator.SetBool("Moving", false);
        }
        if (horizontal_input > 0) {
            playerView.transform.localScale = new Vector3(
                1,
                playerView.transform.localScale.y,
                playerView.transform.localScale.z);
        }
        if (horizontal_input < 0) {
            playerView.transform.localScale = new Vector3(
                -1,
                playerView.transform.localScale.y,
                playerView.transform.localScale.z);
        }
    }
}
