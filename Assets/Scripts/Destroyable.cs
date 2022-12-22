using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour
{
    public float health = 100;
    public float maxHealth;
    public GameObject coin;
    public GameObject particle;
    public AudioSource aud;
    public AudioClip destroyedSound;

    private void Start()
    {
        aud = GameObject.Find("Global Audio").GetComponent<AudioSource>();
        maxHealth = health;
    }

    public void decHealth(float amount)
    {
        health -= amount;
    }

    private void Update()
    {
        if (health <= 0)
        {
            aud.volume = 1;
            aud.PlayOneShot(destroyedSound);
            Destroy(gameObject);
            Instantiate(particle, transform.position, Quaternion.identity);
            Instantiate(coin, transform.position, Quaternion.identity);
        }
    }

}
