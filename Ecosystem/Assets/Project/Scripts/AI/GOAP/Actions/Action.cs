using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace Ecosystem.AI.GOAP
{
    [Serializable]
    public abstract class Action
    {
        public abstract Precondition[] Preconditions { get; }
        public abstract Effect[] Effects { get; }

        public abstract UniTask<bool> Perform(GameObject agent);
    }
}