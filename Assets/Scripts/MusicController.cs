using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioSource bass;
    public AudioSource leadHigh;
    public AudioSource lead;
    public AudioSource second;
    public AudioSource chords;
    public AudioSource drums;

    public bool includeSecond = false;
    public bool includeChords = false;
    public bool includeDrums = false;

    private int section = 3;
    private float lastTime;

    // Start is called before the first frame update
    void Start() {
        bass.mute = false;
        leadHigh.mute = false;
        lead.mute = false;
        second.mute = true;
        chords.mute = true;
        drums.mute = true;

        NextSection();
        lastTime = bass.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (bass.time < lastTime)
            NextSection();
        lastTime = bass.time;
    }

    public void AddInstrument(int opt1, int opt2, int opt3) {
        if (opt1 == 0 && !includeDrums) {
            includeDrums = true;
            return;
        }
        else if (opt1 == 1 && !includeChords) {
            includeChords = true;
            return;
        }
        else if (opt1 == 2 && !includeSecond) {
            includeSecond = true;
            return;
        }
        
        if (opt2 == 0 && !includeDrums) {
            includeDrums = true;
            return;
        }
        else if (opt2 == 1 && !includeChords) {
            includeChords = true;
            return;
        }
        else if (opt2 == 2 && !includeSecond) {
            includeSecond = true;
            return;
        }
        
        if (opt3 == 0 && !includeDrums) {
            includeDrums = true;
            return;
        }
        else if (opt3 == 1 && !includeChords) {
            includeChords = true;
            return;
        }
        else if (opt3 == 2 && !includeSecond) {
            includeSecond = true;
            return;
        }
    }

    void NextSection() {
        section++;
        if (section >= 4) section = 0;

        if (section == 0) {
            leadHigh.mute = false;
            lead.mute = !(includeSecond && includeChords && includeDrums);
            second.volume = 0.3f;
            second.mute = true;
            chords.mute = true;
            drums.mute = !includeDrums;
        } else if (section == 1) {
            leadHigh.mute = true;
            lead.mute = false;
            drums.mute = true;
            chords.mute = !includeChords;
            drums.mute = true;
        } else if (section == 2) {
            leadHigh.mute = false;
            lead.mute = !(includeSecond && includeChords && includeDrums);
            second.mute = !includeSecond;
            chords.mute = !includeChords;
            drums.mute = !includeDrums;
        } else if (section == 3) {
            leadHigh.mute = true;
            lead.mute = true;
            second.volume = 0.6f;
            second.mute = !includeSecond;
            chords.mute = true;
            drums.mute = !includeDrums;
        }
    }
}
