using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : MonoBehaviour
{
    public static event Action onPause;
    public static event Action onResume;
    public event Action<int> onStop;
    public event Action<bool> onWow;
    [SerializeField] private ObstacleInfo[] obstaclePrefabs;
    [SerializeField] private float spawnRate;
    [SerializeField] private Vector3 spawnPoint;
    [SerializeField] private Collider newElementTrigger;
    [SerializeField] private float duration;
    [SerializeField] private float speedMultiplier = 1;
    private bool[] isNew;
    private Queue<Obstacle>[] pools;
    private IEnumerator spawnCoroutine;
    private IEnumerator timerCoroutine;
    private float elapsedTime = 0;
    private bool isPaused = false;

    private void Awake()
    {
        onPause = null;
        onResume = null;
        pools = new Queue<Obstacle>[obstaclePrefabs.Length];
        for(int i = 0; i < pools.Length; i++)
        {
            pools[i] = new Queue<Obstacle>();
        }

        isNew = new bool[obstaclePrefabs.Length];
        for(int i = 0; i < obstaclePrefabs.Length; i++)
        {
            isNew[i] = obstaclePrefabs[i].IsNew;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        spawnCoroutine = Spawner();
        StartCoroutine(spawnCoroutine);

        timerCoroutine = Timer();
        StartCoroutine(timerCoroutine);
    }


    IEnumerator Spawner()
    {
        if(spawnRate <= 0)
        {
            Debug.Log("Spawn rate can't be zero");
        }
        while(elapsedTime < duration)
        {
            
            
            yield return new WaitForSeconds(spawnRate);
            Spawn();
            
        }
    }

    IEnumerator Timer()
    {
        while (elapsedTime < duration)
        {
            yield return new WaitForEndOfFrame();
            elapsedTime += Time.deltaTime;
        }
        onStop?.Invoke(5);
        onPause?.Invoke();
    }

    private void Spawn()
    {
        var obstacleId = UnityEngine.Random.Range(0, obstaclePrefabs.Length);
        if(pools[obstacleId].Count > 0)
        {
            var item = pools[obstacleId].Dequeue();
            item.Init(spawnPoint);
            Debug.Log(item.gameObject.name + "Spawned from pool");
        }
        else
        {
            var item = Instantiate(obstaclePrefabs[obstacleId].Prefab, spawnPoint, Quaternion.identity).GetComponent<Obstacle>();
            item.Velocity *= speedMultiplier;
            if(isNew[obstacleId])
            {
                item.Init(OnNewElementTrigger, OnNewElementRemoved);
                isNew[obstacleId] = false;
            }
            item.onWow += (val) => { onWow?.Invoke(val); };
            item.Id = obstacleId;
            item.OnDestroy += OnObstacleDestroy;
            Debug.Log(item.gameObject.name + "Spawned via Instantiate()");
        }
    }

    #region OnNewElement
    private void OnNewElementTrigger()
    {
        StopCoroutine(spawnCoroutine);
        StopCoroutine(timerCoroutine);
        newElementTrigger.enabled = false;
        Debug.Log("Stop");
        onPause?.Invoke();
        isPaused = true;
    }

    private void OnNewElementRemoved()
    {
        if (!isPaused)
            return;
        StopCoroutine(spawnCoroutine);
        spawnCoroutine = Spawner();
        StartCoroutine(spawnCoroutine);

        StopCoroutine(timerCoroutine);
        timerCoroutine = Timer();
        StartCoroutine(timerCoroutine);
        newElementTrigger.enabled = true;
        Debug.Log("play");
        onResume?.Invoke();
        isPaused = false;
    }
    #endregion

    void OnObstacleDestroy(Obstacle obstacle)
    {
        pools[obstacle.Id].Enqueue(obstacle);
    }
}
