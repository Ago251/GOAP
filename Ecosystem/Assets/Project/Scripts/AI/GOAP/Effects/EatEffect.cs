using Ecosystem.AI.GOAP.Utility;
using Ecosystem.Enum;

namespace Ecosystem.AI.GOAP
{
    public class EatEffect : Effect
    {
        private ENutriment nutriment;

        public EatEffect(ENutriment nutriment)
        {
            this.nutriment = nutriment;
        }

        public override void Apply(ref WorldState worldState)
        {
            worldState.SetValue(DictionaryKeys.Hunger(), 
                                worldState.GetValue<float>(DictionaryKeys.Hunger()) + 70);
            worldState.SetValue(DictionaryKeys.InActionRange(nutriment.ToString()), false);
            worldState.SetValue(DictionaryKeys.NearObject(nutriment.ToString()), false);
        }
    }
}