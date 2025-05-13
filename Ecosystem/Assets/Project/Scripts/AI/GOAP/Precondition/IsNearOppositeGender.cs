using Ecosystem.AI.GOAP.Utility;

namespace Ecosystem.AI.GOAP
{
    public class IsNearOppositeGender : Precondition
    {
        public override bool Evaluate(WorldState worldState)
        {
            return worldState.GetValue<bool>(DictionaryKeys.IsNearOppositeGender());
        }
    }
}