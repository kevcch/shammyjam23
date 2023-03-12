using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float maxHealth = 100f;
    public bool isPlayer = false;
    public int maxUpgrades = 3;

    private float health;
    private bool isShielded;
    public bool isAttacking = false;

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

    public void AddUpgrade(int upgrade) {
        if (upgradeCount >= maxUpgrades) return;

        switch (upgrade) {
            case 0:
                hasSword = true;
                break;
            case 1:
                hasHorns = true;
                break;
            case 2:
                hasShield = true;
                break;
            case 3:
                hasVamp = true;
                break;
            case 4:
                hasBomb = true;
                break;
            case 5:
                hasBow = true;
                break;
            case 6:
                hasFan = true;
                break;
            default:
                return;
        }

        upgradeCount++;
    }

    public void Attack(bool facingRight) {
        float offset = 0.7f;
        if (!facingRight) offset = -offset;

        Weapon attack = Instantiate(weapon).GetComponent<Weapon>();
        attack.transform.position = transform.position + new Vector3(offset, 0f, 0f);
        attack.SetAttributes(
            this, isPlayer, facingRight,
            hasSword, hasShield, hasBomb, hasVamp);
    }

    public void Damage(float amount) {
        if (!isShielded) {
            health -= amount;
        }
        bool a = hasBow;
        a = hasFan;
        a = hasHorns;
    }

    public void Heal(float amount) {
        health += amount;
        if (health > maxHealth) health = maxHealth;
    }
}
