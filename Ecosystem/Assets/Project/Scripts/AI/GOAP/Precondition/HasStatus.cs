using Ecosystem.AI.GOAP.Utility;
using Ecosystem.Enum;

namespace Ecosystem.AI.GOAP
{
    public class HasStatus : Precondition
    {
        private EStatus status;

        public HasStatus(EStatus status)
        {
            this.status = status;
        }

        public override bool Evaluate(WorldState worldState)
        {
            return worldState.GetValue<EStatus>(DictionaryKeys.Status()) == status;
        }
    }
}