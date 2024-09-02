using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesScript : MonoBehaviour
{
    private PlayerScript playerScript;
    // Start is called before the first frame update
    void Start()
    {
         playerScript = GameObject.FindObjectOfType<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        switch (collision.transform.tag)
        {
            case "Barrier":
                playerScript.LoseALife();
                Destroy(gameObject);
                return;
            case "Player":
                playerScript.LoseALife();
                Destroy(gameObject);
                return;
            default:
                return;
        }
    }
}
