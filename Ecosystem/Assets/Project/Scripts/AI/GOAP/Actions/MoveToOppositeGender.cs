using UnityEngine;
using UnityEngine.AI;
using Cysharp.Threading.Tasks;
using Ecosystem.Enum;
using Ecosystem.Utility;
using System;

namespace Ecosystem.AI.GOAP
{
    [Serializable]
    public class MoveToOppositeGender : Action
    {
        [SerializeField]
        private ENutriment targetType;
        [SerializeField]
        private float actionRange;

        private GameObject target;

        public override Precondition[] Preconditions => new Precondition[] { new HasObjectNearPrecondition("Animal_Opposite_Gender") };

        public override Effect[] Effects => new Effect[] { new ObjectInActionRange("Animal_Opposite_Gender")  };

        public override async UniTask<bool> Perform(GameObject agent)
        {
            if (agent.TryGetComponent(out Vision vision))
            {
                target = FindUtility.GetNearestTargets(agent, vision.GetSeenNutriment(targetType));
                if (agent.TryGetComponent(out NavMeshAgent navmeshAgent))
                {
                    navmeshAgent.isStopped = false;
                    while (navmeshAgent.remainingDistance > actionRange)
                    {
                        navmeshAgent.SetDestination(target.transform.position);
                        await UniTask.NextFrame();
                    }
                    navmeshAgent.isStopped = true;
                }
            }

            return true;
        }
    }
}