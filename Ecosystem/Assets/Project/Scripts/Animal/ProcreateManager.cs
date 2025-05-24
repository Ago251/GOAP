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

            foreach (var stat in stats)
            {
                animalA.StatTracker[stat].CurrentValue -= 50;
                child.StatTracker[stat].SetRandomValue();
            }

            var isMale = Random.Range(0, 1);
            child.Gender = isMale == 1 ? Enum.EGender.Male : Enum.EGender.Female;

            child.transform.position = animalA.transform.position;
        }
    }
}
