using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.GraphicsBuffer;

public class CharacterController : MonoBehaviour
{

    public bool isPause;

    public float speed = 10;
    public GameObject bullet;
    public TextMeshProUGUI healthTxt;
    public TextMeshProUGUI levelTxt;
    public GameObject spawner;
    public GameObject grenade;
    public SpriteRenderer spr;
    public Slider experienceUI;
    public Sprite[] skins;
    public Transform gunPoint;
    public float health;
    public GameObject particle_LevelUp;
    public AudioSource aud;
    public AudioClip collectExp_aud;
    public AudioClip levelUp_aud;
    public AudioClip heartBeat_aud;
    public Animator loseOneLifeAnim;
    public GameObject cardTemplate;
    private int currExp;
    private int currLevel;
    public float timeGun;
    private float timeGunx;
    private float timeGunShoot = 0;
    private float immortalTime;
    private bool immortal;
    private Gyroscope m_Gyro;
    private Rigidbody2D rb;

    void Start()
    {
        Instantiate(spawner, new Vector3(Random.Range(-20, 20), Random.Range(-20, 20)), Quaternion.identity);
        Instantiate(spawner, new Vector3(Random.Range(-20, 20), Random.Range(-20, 20)), Quaternion.identity);
        Instantiate(spawner, new Vector3(Random.Range(-20, 20), Random.Range(-20, 20)), Quaternion.identity);
        Instantiate(spawner, new Vector3(Random.Range(-20, 20), Random.Range(-20, 20)), Quaternion.identity);
        currExp = 0;
        timeGunx = timeGun;
        experienceUI.value = 0;
        currLevel = 1;
        health = 5;
        immortalTime = 3;
        immortal = false;
        isPause = false;
        rb = GetComponent<Rigidbody2D>();
        UpdateUI();
    }

    void Update()
    {
        timeGunx -= Time.deltaTime;
        if (immortal)
        {
            immortalTime -= Time.deltaTime;
        }
        if(immortalTime <= 0)
        {
            immortal = false;
            immortalTime = 3;
        }


        // Look Mechanism (Check for joystick connection)
        if(Input.GetJoystickNames().Length == 0 || (Input.GetJoystickNames().Length == 1 && Input.GetJoystickNames()[0] == ""))
        {
            // Mouse look
            var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            cardTemplate.transform.LookAt(dir * 0.1f, Vector3.up);
        } else
        {
            // Controller look
            float x = Input.GetAxis("Mouse X controller");
            float y = Input.GetAxis("Mouse Y controller");
            if (!(x == 0 && y == 0))
            {
                Vector3 pointerPos = new Vector3(transform.position.x + x, transform.position.y + y, 0);
                var dir = pointerPos - transform.position;
                var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                Vector3 pointerPosC = new Vector3(cardTemplate.transform.position.x + x, cardTemplate.transform.position.y + y, 0);
                var dirC = pointerPosC - cardTemplate.transform.position;
                var angleC = Mathf.Atan2(dirC.y, dirC.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
                cardTemplate.transform.rotation = Quaternion.AngleAxis((angleC - 90) * 0.2f, Vector3.up);
            } else
            {
                cardTemplate.transform.rotation.Set(0,0,0,0);
            }
            
        }


        // Movement
        transform.position = new Vector2(transform.position.x + speed * Input.GetAxis("Horizontal") * Time.deltaTime, transform.position.y + speed * Input.GetAxis("Vertical") * Time.deltaTime);

        // Fire Mechanism
        if (Input.GetButton("Fire1"))
        {
            if (timeGunx < 0 && timeGunShoot < 8 )
            {
                Instantiate(bullet, gunPoint.position, transform.rotation);
                timeGunx = timeGun;
            }
            if (timeGunShoot < 8)
            {
                timeGunShoot += Time.deltaTime;
                spr.sprite = skins[Mathf.CeilToInt(Mathf.Clamp(timeGunShoot, 0, 8))];
            }
        } else
        {
            // Change sprite skin based on bullet shot time
            if (Mathf.CeilToInt(timeGunShoot) != 0) { 
                timeGunShoot -= Time.deltaTime;
                spr.sprite = skins[Mathf.CeilToInt(timeGunShoot)];
            } 
        }

        // Secondaru Fire
        if (Input.GetMouseButtonDown(1))
        {
            Instantiate(grenade, gunPoint.position, transform.rotation);
        }

        // Level Up
        if(currExp >= currLevel * 10)
        {
            currLevel++;
            experienceUI.maxValue = currLevel * 10;
            experienceUI.value = 0;
            currExp = 0;
            UpdateUI();
            aud.volume = 0.3f;
            aud.PlayOneShot(levelUp_aud);
            Instantiate(particle_LevelUp, transform.position, Quaternion.identity);
        }

        // Health depleted
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Collectibles")
        {
            Destroy(collision.gameObject);
            aud.volume = 0.4f;
            aud.PlayOneShot(collectExp_aud);
            currExp++;
            UpdateUI();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    { 

        if (collision.gameObject.tag == "Destroyable" && !immortal)
        {
            aud.volume = 1;
            aud.PlayOneShot(heartBeat_aud);
            loseOneLifeAnim.SetTrigger("LoseLife");
            immortal = true;
            health--;
            UpdateUI();
        }
        
    }

    // Update UI
    void UpdateUI()
    {
        healthTxt.text = "x" + health;
        levelTxt.text = "Level " + currLevel;
        experienceUI.value = currExp;
    }
}
