using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movement_speed = 2;
    public float cooldown_time = 0.5f;

    private float cooldown = 0f;
    private bool facing_right = true;

    Rigidbody2D rb;
    Animator animator;
    GameObject playerView;
    Character playerChar;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerView = transform.Find("PlayerView").gameObject;
        animator = playerView.GetComponent<Animator>();
        playerChar = transform.Find("PlayerObject").gameObject.GetComponent<Character>();
    }

    void Update()
    {
        float horizontal_input;
        float vertical_input;

        if (playerChar.isAttacking) {
            horizontal_input = 0f;
            vertical_input = 0f;
        }
        else {
            horizontal_input = Input.GetAxisRaw("Horizontal");
            vertical_input = Input.GetAxisRaw("Vertical");
        }
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
            facing_right = true;
            playerView.transform.localScale = new Vector3(
                1,
                playerView.transform.localScale.y,
                playerView.transform.localScale.z);
        }
        if (horizontal_input < 0) {
            facing_right = false;
            playerView.transform.localScale = new Vector3(
                -1,
                playerView.transform.localScale.y,
                playerView.transform.localScale.z);
        }

        if (cooldown > 0f) {
            cooldown -= Time.deltaTime;
        }
        else if (Input.GetMouseButton(0)) {
            playerChar.Attack(facing_right);
            cooldown = cooldown_time;
        }
    }
}
