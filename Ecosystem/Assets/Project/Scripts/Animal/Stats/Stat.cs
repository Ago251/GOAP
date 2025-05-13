using System;
using UnityEngine;

namespace Ecosystem.Animals.Stats
{
    [Serializable]
    public class Stat
    {
        public StatType Type
        {
            get
            {
                return type;
            }

            private set
            {
                type = value;
            }
        }

        public float CurrentValue
        {
            get
            {
                return currentValue;
            }

            set
            {
                currentValue = Math.Clamp(value, 0, maxValue);
            }
        }

        [SerializeField]
        private StatType type;
        [SerializeField]
        private float currentValue = 50f;
        [SerializeField]
        private float maxValue = 100f;
        [SerializeField]
        private float decay = 1f;

        public Stat(StatType type, float currentValue, float maxValue = 100, float decay = 0)
        {
            this.type = type;
            this.currentValue = currentValue;
            this.maxValue = maxValue;
            this.decay = decay;
        }

        public void Execute()
        {
            CurrentValue -= decay * Time.deltaTime;
        }
    }
}
