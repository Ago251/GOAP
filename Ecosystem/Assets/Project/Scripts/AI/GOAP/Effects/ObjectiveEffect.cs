using Ecosystem.AI.GOAP.Utility;

namespace Ecosystem.AI.GOAP
{
    public class ObjectiveEffect : Effect
    {
        private bool hasObjective;

        public ObjectiveEffect(bool hasObjectie)
        {
            this.hasObjective = hasObjectie;
        }

        public override void Apply(ref WorldState worldState)
        {
            worldState.SetValue(DictionaryKeys.HasObjective(), hasObjective);
        }
    }
}