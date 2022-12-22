using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float damage;
    public float time;

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        transform.Translate(speed * Time.deltaTime * Vector3.up);
        if (time < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {  
        if (collision.gameObject.tag == "Destroyable")
        {
            collision.gameObject.GetComponent<Destroyable>().decHealth(damage);
            Destroy(gameObject);
            
        } 
        
    }
}
