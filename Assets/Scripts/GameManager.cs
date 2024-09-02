using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> Lanes;
    [SerializeField] private List<GameObject> Enemies;
    private int randomlane;
    private int randomenemy;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemy());
    }

    private IEnumerator spawnEnemy()
    {
        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(4);
        }
    }

    private void SpawnEnemy()
    {
        randomlane = Random.Range(0, Lanes.Count) ;
        randomenemy = Random.Range(0, Enemies.Count);
        GameObject enemy = Instantiate(Enemies[randomenemy], (Lanes[randomlane]).transform);
        enemy.transform.position = Lanes[randomlane].transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
