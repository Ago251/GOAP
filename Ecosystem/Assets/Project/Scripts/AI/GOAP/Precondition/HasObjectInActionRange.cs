using Ecosystem.AI.GOAP.Utility;

namespace Ecosystem.AI.GOAP
{
    public class HasObjectInActionRange : Precondition
    {
        private string obj;

        public HasObjectInActionRange(string obj)
        {
            this.obj = obj;
        }

        public override bool Evaluate(WorldState worldState)
        {
            return worldState.GetValue<bool>(DictionaryKeys.InActionRange(obj));
        }
    }

}