using UnityEngine;
using Ecosystem.Animals.Stats;
using Ecosystem.Interfaces;
using Unity.VisualScripting.Antlr3.Runtime.Misc;

namespace Ecosystem.Items
{
    public class NutrimentItem : Item, IEateable

    {

        public Stat[] Consume()
        {
            InvokeOnConsumed();
            gameObject.SetActive(false);
            return stats;
        }
    }
}
