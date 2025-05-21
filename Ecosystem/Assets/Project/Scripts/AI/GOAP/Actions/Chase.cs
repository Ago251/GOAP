using UnityEngine;
using UnityEngine.AI;
using Cysharp.Threading.Tasks;
using Ecosystem.Enum;
using Ecosystem.Utility;
using System;
using System.Threading;

namespace Ecosystem.AI.GOAP
{
    [Serializable]
    public class Chase : Action
    {
        [SerializeField]
        private ENutriment targetType;
        [SerializeField]
        private float actionRange;

        private GameObject target;

        public override Precondition[] Preconditions => new Precondition[] { new HasStatus(EStatus.Safe) };

        public override Effect[] Effects => new Effect[] { new ObjectInActionRange(targetType.ToString()) };

        public override async UniTask<bool> Perform(GameObject agent, CancellationToken cancellationToken)
        {
            if (!agent.TryGetComponent(out Vision vision)) return true;
            
            target = FindUtility.GetNearestTargets(agent, vision.GetSeenNutriment(targetType));

            if (!agent.TryGetComponent(out NavMeshAgent navmeshAgent)) return true;
            
            while (navmeshAgent.remainingDistance > actionRange)
            {
                cancellationToken.ThrowIfCancellationRequested();
                navmeshAgent.SetDestination(target.transform.position);
                await UniTask.NextFrame(cancellationToken);
            }

            return true;
        }
    }
}