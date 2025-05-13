using System;

namespace Ecosystem.AI.GOAP
{
    [Serializable]
    public abstract class Precondition
    {
        public abstract bool Evaluate(WorldState worldState);
    }
}