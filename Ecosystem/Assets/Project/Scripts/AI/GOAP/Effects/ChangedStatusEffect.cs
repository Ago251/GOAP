using Ecosystem.AI.GOAP.Utility;
using Ecosystem.Enum;

namespace Ecosystem.AI.GOAP
{
    public class ChangedStatusEffect : Effect
    {
        private EStatus status;

        public ChangedStatusEffect(EStatus status)
        {
            this.status = status;
        }

        public override void Apply(ref WorldState worldState)
        {
            worldState.SetValue(DictionaryKeys.Status(), status);
        }
    }
}