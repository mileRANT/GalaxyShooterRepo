using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;  //in the hierarchy, want to spawn enemies within this gameobject container
    private bool _stopSpawning = false;
    // Start is called before the first frame update

    void Start()
    {
        //both the below is valid. have both pros and cons
        //StartCoroutine("SpawnRoutine");
        StartCoroutine(SpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //spawn game objects every 5 seconds
    //coroutine of type ienumerator as we need the yield call
    IEnumerator SpawnRoutine()
    {
        while (!_stopSpawning)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        }
        
    }

    public void onPlayerDeath()
    {
        _stopSpawning = true;
    }
}
