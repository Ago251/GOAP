using UnityEngine;

namespace Ecosystem.Animals.Stats
{
    public class StatTracker : MonoBehaviour
    {
        [SerializeField]
        private Stat[] stats;

        public Stat this[StatType type]
        {
            get
            {
                return stats[(int)type];
            }

            set
            {
                stats[(int)type] = value;
            }
        }

        private void Update()
        {
            foreach (var stat in stats)
            {
                stat.Execute();
            }
        }
    }
}
