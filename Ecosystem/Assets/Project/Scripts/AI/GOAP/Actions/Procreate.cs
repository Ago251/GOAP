using System;
using System.Threading;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Ecosystem.Enum;
using Ecosystem.Utility;
using Ecosystem.Animals;
using UnityEngine.Analytics;

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

        public override UniTask<bool> Perform(GameObject agent, CancellationToken cancellationToken)
        {
            if (!agent.TryGetComponent(out Vision vision)) return UniTask.FromResult(true);
            
            var myAnimal = agent.GetComponent<Animal>();
            var animalType = AnimalUtility.GetAnimalEnum(myAnimal);
            var animalOppositeGender = AnimalUtility.GetNearestOppositeGenderAnimal(myAnimal, vision.GetSeenAnimal(animalType));

            if (animalOppositeGender != null)
            {
                myAnimal.Procreate(myAnimal, animalOppositeGender);
                return UniTask.FromResult(true);
            }
            else
                return UniTask.FromResult(false);

        }
    }
}