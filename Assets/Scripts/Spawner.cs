using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject Enemy;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        time = 1;
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if(time < 0)
        {
            Instantiate(Enemy, new Vector3(transform.position.x + 1, transform.position.y + Random.Range(-3, 3), 0), Quaternion.identity);
            time = 5;
        }
    }
}
