using Ecosystem.AI.GOAP.Utility;

namespace Ecosystem.AI.GOAP
{
    public class DrinkEffect : Effect
    {
        public override void Apply(ref WorldState worldState)
        {
            worldState.SetValue(DictionaryKeys.Thirsty(),
                                worldState.GetValue<float>(DictionaryKeys.Thirsty()) + 70);
        }
    }
}