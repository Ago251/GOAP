using System;
using UnityEngine;
using UnityEngine.AI;
using Cysharp.Threading.Tasks;
using Random = UnityEngine.Random;
using Ecosystem.Enum;

namespace Ecosystem.AI.GOAP
{
    [Serializable]
    public class Search : Action
    {
        [SerializeField] ENutriment nutriment;
        [SerializeField] private float arriveDistance;
        [SerializeField] private float maxRadius;

        public override Precondition[] Preconditions => new Precondition[] { new HasStatus(Enum.EStatus.Safe), new HasObjective(true) };

        public override Effect[] Effects => new Effect[] { new HasObjectNearEffect(nutriment.ToString()) , new ObjectiveEffect(true)   };

        public async override UniTask<bool> Perform(GameObject agent)
        {
            if (agent.TryGetComponent(out Vision vision))
            {
                if (agent.TryGetComponent(out NavMeshAgent navMeshAgent))
                {
                    navMeshAgent.isStopped = false;
                    while (vision.GetSeenNutriment(nutriment).Count == 0)
                    {
                        if (navMeshAgent.remainingDistance <= arriveDistance)
                        {
                            var randomPosition = GetRandomPosition(agent);
                            navMeshAgent.SetDestination(randomPosition);
                        }
                        await UniTask.NextFrame();
                    }
                    navMeshAgent.isStopped = true;
                }
            }

            return true;
        }

        private Vector3 GetRandomPosition(GameObject agent)
        {
            Vector2 randomPoint = Random.insideUnitCircle * maxRadius;
            Vector3 randomPosition = new Vector3(randomPoint.x, 0, randomPoint.y) + agent.transform.position;

            if (NavMesh.SamplePosition(randomPosition, out var hit, maxRadius, NavMesh.AllAreas))
            {
                return hit.position;
            }

            return agent.transform.position;
        }
    }
}