using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;
    private Dictionary<GameObject, List<GameObject>> _pooledObjectsByPrefab;

    void Awake()
    {
        Instance = this;    
    }

    void Start()
    {
        _pooledObjectsByPrefab = new Dictionary<GameObject, List<GameObject>>();

    }

    public GameObject GetPooledObject(GameObject prefab)
    {
        List<GameObject> listOfUnusedObjects = null;

        if (_pooledObjectsByPrefab.TryGetValue(prefab, out listOfUnusedObjects))
        {
            foreach (GameObject obj in listOfUnusedObjects) 
            {
                if (!obj.activeInHierarchy)
                {
                    obj.SetActive(true);
                    return obj;
                }
            }

            // if we get to here, all previously created objects are in use. Create a new one and add it to the list.
            GameObject newObjectInstance = GameObject.Instantiate(prefab);
            listOfUnusedObjects.Add( newObjectInstance );
            return newObjectInstance;
        }
        else
        {
            GameObject returnNewObject = GameObject.Instantiate(prefab);
            listOfUnusedObjects = new List<GameObject>();
            listOfUnusedObjects.Add(returnNewObject);
            _pooledObjectsByPrefab.Add(prefab, listOfUnusedObjects);
            return returnNewObject;
        }
    }

    public void ReturnObjectToPool(GameObject obj) 
    {
        obj.SetActive(false);
    }
 
}
