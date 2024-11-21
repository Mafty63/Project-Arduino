using System.Collections.Generic;
using ProjectArduino.Utilities;
using UnityEngine;

namespace ProjectArduino.Gameplay
{
    public class ObjectPool : Singleton<ObjectPool>
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private int poolSize = 10;

        private Queue<GameObject> pool = new Queue<GameObject>();

        protected override void Awake()
        {
            base.Awake();
            for (int i = 0; i < poolSize; i++)
            {
                GameObject obj = Instantiate(prefab);
                obj.SetActive(false);
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
                return null;
            }
        }

        public void ReturnObject(GameObject obj)
        {
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }
}