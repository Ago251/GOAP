using UnityEngine;
using Ecosystem.Animals.Stats;

namespace Ecosystem.Animals
{
    public class ProcreateManager : MonoBehaviour
    {
        [SerializeField]
        private Animal childPrefab;

        public void Procreate(Animal animalA, Animal animalB)
        {
            var child = Instantiate(childPrefab);

            StatType[] stats = (StatType[])System.Enum.GetValues(typeof(StatType));

            bool fromAnimalA = false;

            foreach (var stat in stats)
            {
                child.StatTracker[stat] = fromAnimalA ? animalA.StatTracker[stat] : 
                                                        animalB.StatTracker[stat];
                fromAnimalA = !fromAnimalA;
            }

            var isMale = Random.Range(0, 1);
            child.Gender = isMale == 1 ? Enum.EGender.Male : Enum.EGender.Female;

            child.transform.position = animalA.transform.position;
        }
    }
}
