using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance { get; private set; }

    public Dictionary<string, Queue<GameObject>> poolDictionary;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        poolDictionary = new Dictionary<string, Queue<GameObject>>();
    }

    public void CreatePool(GameObject prefab, int size)
    {
        string key = prefab.name;
        if (!poolDictionary.ContainsKey(key))
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < size; i++)
            {
                GameObject obj = Instantiate(prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(key, objectPool);
        }
    }

    public GameObject SpawnFromPool(string key, Vector3 position)
    {
        if (poolDictionary.TryGetValue(key, out Queue<GameObject> queue))
        {
            if (queue.Count > 0)
            {
                GameObject obj = queue.Dequeue();
                obj.transform.position = position;
                obj.SetActive(true);
                return obj;
            }
        }
        return null;
    }
}