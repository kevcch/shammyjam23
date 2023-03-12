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

    private Color colorNormal = new Color(1.0f, 1.0f, 1.0f);
    private Color colorStrong = new Color(0.4f, 1.0f, 1.0f);
    private Color colorFire = new Color(1.0f, 0.3f, 0.3f);
    private Color colorShield = new Color(0.7f, 0.7f, 0.0f);

    private bool isSword = false;
    private bool isShield = false;
    private bool isBomb = false;
    private bool isVampire = false;

    private bool facingRight = true;

    private float time = 0f;

    // Start is called before the first frame update
    void Start() {
        slashSprite = transform.Find("slash_0").gameObject.GetComponent<SpriteRenderer>();
        explosion = Resources.Load(
            "Prefabs/Explosion.prefab", typeof(GameObject)) as GameObject;
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
        time += Time.deltaTime;
        if (time >= 0.5f) {
            Destroy(gameObject);
            Debug.Log("End attack");
            actor.isAttacking = false;

            // if the player is shielded, remove it here
            if (isShield) actor.SetShielded(false);
        }
    }

    public void SetAttributes(Character attacker, bool attackerIsPlayer, bool right,
                       bool sword, bool shield, bool bomb, bool vampire) {
        actor = attacker;
        playerCreated = attackerIsPlayer;
        actor.isAttacking = true;

        facingRight = right;

        isSword = sword;
        isShield = shield;
        isBomb = bomb;
        isVampire = vampire;
    }
    
    // If this was player created, damage all objects with the "Enemy" tag
    // colliding with this object.
    // If this was NOT player created, damage the player if they collide.
    void OnCollisionEnter(Collision collision) {
        Character victim = collision.gameObject.GetComponent<Character>();
        if (victim == null) return;

        if (playerCreated && victim.tag == "Enemy") {
            Debug.Log("Collision with enemy.");
            Attack(victim);
        }

        else if (!playerCreated && victim.tag == "Player") {
            Debug.Log("Collision with player.");
            Attack(victim);
        }
    }

    void Attack(Character defender) {
        defender.Damage(damage);

        if (isVampire) {
            actor.Heal(damage * 0.3f);
        }

        if (isBomb) {
            Instantiate(explosion, transform.position, transform.rotation);
            isBomb = false; // only explode once per attack
        }
    }
}
