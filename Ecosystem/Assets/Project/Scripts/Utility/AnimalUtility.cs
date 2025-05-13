using Ecosystem.Animals;
using Ecosystem.Enum;
using System.Collections.Generic;
using UnityEngine;

namespace Ecosystem.Utility
{
    public static class AnimalUtility
    {
        public static Animal GetNearestOppositeGenderAnimal(Animal myAnimal, List<Animal> animals)
        {
            Animal nearestTarget = null;
            float nearestDistance = float.MaxValue;

            foreach (Animal animal in animals)
            {
                if (animal != null && animal.Gender != myAnimal.Gender)
                {
                    float distance = Vector3.Distance(myAnimal.transform.position, animal.transform.position);
                    if (distance < nearestDistance)
                    {
                        nearestDistance = distance;
                        nearestTarget = animal;
                    }
                }
            }

            return nearestTarget;
        }

        public static EAnimal GetAnimalEnum(Animal animal) 
        {
            if(animal is Rabbit)
            {
                return EAnimal.Rabbit;
            }

            if(animal is Fox)
            {
                return EAnimal.Fox;
            }

            return EAnimal.None;
        }
    }
}
