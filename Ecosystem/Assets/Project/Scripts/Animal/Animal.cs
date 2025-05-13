using UnityEngine;
using Ecosystem.Animals.Stats;
using Ecosystem.Enum;
using Ecosystem.Interfaces;

namespace Ecosystem.Animals
{
    public abstract class Animal : MonoBehaviour, IEateable
    {
        public StatTracker StatTracker;

        public ENutriment Nutriment
        {
            get { return nutriment; }

            private set
            {
                nutriment = value;
            }
        }

        [SerializeField]
        private ENutriment nutriment;

        public ELiquid Liquid
        {
            get { return liquid; }

            private set
            {
                liquid = value;
            }
        }

        [SerializeField]
        private ELiquid liquid;

        public EGender Gender
        {
            get { return gender; }

            set
            {
                gender = value;
            }
        }

        public EAnimal Enemy
        {
            get
            {
                return enemy;
            }

            private set
            {
                enemy = value;
            }
        }
        [SerializeField]
        private EAnimal enemy;


        [SerializeField]
        private EGender gender;

        [SerializeField]
        private ProcreateManager procreateManager;


        public void Eat(IEateable eateable)
        {
            var stats = eateable.Consume();
            foreach (var stat in stats)
            {
                StatTracker[stat.Type].CurrentValue += stat.CurrentValue;
            }
        }

        public void Drink(IDrinkable drinkable)
        {
            var stats = drinkable.ConsumeLiquid();
            foreach (var stat in stats)
            {
                StatTracker[stat.Type].CurrentValue += stat.CurrentValue;
            }
        }

        public void Procreate(Animal animalA, Animal animalB) => procreateManager.Procreate(animalA, animalB);

        public Stat[] Consume()
        {
            gameObject.SetActive(false);
            return new Stat[] { new Stat(StatType.Hunger, 70) };
        }
    }
}
