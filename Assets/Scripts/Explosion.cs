using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float damage = 10f;

    private float time = 0f;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        time += Time.deltaTime;
        if (time >= 0.75f) {
            Destroy(gameObject);
            Debug.Log("End explosion");
        }
    }

    void OnCollisionEnter(Collision collision) {
        Character victim = collision.gameObject.GetComponent<Character>();
        if (victim == null) return;
        
        victim.Damage(damage);
    }
}
