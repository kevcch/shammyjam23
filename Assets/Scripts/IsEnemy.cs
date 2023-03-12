using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class IsEnemy : MonoBehaviour
{

    GameObject enemyView;
    public float movement_speed = 1;
    private char direction_facing;
    private int direction;
    private float dirX;
    private float dirY;


    private int tiles_to_travel;
    private float tiles_travelled = 0;
    private Vector2 last_position;
    Rigidbody2D rb;
    public bool isMovementDisabled = false;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        enemyView = transform.Find("EnemyView").gameObject;
        animator = enemyView.GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        last_position = transform.position;
        RandomizeDirection();
        RandomizeNumberOfTilesToMove();
    }

    // Update is called once per frame
    void Update()
    {
        //Move 
        tiles_travelled += Vector2.Distance(transform.position, last_position);
        last_position = transform.position;
        if (tiles_travelled >= tiles_to_travel)
        {
            tiles_travelled = 0;
            rb.velocity = Vector2.zero;
            RandomizeDirection();
            RandomizeNumberOfTilesToMove();
            isMovementDisabled = true;
            StartCoroutine(PauseWalk());

        }
        if (isMovementDisabled){
            rb.velocity = Vector2.zero;
            animator.SetBool("Moving", false);
        }
        else {
            rb.velocity = (new Vector2(dirX, dirY) * movement_speed);
            animator.SetBool("Moving", true);
        }

    }

    private void Death()
    {
        //Drop pickup randomly
        Destroy(gameObject);
    }

    void RandomizeDirection()
    {
        // Debug.Log("Direction Randomized");
        int num = Random.Range(0, 4);
        
        if (num == 0)
        {
            direction_facing = 'N';
            direction = 0;
            dirX = 0f;
            dirY = 1f;
        }
        else if (num == 1)
        {
            direction_facing = 'S';
            direction = 2;
            dirX = 0f;
            dirY = -1f;
        }
        else if (num == 2)
        {
            direction_facing = 'E';
            direction = 1;
            dirX = 1f;
            dirY = 0f;
        }
        else
        {
            direction_facing = 'W';
            direction = 3;
            dirX = -1f;
            dirY = 0f;
        }

    }

    void RandomizeNumberOfTilesToMove()
    {
        tiles_to_travel = Random.Range(1, 6);
    }

    IEnumerator PauseWalk() {
        yield return new WaitForSeconds(1.0f);
        isMovementDisabled = false;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        RandomizeDirection();
    }
}
