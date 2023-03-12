using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public bool playerCreated;
    public float damage = 20f;

    private Character actor;
    private GameObject explosion;
    private SpriteRenderer slashSprite;

    // private Color colorNormal = new Color(1.0f, 1.0f, 1.0f);
    private Color colorNormal = new Color(1.0f, 0.85f, 0.9f);
    private Color colorStrong = new Color(0.4f, 1.0f, 1.0f);
    private Color colorFire = new Color(1.0f, 0.3f, 0.3f);
    private Color colorShield = new Color(1f, 0.9f, 0.2f);

    private bool isSword = false;
    private bool isShield = false;
    private bool isBomb = false;
    private bool isVampire = false;
    private bool isHorns = false;

    private bool facingRight = true;
    private Vector3 offset;

    private float time = 0f;

    // Start is called before the first frame update
    void Start() {
        slashSprite = transform.Find("attack_0").gameObject.GetComponent<SpriteRenderer>();
        explosion = Resources.Load("Prefabs/Explosion", typeof(GameObject)) as GameObject;
        slashSprite.color = colorNormal;

        slashSprite.flipX = !facingRight;

        if (isSword) {
            slashSprite.color = colorStrong;
            damage = 50f;
        }

        if (isShield) {
            slashSprite.color = colorShield;
            actor.SetShielded(true);
        }
    }

    // Update is called once per frame
    void Update() {
        transform.position = actor.transform.position + offset;
        time += Time.deltaTime;
        if (time >= 0.3f) {
            Destroy(gameObject);
            Debug.Log("End attack");
            actor.isAttacking = false;
            actor.isDashing = false;

            // if the player is shielded, remove it here
            if (isShield) actor.SetShielded(false);
        }
    }

    public void SetAttributes(
            Character attacker, bool attackerIsPlayer, bool right,
            bool sword, bool shield, bool bomb, bool vampire, bool horns) {
        actor = attacker;
        playerCreated = attackerIsPlayer;

        facingRight = right;

        isSword = sword;
        isShield = shield;
        isBomb = bomb;
        isVampire = vampire;
        isHorns = horns;

        if (isHorns) actor.isDashing = true;
        else actor.isAttacking = true;

        offset = transform.position - actor.transform.position;
    }
    
    // If this was player created, damage all objects with the "Enemy" tag
    // colliding with this object.
    // If this was NOT player created, damage the player if they collide.
    void OnTriggerEnter2D(Collider2D collision) {
        GameObject victim = collision.gameObject;

        Debug.Log(victim);

        if (playerCreated && victim.tag == "Enemy") {
            Debug.Log("Collision with enemy.");
            Attack(victim.GetComponent<Character>());
        }

        else if (!playerCreated && victim.tag == "Player") {
            Debug.Log("Collision with player.");
            Attack(victim.transform.Find("PlayerObject").GetComponent<Character>());
        }
    }

    void Attack(Character defender) {
        defender.Damage(damage);

        if (isVampire) {
            actor.Heal(damage * 0.3f);
        }

        if (isBomb) {
            // Instantiate(explosion, transform.position, transform.rotation);
            GameObject bomb = Instantiate(explosion);
            bomb.transform.position = transform.position;
            isBomb = false; // only explode once per attack
        }
    }
}
