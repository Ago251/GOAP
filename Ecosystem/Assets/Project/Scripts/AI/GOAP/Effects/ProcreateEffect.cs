using Ecosystem.AI.GOAP.Utility;

namespace Ecosystem.AI.GOAP
{
    public class ProcreateEffect : Effect
    {
        public override void Apply(ref WorldState worldState)
        {
            worldState.SetValue(DictionaryKeys.Hunger(),
                                worldState.GetValue<float>(DictionaryKeys.Hunger()) - 50);
            worldState.SetValue(DictionaryKeys.Hunger(),
                                worldState.GetValue<float>(DictionaryKeys.Thirsty()) - 50);
            worldState.SetValue(DictionaryKeys.ChildTotal(),
                                worldState.GetValue<int>(DictionaryKeys.ChildTotal()) + 1);
        }
    }
}