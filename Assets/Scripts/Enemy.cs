using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float speed;
    private GameObject player;
    private CharacterController playerCont;
    private Rigidbody2D rb;
    public GameObject bullet;
    public Sprite[] skins;
    public SpriteRenderer spr;
    public Destroyable destroyable;
    public float timeGun;
    private float timeGunx;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        playerCont = player.GetComponent<CharacterController>();
        timeGunx = timeGun;
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerCont.isPause)
        {
            Vector3 target = (player.transform.position - rb.transform.position).normalized;
            rb.MovePosition(rb.transform.position + target * speed * Time.fixedDeltaTime);
            //transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            if (destroyable.health > destroyable.maxHealth * 5/6)
            {
                spr.sprite = skins[0];
            } else if (destroyable.health > destroyable.maxHealth * 4/6)
            {
                spr.sprite = skins[1];
            }
            else if (destroyable.health > destroyable.maxHealth * 3 / 6)
            {
                spr.sprite = skins[2];
            }
            else if (destroyable.health > destroyable.maxHealth * 2 / 6)
            {
                spr.sprite = skins[3];
            }
            else if (destroyable.health > destroyable.maxHealth * 1 / 6)
            {
                spr.sprite = skins[4];
            }
        }
        
    }
}
