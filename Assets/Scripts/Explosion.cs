using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float damage = 10f;

    public AudioSource clipExplosion;

    private float time = 0f;

    // Start is called before the first frame update
    void Start() {
        clipExplosion.pitch = Random.Range(0.8f, 1f);
        clipExplosion.Play();
    }

    // Update is called once per frame
    void Update() {
        time += Time.deltaTime;
        if (time >= 0.315f) {
            Destroy(gameObject);
            Debug.Log("End explosion");
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        GameObject victim = collision.gameObject;

        if (victim.tag == "Player") {
            Character victimData = victim.transform.Find("PlayerObject")
                .GetComponent<Character>();
            victimData.Damage(damage);
        }
        else if (victim.tag == "Enemy") {
            Character victimData = victim.GetComponent<Character>();
            victimData.Damage(damage);
        }
    }
}
