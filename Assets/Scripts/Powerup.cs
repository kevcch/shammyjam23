using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public Sprite bombSprite;
    public Sprite bowSprite;
    public Sprite bullSprite;
    public Sprite eyeSprite;
    public Sprite shieldSprite;
    public Sprite swordSprite;

    private GameObject sprite;
    private SpriteRenderer spriteRender;

    private string weapon;

    private float time = 0f;

    // Start is called before the first frame update
    
    void Start() {
        sprite = transform.Find("sprite").gameObject;
        spriteRender = sprite.GetComponent<SpriteRenderer>();
        Debug.Log(sprite);
    }
    

    // Update is called once per frame
    void Update() {
        time += Time.deltaTime;
        float yOffset = Mathf.Sin(time) / 10;
        sprite.transform.position = transform.position + new Vector3(0f, yOffset, 0f);
    }

    public void SetWeapon(string weaponType) {
        sprite = transform.Find("sprite").gameObject;
        spriteRender = sprite.GetComponent<SpriteRenderer>();
        weapon = weaponType;

        switch (weaponType) {
            case "bomb":
                spriteRender.sprite = bombSprite;
                break;
            case "bow":
                spriteRender.sprite = bowSprite;
                break;
            case "bull":
                spriteRender.sprite = bullSprite;
                break;
            case "eye":
                spriteRender.sprite = eyeSprite;
                break;
            case "shield":
                spriteRender.sprite = shieldSprite;
                break;
            case "sword":
                spriteRender.sprite = swordSprite;
                break;
            default:
                spriteRender.sprite = swordSprite;
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        GameObject player = collision.gameObject;
        if (!(player.tag == "Player")) return;
        Debug.Log("Gained " + weapon);

        PlayerCharacter playerData = player.transform.Find("PlayerObject")
            .gameObject.GetComponent<PlayerCharacter>();
        if (playerData.AddUpgrade(weapon)) {
            Destroy(gameObject);
        }
    }
}
