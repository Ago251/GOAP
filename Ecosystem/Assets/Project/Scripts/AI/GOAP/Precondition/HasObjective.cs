using Ecosystem.AI.GOAP.Utility;

namespace Ecosystem.AI.GOAP
{
    public class HasObjective : Precondition
    {
        private bool negate;

        public HasObjective(bool negate = false)
        {
            this.negate = negate;
        }

        public override bool Evaluate(WorldState worldState)
        {
            return worldState.GetValue<bool>(DictionaryKeys.HasObjective()) ^ negate;
        }
    }
}