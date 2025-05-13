using Ecosystem.AI.GOAP.Utility;

namespace Ecosystem.AI.GOAP
{
    public class HasObjectNearPrecondition : Precondition
    {
        private string obj;
        private bool negate = false;

        public HasObjectNearPrecondition(string obj, bool negate = false)
        {
            this.obj = obj;
            this.negate = negate;
        }

        public override bool Evaluate(WorldState worldState)
        {
            return worldState.GetValue<bool>(DictionaryKeys.NearObject(obj)) ^ negate;
        }
    }
}