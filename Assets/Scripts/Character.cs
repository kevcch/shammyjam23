using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public AudioSource clipPickup;

    public float maxHealth = 100f;
    public bool isPlayer = false;
    public int maxUpgrades = 3;

    private float health;
    private bool isShielded;
    public bool isAttacking = false;
    public bool isDashing = false;

    private int upgradeCount = 0;
    private bool hasSword = false;
    private bool hasHorns = false;
    private bool hasShield = false;
    private bool hasVamp = false;
    private bool hasBomb = false;
    private bool hasBow = false;
    private bool hasFan = false;

    private GameObject weapon;

    // Start is called before the first frame update
    void Start() {
        health = maxHealth;
        weapon = Resources.Load(
            "Prefabs/Weapon", typeof(GameObject)) as GameObject;
    }

    // Update is called once per frame
    void Update() {

    }

    public void SetShielded(bool shielded) {
        isShielded = shielded;
    }

    public bool AddUpgrade(string upgrade) {
        if (upgradeCount >= maxUpgrades) return false;

        switch (upgrade) {
            case "sword":
                hasSword = true;
                break;
            case "bull":
                hasHorns = true;
                break;
            case "shield":
                hasShield = true;
                break;
            case "eye":
                hasVamp = true;
                break;
            case "bomb":
                hasBomb = true;
                break;
            case "bow":
                hasBow = true;
                break;
            case "fan":
                hasFan = true;
                break;
            default:
                return false;
        }

        clipPickup.Play();

        upgradeCount++;
        return true;
    }

    public void Attack(bool facingRight) {
        float offset = 0.7f;
        if (!facingRight) offset = -offset;

        GameObject attack = Instantiate(weapon);
        attack.transform.position = transform.position + new Vector3(offset, 0f, 0f);
        Weapon attackData = attack.GetComponent<Weapon>();
        attackData.SetAttributes(
            this, isPlayer, facingRight,
            hasSword, hasShield, hasBomb, hasVamp, hasHorns);

        // GameObject explosion = Resources.Load(
        //     "Prefabs/Explosion", typeof(GameObject)) as GameObject;
        // Instantiate(explosion);
    }

    public void Damage(float amount) {
        if (!isShielded) {
            health -= amount;
        }
        bool a = hasBow;
        a = hasFan;
        a = hasHorns;
        Debug.Log("Took damage.");
    }

    public void Heal(float amount) {
        health += amount;
        if (health > maxHealth) health = maxHealth;
    }
}
