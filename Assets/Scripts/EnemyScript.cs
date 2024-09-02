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
    [SerializeField] private int Health;
    private Rigidbody2D RB;

    private bool isEnemyMoving;
    private float moveDirection;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        RB = gameObject.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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
            Health--;
            StartCoroutine(StunEnemy());
            animator.SetTrigger("Hit");
            AudioSource.PlayClipAtPoint(Dead ,transform.position);
            playerScript.UpdateScore();
            Instantiate(Explosion1, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(fireExplosion, transform.position);
            if(Health == 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private IEnumerator StunEnemy()
    {
        RB.velocity = Vector2.zero;
        yield return new WaitForSeconds(1);
        RB.velocity = Vector2.left * speed;
    }
}
