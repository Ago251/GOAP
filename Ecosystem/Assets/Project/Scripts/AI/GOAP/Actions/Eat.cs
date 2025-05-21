using UnityEngine;
using Cysharp.Threading.Tasks;
using Ecosystem.Enum;
using Ecosystem.Interfaces;
using Ecosystem.Utility;
using System;
using System.Threading;
using Ecosystem.Animals;

namespace Ecosystem.AI.GOAP
{
    [Serializable]
    public class Eat : Action
    {
        [SerializeField]
        private ENutriment nutriment;

        public override Precondition[] Preconditions => new Precondition[] { new HasObjectInActionRange(nutriment.ToString()),
                                                                             new HasStatus(EStatus.Safe)};

        public override Effect[] Effects => new Effect[] { new EatEffect(nutriment), new ObjectiveEffect(false) };

        public override UniTask<bool> Perform(GameObject agent, CancellationToken cancellationToken)
        {
            if (!agent.TryGetComponent(out Vision vision)) return UniTask.FromResult(true);
            
            var nearestTarget = FindUtility.GetNearestTargets(agent, vision.GetSeenNutriment(nutriment));

            if (nearestTarget == null) return UniTask.FromResult(true);
            
            var animal = agent.GetComponent<Animal>();
            var eatable = nearestTarget.GetComponent<IEateable>();
            if (eatable == null) return UniTask.FromResult(false);
            animal.Eat(eatable);
            return UniTask.FromResult(true);
        }
    }
}