using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using System;


public class ObjectPooling<T> where T:Component
{
    public List<T> poolActive = new List<T>();
    public Queue<T> poolDeactive = new Queue<T>();

   

    public void UpdateFixed()
    {
        int i = 0;
        while (i<poolActive.Count)
        {
            if (!poolActive[i].gameObject.activeInHierarchy)
            {
                poolDeactive.Enqueue(poolActive[i]);
                poolActive.RemoveAt(i);
            }
            else i++;
        }
    }

    public void GrowPool(T prefab, int number)
    {
        for (int i = 0; i < number; i++)
            AddToPool(prefab);
    }

    public void AddToPool(T prefab)
    {
        T obj = GameObject.Instantiate(prefab) as T;
        obj.gameObject.SetActive(false);
        poolDeactive.Enqueue(obj);
    }

    public T GetFromPool(T prefab)
    {
        if (poolDeactive.Count == 0)
            GrowPool(prefab, 5);
        T obj = poolDeactive.Dequeue();
        obj.gameObject.SetActive(true);
        poolActive.Add(obj);
        return obj;
    }

}
