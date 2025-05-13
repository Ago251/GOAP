using UnityEngine;
using Ecosystem.Items;

namespace Ecosystem.Spawners
{
    public class PlantSpawner : Spawner
    {
        [SerializeField] private Transform spawnPlane;
        [SerializeField] private float spawnInterval;

        private float timer;

        private void Start()
        {
            timer = spawnInterval;
        }


        private void Update()
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                Spawn();
                timer = spawnInterval;
            }
        }

        protected override void Spawn()
        {
            var obj = poolManager.GetObject();
            obj.transform.position = GetSpawnPosition();
            var plant = obj.GetComponent<Item>();
            plant.OnConsumed += ReturnObject;
        }

        protected override void ReturnObject(GameObject obj)
        {
            var plant = obj.GetComponent<Item>();
            plant.OnConsumed -= ReturnObject;
            poolManager.ReturnObject(obj);
        }

        private Vector3 GetSpawnPosition()
        {
            Vector3 planeSize = spawnPlane.transform.localScale * 10;
            float randomX = Random.Range(-planeSize.x / 2, planeSize.x / 2);
            float randomZ = Random.Range(-planeSize.z / 2, planeSize.z / 2);

            return new Vector3(randomX, spawnPlane.transform.position.y, randomZ);
        }
    }
}
