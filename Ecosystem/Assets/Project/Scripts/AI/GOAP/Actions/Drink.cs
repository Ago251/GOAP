using System;
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

        public override UniTask<bool> Perform(GameObject agent)
        {
            if (agent.TryGetComponent(out Vision vision))
            {
                var nearestTarget = FindUtility.GetNearestTargets(agent, vision.GetSeenLiquid(liquid));
                if (nearestTarget != null)
                {
                    var animal = agent.GetComponent<Animal>();
                    var drinkable = nearestTarget.GetComponent<IDrinkable>();
                    if (drinkable != null)
                    {
                        animal.Drink(drinkable);
                    }
                    else
                    {
                        return UniTask.FromResult(false);
                    }
                }
            }

            return UniTask.FromResult(true);
        }
    }
}