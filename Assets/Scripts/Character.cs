using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public GameObject musicObject;
    public GameObject healthBarObject;
    
    public AudioSource clipPickup;
    public AudioSource clipAttack;
    public AudioSource clipHurt;

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
    private bool hasBomb = true;
    private bool hasBow = false;
    private bool hasFan = false;

    private GameObject weapon;
    private MusicController music;
    private GameObject healthBar;
    private GameObject powerUp;

    // Start is called before the first frame update
    void Start() {
        health = maxHealth;
        weapon = Resources.Load(
            "Prefabs/Weapon", typeof(GameObject)) as GameObject;
        music = musicObject.GetComponent<MusicController>();
        healthBar = healthBarObject.transform.Find("Image").gameObject;
        powerUp = Resources.Load("Prefabs/Powerup", typeof(GameObject)) as GameObject;
    }

    // Update is called once per frame
    void Update() {
        if (isPlayer) {
            if (Random.Range(0f, 1f) < 0.01f) {
                Heal(1);
            }
        }
    }

    public void SetShielded(bool shielded) {
        isShielded = shielded;
    }

    public bool AddUpgrade(string upgrade) {
        if (upgradeCount >= maxUpgrades) return false;

        switch (upgrade) {
            case "sword":
                hasSword = true;
                music.AddInstrument(2, 1, 0);
                break;
            case "bull":
                hasHorns = true;
                music.AddInstrument(0, 1, 2);
                break;
            case "shield":
                hasShield = true;
                music.AddInstrument(1, 0, 2);
                break;
            case "eye":
                hasVamp = true;
                music.AddInstrument(2, 1, 0);
                break;
            case "bomb":
                hasBomb = true;
                music.AddInstrument(0, 2, 1);
                break;
            case "bow":
                hasBow = true;
                music.AddInstrument(2, 1, 0);
                break;
            case "fan":
                hasFan = true;
                music.AddInstrument(1, 2, 0);
                break;
            default:
                return false;
        }

        clipPickup.Play();

        upgradeCount++;
        return true;
    }

    void UpdateHealthBar() {
        float scaleX = health / maxHealth;
        healthBar.transform.localScale = new Vector3(scaleX, 1f, 1f);
    }

    public void Death() {
        Debug.Log("Dead");
        string[] choices = {"sword", "shield", "bull", "eye", "bomb"};
        float r = Random.Range(0f, 1f);
        if (r <= 0.5f) {
            int index = Random.Range(0, 4);
            GameObject powerUpObject = Instantiate(powerUp);
            powerUpObject.transform.position = transform.position;
            powerUpObject.GetComponent<Powerup>().SetWeapon(choices[index]);
        }
        Destroy(gameObject);
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

        clipAttack.pitch = Random.Range(0.8f, 1.0f);
        clipAttack.Play();

        // GameObject explosion = Resources.Load(
        //     "Prefabs/Explosion", typeof(GameObject)) as GameObject;
        // Instantiate(explosion);
    }

    public void Damage(float amount) {
        if (!isShielded) {
            health -= amount;
            if (clipHurt) clipHurt.Play();
            if (health <= 0) Death();
            UpdateHealthBar();
        }
        // bool a = hasBow;
        // a = hasFan;
        // a = hasHorns;
        // Debug.Log("Took damage.");
    }

    public void Heal(float amount) {
        health += amount;
        if (health > maxHealth) health = maxHealth;
        UpdateHealthBar();
    }
}
