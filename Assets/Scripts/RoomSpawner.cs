using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int openingDirection;
    public bool spawned = false;
    // 1 = need bottom, 2 == need top, 3 = need left, 4 = need right

    private RoomTemplates templates;

    private void Start() {
        Invoke("SpawnRooms", 1.0f);
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        StartCoroutine(destroySpawnpoint());
    }
    
    void SpawnRooms()
    {
        int rand;
        if (!spawned && templates.num_rooms < templates.max_rooms) {
            templates.num_rooms++;
            switch (openingDirection) {
                case 1: //Bottom
                    rand = Random.Range(0, templates.bottomRooms.Length);
                    Instantiate(templates.bottomRooms[rand], transform.position, templates.bottomRooms[rand].transform.rotation);
                    break;
                case 2: //Top
                    rand = Random.Range(0, templates.topRooms.Length);
                    Instantiate(templates.topRooms[rand], transform.position, templates.topRooms[rand].transform.rotation);
                    break;
                case 3: //Left
                    rand = Random.Range(0, templates.leftRooms.Length);
                    Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation);
                    break;
                case 4: //Right
                    rand = Random.Range(0, templates.rightRooms.Length);
                    Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation);
                    break;
            }
            spawned = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("Despawning spawn point");
        if (other.CompareTag("Spawnpoint") ) {
            Destroy(gameObject);
        }
        if (other.CompareTag("RoomTrigger"))
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        spawned = true;
        Debug.Log("Despawning spawn point");
        if (collision.CompareTag("Spawnpoint"))
        {
            Destroy(gameObject);
        }
        if (collision.CompareTag("RoomTrigger"))
        {
            Destroy(gameObject);
        }
    }
    IEnumerator destroySpawnpoint() {
        yield return new WaitForSeconds(5.0f);
        Destroy(gameObject);
    }
}
