using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public AudioSource audioSource;
    // public GameObject healthBarObject;
    
    protected AudioClip clipAttack;
    protected AudioClip clipHurt;

    public float maxHealth = 100f;
    public int maxUpgrades = 3;

    protected float health;
    protected bool isShielded;
    protected bool isAttacking = false;
    protected bool isDashing = false;
    protected bool isPlayer = false;

    protected int upgradeCount = 0;
    protected bool hasSword = false;
    protected bool hasHorns = false;
    protected bool hasShield = false;
    protected bool hasVamp = false;
    protected bool hasBomb = false;
    protected bool hasBow = false;
    protected bool hasFan = false;

    // loaded prefabs
    protected GameObject weaponPF;      // Prefabs/Weapon
    protected GameObject healthBarPF;   // Prefabs/HealthBar
    protected GameObject powerUpPF;

    protected MusicController music;

    protected GameObject healthBarObject;
    protected GameObject healthBar;

    // Start is called before the first frame update
    protected virtual void Start() {
        // load prefabs from Resources
        weaponPF = Resources.Load(
            "Prefabs/Weapon", typeof(GameObject)) as GameObject;
        healthBarPF = Resources.Load(
            "Prefabs/HealthBar", typeof(GameObject)) as GameObject;
        powerUpPF = Resources.Load(
            "Prefabs/Powerup", typeof(GameObject)) as GameObject;

        // load audio from Resources
        clipAttack = Resources.Load<AudioClip>("Audio/sword/sword_retro");
        clipHurt = Resources.Load<AudioClip>("Audio/hurt");
        
        // create a health bar with the same location as this Game Object
        healthBarObject = Instantiate(healthBarPF, gameObject.transform);
        // healthBarObject.transform.parent = gameObject.transform;
        healthBar = healthBarObject.transform.Find("Image").gameObject;
        
        // start at full health
        health = maxHealth;
    }

    protected void UpdateHealthBar() {
        float scaleX = health / maxHealth;
        healthBar.transform.localScale = new Vector3(scaleX, 1f, 1f);
    }

    // TODO: use a "state" variable instead of independent "is___" variables
    public bool IsAttacking() { return isAttacking; }
    public bool IsDashing() { return isDashing; }

    public void SetAttacking(bool attacking) {
        isAttacking = attacking;
    }

    public void SetDashing(bool dashing) {
        isDashing = dashing;
    }

    public void SetShielded(bool shielded) {
        isShielded = shielded;
    }

    public virtual void Death() {
        // Debug.Log("Dead");
        // string[] choices = {"sword", "shield", "bull", "eye", "bomb"};
        // float r = Random.Range(0f, 1f);
        // if (r <= 0.5f) {
        //     int index = Random.Range(0, 4);
        //     GameObject powerUpObject = Instantiate(powerUpPF);
        //     powerUpObject.transform.position = transform.position;
        //     Debug.Log(powerUpObject.transform.position);
        //     powerUpObject.GetComponent<Powerup>().SetWeapon(choices[index]);
        // }
        Destroy(gameObject);
    }

    public void Attack(bool facingRight) {
        float offset = 0.7f;
        if (!facingRight) offset = -offset;

        GameObject attack = Instantiate(weaponPF, gameObject.transform);
        attack.transform.position += new Vector3(offset, 0f, 0f);
        Weapon attackData = attack.GetComponent<Weapon>();
        attackData.SetAttributes(
            this, isPlayer, facingRight,
            hasSword, hasShield, hasBomb, hasVamp, hasHorns);

        // clipAttack.pitch = Random.Range(0.8f, 1.0f);
        // clipAttack.Play();
        audioSource.PlayOneShot(clipAttack);
    }

    public void Damage(float amount) {
        if (!isShielded) {
            health -= amount;
            // if (clipHurt) clipHurt.Play();
            audioSource.PlayOneShot(clipHurt);
            if (health <= 0) Death();
            UpdateHealthBar();
        }
    }

    public void Heal(float amount) {
        health += amount;
        if (health > maxHealth) health = maxHealth;
        UpdateHealthBar();
    }
}
