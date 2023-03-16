using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerCharacter : Character
{
    public GameObject musicObject;

    private AudioClip clipPickup;

    protected override void Start() {
        base.Start();

        // load audio from Resources
        clipPickup = Resources.Load<AudioClip>("Audio/pickup");

        // TODO: find the Dynamic Music controller globally
        if (musicObject)
            music = musicObject.GetComponent<MusicController>();

        isPlayer = true;
    }

    void Update() {
        if (Random.Range(0f, 1f) < 0.01f) Heal(1);
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

        // clipPickup.Play();
        audioSource.PlayOneShot(clipPickup);

        upgradeCount++;
        return true;
    }
}