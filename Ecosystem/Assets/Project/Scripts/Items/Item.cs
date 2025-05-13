using Ecosystem.Animals.Stats;
using System;
using UnityEngine;

namespace Ecosystem.Items
{
    public abstract class Item : MonoBehaviour
    {
        [SerializeField]
        protected Stat[] stats = new Stat[0];

        public event Action<GameObject> OnConsumed;

        protected void InvokeOnConsumed() => OnConsumed?.Invoke(gameObject);
    }
}
