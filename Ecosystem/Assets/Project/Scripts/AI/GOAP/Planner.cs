using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Ecosystem.AI.GOAP
{
    public class Planner : MonoBehaviour
    {
        [SerializeReference, SubclassSelector]
        public Action[] PossibleActions;
        
        public bool Plan(WorldState startState, Goal goal, List<Action> previousMoves, out List<Action> actionsPerformed)
        {
            actionsPerformed = new List<Action>();
            if (goal.IsVerified(startState))
            {
                return previousMoves.Count > 0;
            }

            var possibleMoves = PossibleActions
                                .Where(x => x.Preconditions.All(y => y.Evaluate(startState)))
                                .Where(x => !previousMoves.Contains(x))
                                .ToArray();

            if (!possibleMoves.Any())
            {
                return false;
            }
            
            var possiblePaths = new List<List<Action>>();

            foreach (var possibleMove in possibleMoves)
            {
                var mutatedState = startState.Clone();
                foreach (var effect in possibleMove.Effects)
                {
                    effect.Apply(ref mutatedState);
                }

                var previousMovesAlternative = new List<Action>();
                foreach (var previousMove in previousMoves)
                {
                    previousMovesAlternative.Add(previousMove);
                }

                previousMovesAlternative.Add(possibleMove);

                if (!Plan(mutatedState, goal, previousMovesAlternative, out var nextActionsPerformed))
                {
                    continue;
                }

                var newSequence = new List<Action> { possibleMove };
                newSequence.AddRange(nextActionsPerformed);
                possiblePaths.Add(newSequence);
            }

            if (possiblePaths.Count == 0)
            {
                return false;
            }
            
            actionsPerformed = possiblePaths[Random.Range(0, possiblePaths.Count)];
            return true;
        }
    }
}