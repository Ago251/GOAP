using System;
using System.Collections.Generic;
using System.Linq;

namespace Ecosystem.AI.GOAP
{
    [Serializable]
    public class Goal
    {
        public readonly Dictionary<string, object> Conditions = new();

        public bool IsVerified(WorldState worldState)
        {
            return Conditions.All(x => worldState.GetValue<object>(x.Key).Equals(x.Value));
        }
    }
}