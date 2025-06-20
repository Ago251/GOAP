﻿using System;
using System.Threading;
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

        public override Precondition[] Preconditions => new Precondition[] { new HasStatus(Enum.EStatus.Safe), new HasObjective(true), new HasNeedNutriment(nutriment) };

        public override Effect[] Effects => new Effect[] { new HasObjectNearEffect(nutriment.ToString()) , new ObjectiveEffect(true)   };

        public override async UniTask<bool> Perform(GameObject agent, CancellationToken cancellationToken)
        {
            if (!agent.TryGetComponent(out Vision vision) || !agent.TryGetComponent(out NavMeshAgent navMeshAgent)) return true;
            
            while (vision.GetSeenNutriment(nutriment).Count == 0)
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

            return NavMesh.SamplePosition(randomPosition, out var hit, maxRadius, NavMesh.AllAreas) ? hit.position : agent.transform.position;
        }
    }
}