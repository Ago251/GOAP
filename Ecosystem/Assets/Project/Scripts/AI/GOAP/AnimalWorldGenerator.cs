using UnityEngine;
using Ecosystem.Animals.Stats;
using Ecosystem.AI.GOAP.Utility;
using Ecosystem.Animals;
using Ecosystem.Utility;
using Ecosystem.Enum;

namespace Ecosystem.AI.GOAP
{
    public class AnimalWorldGenerator : WorldStateGenerator
    {
        [SerializeField]
        private Animal animal;
        [SerializeField]
        private Vision vision;
        [SerializeField]
        private StatTracker statTracker;


        private WorldState world;

        public override WorldState Construct()
        {
            var animalType = AnimalUtility.GetAnimalEnum(animal);
            world = new WorldState();
            world.SetValue(DictionaryKeys.NearObject(animal.Nutriment.ToString()), vision.GetSeenNutriment(animal.Nutriment).Count > 0);
            world.SetValue(DictionaryKeys.NearObject(animal.Liquid.ToString()), vision.GetSeenLiquid(animal.Liquid).Count > 0);
            world.SetValue(DictionaryKeys.NearObject("Animal_Opposite_Gender"), 
                           AnimalUtility.GetNearestOppositeGenderAnimal(animal, vision.GetSeenAnimal(animalType)) != null);
            world.SetValue(DictionaryKeys.Hunger(), statTracker[StatType.Hunger].CurrentValue);
            world.SetValue(DictionaryKeys.Thirsty(), statTracker[StatType.Thirsty].CurrentValue);
            world.SetValue(DictionaryKeys.Status(), vision.GetSeenAnimal(animal.Enemy).Count > 0 ? EStatus.Danger : EStatus.Safe);
            world.SetValue(DictionaryKeys.NearObject(animal.Enemy.ToString()), vision.GetSeenAnimal(animal.Enemy).Count > 0);

            return world;
        }

        public override bool IsChangedCriticalCondition()
        {
            var newStatus = vision.GetSeenAnimal(animal.Enemy).Count > 0 ||
                            world.GetValue<bool>(DictionaryKeys.NearObject(animal.Enemy.ToString())) ? EStatus.Danger : EStatus.Safe;
            return world.GetValue<EStatus>(DictionaryKeys.Status()) != newStatus;
        }
    }
}