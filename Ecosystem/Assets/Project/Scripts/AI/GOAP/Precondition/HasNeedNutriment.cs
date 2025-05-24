using Ecosystem.AI.GOAP.Utility;
using Ecosystem.Enum;

namespace Ecosystem.AI.GOAP
{
    public class HasNeedNutriment : Precondition
    {
        private ENutriment nutriment;

        public HasNeedNutriment(ENutriment nutriment)
        {
            this.nutriment = nutriment;
        }   

        public override bool Evaluate(WorldState worldState)
        {
            var key = nutriment == ENutriment.Water ? DictionaryKeys.Thirsty() : DictionaryKeys.Hunger();
            return worldState.GetValue<float>(key) < 70f;
        }
    }
}