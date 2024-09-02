using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private GameObject Enemy;
    [SerializeField] private GameObject Explosion1;
    [SerializeField] private AudioClip fireExplosion;
    [SerializeField] private float speed = 15f;
    [SerializeField] private AudioSource Death;
    [SerializeField] private AudioClip Dead;
    private Rigidbody2D RB;

    private bool isEnemyMoving;
    private float moveDirection;
    // Start is called before the first frame update
    void Start()
    {
        RB = gameObject.GetComponent<Rigidbody2D>();
        RB.velocity = speed * Vector2.left;
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerScript playerScript = GameObject.FindObjectOfType<PlayerScript>();

        if (collision.transform.tag == "Bullet")
        {
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(Dead ,transform.position);
            playerScript.UpdateScore();
            Instantiate(Explosion1, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(fireExplosion, transform.position);
        }
    }

}
