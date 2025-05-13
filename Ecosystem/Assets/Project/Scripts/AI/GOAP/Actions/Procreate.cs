using System;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Ecosystem.Enum;
using Ecosystem.Utility;
using Ecosystem.Animals;

namespace Ecosystem.AI.GOAP
{
    [Serializable]
    public class Procreate : Action
    {
        [SerializeField]
        private ENutriment nutriment;
        [SerializeField]
        private float minHunger = 70;
        [SerializeField]
        private float minThirsty = 70;

        public override Precondition[] Preconditions => new Precondition[] { new HasObjectInActionRange("Animal_Opposite_Gender"),
                                                                             new HasHungerHigh(minHunger),
                                                                             new HasThirstyHigh(minThirsty),
                                                                             new HasStatus(EStatus.Safe)  };

        public override Effect[] Effects => new Effect[] { new ProcreateEffect() };

        public override UniTask<bool> Perform(GameObject agent)
        {
            if (agent.TryGetComponent(out Vision vision))
            {
                var myAnimal = agent.GetComponent<Animal>();
                var animalType = AnimalUtility.GetAnimalEnum(myAnimal);
                var animalOppositeGender = AnimalUtility.GetNearestOppositeGenderAnimal(myAnimal, vision.GetSeenAnimal(animalType));

                myAnimal.Procreate(myAnimal, animalOppositeGender);
            }

            return UniTask.FromResult(true);
        }
    }
}