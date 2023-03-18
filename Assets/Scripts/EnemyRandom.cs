using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class EnemyRandom : Character
{
    string dropItem;

    protected override void Start() {
        base.Start();

        float r = Random.Range(0f, 1f);
        if (r < 0.25) {
            hasSword = true;
            dropItem = "sword";
        }
        else if (r < 0.5) {
            hasShield = true;
            dropItem = "shield";
        }
        else if (r < 0.7) {
            hasVamp = true;
            dropItem = "eye";
        }
        else if (r < 0.9) {
            hasHorns = true;
            dropItem = "bull";
        }
        else {
            hasBomb = true;
            dropItem = "bomb";
        }

        Debug.Log(health);
    }

    public override void Death() {
        GameObject powerUpObject = Instantiate(powerUpPF);
        powerUpObject.transform.position = transform.position;
        powerUpObject.GetComponent<Powerup>().SetWeapon(dropItem);
        Destroy(gameObject);
    }
}