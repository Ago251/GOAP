using System;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
using Cysharp.Threading.Tasks;
using Random = UnityEngine.Random;
using Ecosystem.Enum;
using Ecosystem.Utility;
using Ecosystem.Animals;

namespace Ecosystem.AI.GOAP
{
    [Serializable]
    public class SearchOppositeGender : Action
    {
        [SerializeField] private EAnimal animalType;
        [SerializeField] private float arriveDistance;
        [SerializeField] private float maxRadius;
        [SerializeField] private float minHunger = 70;
        [SerializeField] private float minThirsty = 70;

        public override Precondition[] Preconditions => new Precondition[] { new HasHungerHigh(minHunger),
                                                                             new HasThirstyHigh(minThirsty),
                                                                             new HasStatus(EStatus.Safe)  };

        public override Effect[] Effects => new Effect[] { new HasObjectNearEffect("Animal_Opposite_Gender") };

        public override async UniTask<bool> Perform(GameObject agent, CancellationToken cancellationToken)
        {
            if (!agent.TryGetComponent(out Vision vision)) return true;
            
            var animal = agent.GetComponent<Animal>();
            
            if (!agent.TryGetComponent(out NavMeshAgent navMeshAgent)) return true;
            
            while (AnimalUtility.GetNearestOppositeGenderAnimal(animal, vision.GetSeenAnimal(animalType)) == null && agent.activeInHierarchy)
            {
                cancellationToken.ThrowIfCancellationRequested();
                if (navMeshAgent.remainingDistance <= arriveDistance)
                {
                    var randomPosition = GetRandomPosition(agent);
                    navMeshAgent.SetDestination(randomPosition);
                }
                        
                await UniTask.NextFrame(cancellationToken);
            }

            return true;
        }

        private Vector3 GetRandomPosition(GameObject agent)
        {
            Vector2 randomPoint = Random.insideUnitCircle * maxRadius;
            Vector3 randomPosition = new Vector3(randomPoint.x, 0, randomPoint.y) + agent.transform.position;

            if (NavMesh.SamplePosition(randomPosition, out var hit,  maxRadius, NavMesh.AllAreas))
            {
                return hit.position;
            }

            return agent.transform.position;
        }
    }
}