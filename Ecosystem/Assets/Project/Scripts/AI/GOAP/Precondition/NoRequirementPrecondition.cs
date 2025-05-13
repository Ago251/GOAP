namespace Ecosystem.AI.GOAP
{
    public class NoRequirementPrecondition : Precondition
    {
        public override bool Evaluate(WorldState worldState)
        {
            return true;
        }
    }
}