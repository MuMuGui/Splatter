using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    float timer = 0f;
    [SerializeField] float spawnInterval;
    [SerializeField] int enemyLimit;
    GameObject[] enemies;
    //Array
    float[] PlatformPositionsx = new float[4];
    float[] PlatformPositionsy= new float[4];
    int index;
    bool dontSpawn = false;
    public GameObject[] colors;

    // Start is called before the first frame update
    void Start()
    {
        PlatformPositionsx[0] = Random.Range(4, 9);
        PlatformPositionsx[1] = Random.Range(15, 20);
        PlatformPositionsy[0] = 3f;
        PlatformPositionsy[1] = 0f;
        
    }

    // Update is called once per frame
    //wait

    
    void Update()
    {
        
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        index = Random.Range(0, 1);

        if (timer > spawnInterval)
        {
            
            if (enemies.Length < enemyLimit && dontSpawn == false)
            {

                Debug.Log(index);
                GameObject enemyClone = Instantiate(colors[Random.Range(0,9)]);
                enemyClone.transform.position = new Vector3(gameObject.transform.position.x + Random.Range(-3,3), gameObject.transform.position.y, 0);
                //new Vector3(Random.Range(-1, 1), Random.Range(-3f, 1.4f), 0)
                timer = 0.0f;
            }
            
        }
        else
        {
            timer += Time.deltaTime;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.tag == "Enemy")
        {
            dontSpawn = true;
            Debug.Log("spawn");
        }
        else if (collision.tag == "good")
        {
            dontSpawn = false;
            Debug.Log("!");
        }

            
    }
}
