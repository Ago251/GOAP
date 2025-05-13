using Ecosystem.Animals.Stats;
using Ecosystem.Interfaces;

namespace Ecosystem.Items
{
    public class LiquidItem : Item, IDrinkable
    {
        public Stat[] ConsumeLiquid()
        {
            return stats;
        }
    }
}
