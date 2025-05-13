using System;

namespace Ecosystem.AI.GOAP
{
    [Serializable]
    public abstract class Effect
    {
        public abstract void Apply(ref WorldState worldState);
    }
}