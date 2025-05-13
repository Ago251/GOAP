using Ecosystem.AI.GOAP.Utility;

namespace Ecosystem.AI.GOAP
{
    public class SeachOppositeGenderEffect : Effect
    {
        public override void Apply(ref WorldState worldState)
        {
            worldState.SetValue(DictionaryKeys.IsNearOppositeGender(), true);
        }
    }
}