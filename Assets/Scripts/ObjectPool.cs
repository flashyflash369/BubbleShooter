using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.UIElements;
public class ObjectPool : MonoBehaviour
{
   public List<PoolList> AllPool;
   public Dictionary<string,Queue<GameObject>> SearchPool = new Dictionary<string, Queue<GameObject>>();
    public Dictionary<string,GameObject> AllSpawnObjects = new Dictionary<string, GameObject>();
   public static ObjectPool instance;
   public int ExpandPoolLenght;
   void Awake(){if(instance==null){instance= this;}}
   void Start()=>SpawnObjects(AllPool);
//PoolOperations
   void SpawnObjects(List<PoolList> allPool)
    {
        allPool.ForEach(pool => {  
        // Attempting to add a duplicate key will throw an exception
        if(SearchPool.ContainsKey(pool.spawnObject.name)) {Debug.Log("Object Already exist in SearchPool"); return;}
        SearchPool.Add(pool.spawnObject.name,pool.poolObjects);
        AllSpawnObjects.Add(pool.spawnObject.name,pool.spawnObject);
        for(int i = 0; i < pool.PoolSize;i++)
        {
            GameObject gameObject = Instantiate(pool.spawnObject);
            gameObject.SetActive(false);
            pool.poolObjects.Enqueue(gameObject);
        }});
    }
    public Queue<GameObject> FindPoolByObjectName(string tag){
    if(SearchPool.ContainsKey(tag)){ExpandPool( SearchPool[tag],AllSpawnObjects[tag]);};
    return !SearchPool.ContainsKey(tag)? GameObjectExtensions.LogAndReturnNull<Queue<GameObject>>("Object not in List") :SearchPool[tag].Also(obj => {});}

    public GameObject GetObject(string ObjectName){if(!AllSpawnObjects.ContainsKey(ObjectName)){;Debug.Log("IncorrectPool Name");return null;}
    Queue<GameObject> poolObjects= new Queue<GameObject>();
    poolObjects = FindPoolByObjectName(ObjectName);
    return poolObjects.Count==0 ? GameObjectExtensions.LogAndReturnNull<GameObject>("Pool is full")  :  poolObjects.Dequeue().Also(obj => obj.SetActive(true));}

    public GameObject GetObject(string ObjectName, Transform transform){if(!AllSpawnObjects.ContainsKey(ObjectName)){;Debug.Log("IncorrectPool Name");return null;}
    Queue<GameObject> poolObjects= new Queue<GameObject>();
    poolObjects = FindPoolByObjectName(ObjectName);
    return poolObjects.Count==0 ? GameObjectExtensions.LogAndReturnNull<GameObject>("Pool is full")  :  poolObjects.Dequeue().
    Also(obj => obj.transform.position =transform.position).Also(obj => obj.transform.rotation =transform.rotation).Also(obj => obj.SetActive(true));}


    public void ReturnObject(string ObjectName,GameObject gameObject){
    if(gameObject == null|| ObjectName==""){return;}
    string gameObjectName =gameObject.name.Replace("(Clone)","").Trim();
    Queue<GameObject> poolObjects= new Queue<GameObject>();
    if(gameObjectName == ObjectName){  
     poolObjects = FindPoolByObjectName(ObjectName);
     poolObjects.Enqueue(gameObject); gameObject.SetActive(false);}
    else{Debug.Log("GameObject Doesnt Match Pool Object");}
   }
    public void ExpandPool(  Queue<GameObject> poolObjects,GameObject spawnObject)
    { 
        if(poolObjects.Count>0){return;}
        for(int i = 0;i<ExpandPoolLenght;i++)
        {
            GameObject gameObject = Instantiate(spawnObject);
            gameObject.SetActive(false);
            poolObjects.Enqueue(gameObject);
        }
    }
}
[System.Serializable]
public class PoolList
{
    public GameObject spawnObject;
    public int PoolSize;
    public Queue<GameObject> poolObjects = new Queue<GameObject>();
}
public static class GameObjectExtensions
{
    public static T Also<T>(this T obj, System.Action<T> action){action(obj); return obj;}    
    public static void And(this GameObject obj, System.Action<GameObject> action){action(obj);}
    public static T LogAndReturnNull<T>(string message) where T : class {Debug.Log(message);return null;}
    
}
