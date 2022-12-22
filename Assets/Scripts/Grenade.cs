using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{

    private bool explode;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        explode = false;
        time = 1;
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        transform.Translate(2 * Time.deltaTime * Vector3.up);
        if (time < 0)
        {
            explode = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (explode)
        {
            if (collision.gameObject.tag == "Destroyable")
            {
                collision.gameObject.GetComponent<Destroyable>().decHealth(75);
            }
            Destroy(gameObject);
        }
    }
}
