using Ecosystem.AI.GOAP.Utility;

namespace Ecosystem.AI.GOAP
{
    public class HasObjectNearEffect : Effect
    {
        private string obj;
        private bool negate = false;

        public HasObjectNearEffect(string obj, bool negate = false)
        {
            this.obj = obj;
            this.negate = negate;
        }

        public override void Apply(ref WorldState worldState)
        {
           worldState.SetValue(DictionaryKeys.NearObject(obj), true ^ negate);
        }
    }
}