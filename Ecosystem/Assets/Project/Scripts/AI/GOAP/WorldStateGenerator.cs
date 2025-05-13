using UnityEngine;

namespace Ecosystem.AI.GOAP
{
    public abstract class WorldStateGenerator : MonoBehaviour
    {
        public abstract WorldState Construct();

        public abstract bool IsChangedCriticalCondition();
    }
}