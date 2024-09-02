using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchBullet : MonoBehaviour
{
    private PlayerScript PS;
    private Rigidbody2D rb2d;
    [SerializeField] private GameObject Bullet;
    [SerializeField] private GameObject Barrel;
    private int BulletDirection;
    private GameObject Player;
    [SerializeField] private int BulletSpeed;

    // Start is called before the first frame update
    void Start()
    {
        PS = GetComponent<PlayerScript>();
        rb2d = GetComponent<Rigidbody2D>();

        Player = GameObject.Find("Player");
    }

    public void FireBullet(bool left ,bool right)
    {


        if (left == true)
        {
            BulletDirection = -1;
        }
        if (right == true)
        {
            BulletDirection = 1;
        }
        GameObject bFire = Instantiate(Bullet, Barrel.transform.position, Quaternion.identity);
        rb2d.velocity = new Vector2(BulletDirection * BulletSpeed, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            Destroy(gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
