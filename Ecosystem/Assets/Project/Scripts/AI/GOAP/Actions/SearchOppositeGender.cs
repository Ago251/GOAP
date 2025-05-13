using System;
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

        public async override UniTask<bool> Perform(GameObject agent)
        {
            if (agent.TryGetComponent(out Vision vision))
            {
                var animal = agent.GetComponent<Animal>();
                if (agent.TryGetComponent(out NavMeshAgent navMeshAgent))
                {
                    navMeshAgent.isStopped = false;
                    while (AnimalUtility.GetNearestOppositeGenderAnimal(animal, vision.GetSeenAnimal(animalType)) == null)
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
            var randomRotation = Random.Range(0, 2 * Mathf.PI);
            var direction = Quaternion.Euler(0, randomRotation, 0) * agent.transform.forward;
            var targetPosition = agent.transform.position + direction * Random.Range(1, maxRadius);

            if (NavMesh.SamplePosition(targetPosition, out var hit,  maxRadius, NavMesh.AllAreas))
            {
                return hit.position;
            }

            return agent.transform.position;
        }
    }
}