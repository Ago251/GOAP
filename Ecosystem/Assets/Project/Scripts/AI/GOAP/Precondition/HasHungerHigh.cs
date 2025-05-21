using Ecosystem.AI.GOAP.Utility;

namespace Ecosystem.AI.GOAP
{
    public class HasHungerHigh : Precondition
    {
        private float minHunger;

        public HasHungerHigh(float minHunger)
        {
            this.minHunger = minHunger;
        }   

        public override bool Evaluate(WorldState worldState)
        {
            return worldState.GetValue<float>(DictionaryKeys.Hunger()) >= minHunger;
        }
    }
}