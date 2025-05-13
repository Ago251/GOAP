using System.Collections.Generic;
using UnityEngine;

namespace Ecosystem.Spawners.PoolManagers
{
    public class PoolManager : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private int inititalSize = 10;

        private Queue<GameObject> pool = new Queue<GameObject>();

        private void Start()
        {
            for (int i = 0; i < inititalSize; i++)
            {
                var obj = Instantiate(prefab);
                obj.gameObject.SetActive(false);
                pool.Enqueue(obj);
            }
        }

        public GameObject GetObject()
        {
            if (pool.Count > 0)
            {
                GameObject obj = pool.Dequeue();
                obj.SetActive(true);
                return obj;
            }
            else
            {
                GameObject obj = Instantiate(prefab);
                return obj;
            }
        }

        public void ReturnObject(GameObject obj)
        {
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }
}
