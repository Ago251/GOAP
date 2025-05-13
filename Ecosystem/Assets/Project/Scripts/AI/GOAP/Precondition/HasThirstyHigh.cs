using Ecosystem.AI.GOAP.Utility;
using Ecosystem.Enum;

namespace Ecosystem.AI.GOAP
{
    public class HasThirstyHigh : Precondition
    {
        private float minThirsty;

        public HasThirstyHigh(float minThirsty)
        {
            this.minThirsty = minThirsty;
        }

        public override bool Evaluate(WorldState worldState)
        {
            return worldState.GetValue<float>(DictionaryKeys.Thirsty()) > minThirsty;
        }
    }
}