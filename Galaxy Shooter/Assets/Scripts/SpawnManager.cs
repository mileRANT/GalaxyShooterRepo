using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;  //in the hierarchy, want to spawn enemies within this gameobject container
    [SerializeField]
    private GameObject[] _powerUpsPrefab;
    private bool _stopSpawning = false;
    // Start is called before the first frame update

    void Start()
    {
        ////both the below is valid. have both pros and cons
        ////StartCoroutine("SpawnRoutine");
        //StartCoroutine(SpawnEnemyRoutine());
        //StartCoroutine("SpawnPowerUpRoutine");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //spawn game objects every 5 seconds
    //coroutine of type ienumerator as we need the yield call
    IEnumerator SpawnEnemyRoutine()
    {
        while (!_stopSpawning)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        }
        
    }

    IEnumerator SpawnPowerUpRoutine()
    {
        while (!_stopSpawning)
        {

            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            int randomPowerUp = Random.Range(0, _powerUpsPrefab.Length);
            GameObject newPowerUp = Instantiate(_powerUpsPrefab[randomPowerUp], posToSpawn, Quaternion.identity);
            //newPowerUp.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(Random.Range(5f, 8f));
        }
    }

    public void StartSpawnRoutines()
    {
        //both the below is valid. have both pros and cons
        //StartCoroutine("SpawnRoutine");
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine("SpawnPowerUpRoutine");
    }
    public void onPlayerDeath()
    {
        _stopSpawning = true;
    }
}
