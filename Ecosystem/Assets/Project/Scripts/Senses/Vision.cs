using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Ecosystem.Animals;
using Ecosystem.Enum;
using Ecosystem.Items;

namespace Ecosystem
{
    public class Vision : MonoBehaviour
    {
        [SerializeField]
        private float viewRadius = 10f;
        [SerializeField]
        private float viewAngle = 90f;
        [SerializeField]
        private LayerMask targetMask;
        [SerializeField]
        private LayerMask obstacleMask;

        private List<GameObject> seenObjects = new List<GameObject>();

        private void Update()
        {
            FindVisibleObjects();
        }

        private void FindVisibleObjects()
        {
            seenObjects.Clear();

            Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

            foreach (Collider target in targetsInViewRadius)
            {
                Transform targetTransform = target.transform;
                Vector3 dirToTarget = (targetTransform.position - transform.position).normalized;

                if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
                {
                    float distanceToTarget = Vector3.Distance(transform.position, targetTransform.position);

                    if (!Physics.Raycast(transform.position, dirToTarget, distanceToTarget, obstacleMask))
                    {
                        if (target.gameObject != gameObject)
                        {
                            seenObjects.Add(target.gameObject);
                        }
                    }
                }
            }
        }

        public List<T> GetVisibleObjects<T>()
        {
            List<T> visibleObjects = new List<T>();

            foreach (GameObject obj in seenObjects)
            {
                T component = obj.GetComponent<T>();
                if (component != null)
                {
                    visibleObjects.Add(component);
                }
            }

            return visibleObjects;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, viewRadius);

            Vector3 viewAngleA = DirFromAngle(-viewAngle / 2);
            Vector3 viewAngleB = DirFromAngle(viewAngle / 2);

            Gizmos.DrawLine(transform.position, transform.position + viewAngleA * viewRadius);
            Gizmos.DrawLine(transform.position, transform.position + viewAngleB * viewRadius);
        }

        private Vector3 DirFromAngle(float angleInDegrees)
        {
            angleInDegrees += transform.eulerAngles.y;
            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }

        public List<GameObject> GetSeenNutriment(ENutriment nutriment)
        {
            List<GameObject> gameObjects = new List<GameObject>();
            switch (nutriment)
            {
                case ENutriment.Plant:
                    gameObjects = GetVisibleObjects<NutrimentItem>().Select(item => item.gameObject).ToList();
                    break;
                case ENutriment.Rabbit:
                    gameObjects = GetVisibleObjects<Rabbit>().Select(item => item.gameObject).ToList();
                    break;
                case ENutriment.Fox:
                    gameObjects = GetVisibleObjects<Fox>().Select(item => item.gameObject).ToList();
                    break;
                case ENutriment.Water:
                    gameObjects = GetVisibleObjects<LiquidItem>().Select(item => item.gameObject).ToList();
                    break;
                default:
                    return gameObjects;
            }

            return gameObjects;
        }

        public List<GameObject> GetSeenLiquid(ELiquid liquid)
        {
            List<GameObject> gameObjects = new List<GameObject>();
            switch (liquid)
            {
                case ELiquid.Water:
                    gameObjects = GetVisibleObjects<Rabbit>().Select(item => item.gameObject).ToList();
                    break;
                case ELiquid.None:
                    return gameObjects;
                default:
                    return gameObjects;
            }

            return gameObjects;
        }

        public List<Animal> GetSeenAnimal(EAnimal animal)
        {
            List<Animal> animals = new List<Animal>();

            switch (animal)
            {
                case EAnimal.Fox:
                    List<Fox> foxes = GetVisibleObjects<Fox>().ToList();
                    animals = new List<Animal>(foxes.Cast<Animal>());
                    return animals;
                case EAnimal.Rabbit:
                    List<Rabbit> rabbits = GetVisibleObjects<Rabbit>().ToList();
                    animals = new List<Animal>(rabbits.Cast<Animal>());
                    return animals;
                default:
                    return animals;
            }
        }
    }
}
