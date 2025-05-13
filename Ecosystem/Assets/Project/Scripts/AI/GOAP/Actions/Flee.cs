using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Cysharp.Threading.Tasks;
using Ecosystem.Enum;
using System;

namespace Ecosystem.AI.GOAP
{
    [Serializable]
    public class Flee : Action
    {
        [SerializeField]
        private ENutriment enemyType;
        [SerializeField]
        private float fleeRange;

        private List<GameObject> enemies;

        public override Precondition[] Preconditions => new Precondition[] { new HasObjectNearPrecondition(enemyType.ToString()) };

        public override Effect[] Effects => new Effect[] { new HasObjectNearEffect(enemyType.ToString(), true), new ChangedStatusEffect(EStatus.Safe) };

        public override async UniTask<bool> Perform(GameObject agent)
        {
            if (agent.TryGetComponent(out Vision vision))
            {
                enemies = vision.GetSeenNutriment(enemyType);
                if (agent.TryGetComponent(out NavMeshAgent navmeshAgent))
                {
                    navmeshAgent.isStopped = false;
                    while (GetDistanceFromEnemies(agent) < fleeRange)
                    {
                        navmeshAgent.SetDestination(agent.transform.position + GetFleeDirection(agent) * 5);
                        await UniTask.NextFrame();
                    }
                    navmeshAgent.isStopped = true;
                }
            }

            return true;
        }

        private Vector3 GetFleeDirection(GameObject agent)
        {
            Vector3 fleeDirection = Vector3.zero;

            foreach (GameObject enemy in enemies)
            {
                if (enemy != null)
                {
                    Vector3 directionToEnemy = (agent.transform.position - enemy.transform.position).normalized;
                    fleeDirection += directionToEnemy;
                }
            }

            if (enemies.Count > 0)
            {
                fleeDirection /= enemies.Count;
                fleeDirection = fleeDirection.normalized;
            }

            return fleeDirection;
        }


        private float GetDistanceFromEnemies(GameObject agent)
        {
            return Vector3.Distance(agent.transform.position, GetEnemiesCenter());
        }

        private Vector3 GetEnemiesCenter()
        {
            Vector3 center = Vector3.zero;

            foreach (GameObject enemy in enemies)
            {
                if (enemy != null)
                {
                    center += enemy.transform.position;
                }
            }

            if (enemies.Count > 0)
            {
                center /= enemies.Count;
            }

            return center;
        }
    }
}