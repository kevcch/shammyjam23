using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float damage;
    public int direction;

    public bool playerCreated;

    public GameObject slash;

    private SpriteRenderer slashSprite;

    // private Color colorGreen = new Color(100, 255, 255, 255);
    private Color colorGreen = new Color(0.4f, 1.0f, 1.0f);
    private Color colorRed = new Color(1.0f, 0.3f, 0.3f);

    private float time = 0f;

    // Start is called before the first frame update
    void Start() {
        slashSprite = slash.GetComponent<SpriteRenderer>();
        slashSprite.color = colorGreen;
    }

    // Update is called once per frame
    void Update() {
        // slashSprite.color = colorGreen;
        time += Time.deltaTime;
        if (time >= 0.5f) {
            Destroy(gameObject);
            Debug.Log("End attack");
        }
    }
    
    // If this was player created, damage all objects with the "Enemy" tag
    // colliding with this object.
    // If this was NOT player created, damage the player if they collide.
    void OnCollisionEnter(Collision collision) {
        if (playerCreated && collision.gameObject.tag == "Enemy") {
            Debug.Log("Collision with enemy.");
        }
    }
}
