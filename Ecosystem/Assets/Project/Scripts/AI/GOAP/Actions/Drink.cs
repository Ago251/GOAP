using System;
using System.Threading;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Ecosystem.Enum;
using Ecosystem.Interfaces;
using Ecosystem.Utility;
using Ecosystem.Animals;

namespace Ecosystem.AI.GOAP
{
    [Serializable]
    public class Drink : Action
    {
        [SerializeField]
        private ELiquid liquid;

        public override Precondition[] Preconditions => new Precondition[] { new HasObjectInActionRange(liquid.ToString()), 
                                                                             new HasStatus(EStatus.Safe) };

        public override Effect[] Effects => new Effect[] { new DrinkEffect(), new ObjectiveEffect(false) };

        public override UniTask<bool> Perform(GameObject agent, CancellationToken cancellationToken)
        {
            if (!agent.TryGetComponent(out Vision vision)) return UniTask.FromResult(true);
            
            var nearestTarget = FindUtility.GetNearestTargets(agent, vision.GetSeenLiquid(liquid));

            if (nearestTarget == null) return UniTask.FromResult(true);
            
            var animal = agent.GetComponent<Animal>();
            var drinkable = nearestTarget.GetComponent<IDrinkable>();
            if (drinkable == null) return UniTask.FromResult(false);
            animal.Drink(drinkable);
            return UniTask.FromResult(true);

        }
    }
}