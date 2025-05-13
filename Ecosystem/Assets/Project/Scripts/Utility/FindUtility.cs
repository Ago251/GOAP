using Ecosystem.Animals;
using System.Collections.Generic;
using UnityEngine;

namespace Ecosystem.Utility
{
    public static class FindUtility
    {
        public static GameObject GetNearestTargets(GameObject agent, List<GameObject> targets)
        {
            GameObject nearestTarget = null;
            float nearestDistance = float.MaxValue;

            foreach (GameObject target in targets)
            {
                if (target != null)
                {
                    float distance = Vector3.Distance(agent.transform.position, target.transform.position);
                    if (distance < nearestDistance)
                    {
                        nearestDistance = distance;
                        nearestTarget = target;
                    }
                }
            }

            return nearestTarget;
        }
    }
}
