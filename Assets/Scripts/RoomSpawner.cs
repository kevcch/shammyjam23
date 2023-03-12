using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int openingDirection;
    public bool spawned = false;
    // 1 = need bottom, 2 == need top, 3 = need left, 4 = need right

    private void Start() {
        Invoke("SpawnRooms", 0.1f);
    }
    // Update is called once per frame
    void SpawnRooms()
    {
        if (!spawned) {
            switch (openingDirection) {
                case 1:
                    //int rand = Random.RandomRange(0, bottomRooms.Length);

                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
            }
            spawned = true;
        }
    }
}
