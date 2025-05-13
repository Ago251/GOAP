using Ecosystem.AI.GOAP.Utility;
using Ecosystem.Enum;

namespace Ecosystem.AI.GOAP
{
    public class ObjectInActionRange : Effect
    {
        private string obj;

        public ObjectInActionRange(string obj)
        { 
            this.obj = obj; 
        }

        public override void Apply(ref WorldState worldState)
        {
            worldState.SetValue(DictionaryKeys.InActionRange(obj), true);
        }
    }
}