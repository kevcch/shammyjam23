using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsEnemy : MonoBehaviour
{

    GameObject enemyView;

    // Start is called before the first frame update
    void Start()
    {
        enemyView = transform.Find("EnemyView").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
