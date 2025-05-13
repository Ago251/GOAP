using UnityEngine;
using Ecosystem.Spawners.PoolManagers;

namespace Ecosystem.Spawners
{
    public abstract class Spawner : MonoBehaviour
    {
        [SerializeField]
        protected PoolManager poolManager;

        protected abstract void Spawn();

        protected abstract void ReturnObject(GameObject obj);
    }
}
