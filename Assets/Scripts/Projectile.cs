using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 15f;

    [SerializeField]private float timeToLive = 5f;

    private float timeSinceSpawned = 0f;

    [SerializeField] private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += moveSpeed * transform.right * Time.deltaTime;

        timeSinceSpawned += Time.deltaTime;
        if (timeSinceSpawned > timeToLive)
        {
            Destroy(gameObject);
        }
    }


}
