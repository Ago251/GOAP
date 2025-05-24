using System;
using System.Threading;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Ecosystem.AI.GOAP.Utility;
using Ecosystem.Enum;
using System.Linq;

namespace Ecosystem.AI.GOAP
{
    [RequireComponent(typeof(WorldStateGenerator), typeof(Planner))]
    public class Brain : MonoBehaviour
    {
        private CancellationTokenSource cancellationSource;
        private WorldStateGenerator worldStateGenerator;
        private Planner currentPlanner;
        [SerializeField]
        private Planner planner;

        private Goal goal;

        private void Awake()
        {
            worldStateGenerator = GetComponent<WorldStateGenerator>();
            currentPlanner = GetComponent<Planner>();
        }

        private void OnEnable()
        {
            cancellationSource = new CancellationTokenSource();
            BrainLoop(cancellationSource.Token).Forget();
        }

        private void OnDisable()
        {
            cancellationSource.Cancel();
        }

        private async UniTask BrainLoop(CancellationToken cancellationToken)
        {
            while (gameObject.activeInHierarchy)
            {
                if (cancellationToken.IsCancellationRequested) return;

                var currentState = worldStateGenerator.Construct();

                goal = new Goal();
                goal.Conditions.Add(DictionaryKeys.ChildTotal(), currentState.GetValue<int>(DictionaryKeys.ChildTotal()) + 1);

                if (planner.Plan(currentState, goal, new List<Action>(), out var path))
                {
                    foreach (var action in path)
                    {
                        var task = await UniTask.WhenAny(
                            action.Perform(gameObject, cancellationToken),
                            EvuluateCriticalConditions(action, cancellationToken));
                        if (!task.result1 || task.result2)
                            break;
                    }
                }

                await UniTask.NextFrame(cancellationToken);
            }
        }

        private async UniTask<bool> EvuluateCriticalConditions(Action action, CancellationToken cancellationToken)
        {
            var currentState = worldStateGenerator.Construct();
            do
            {
                cancellationToken.ThrowIfCancellationRequested();
                await UniTask.NextFrame(cancellationToken);
            } while (action.Preconditions.All(y => y.Evaluate(worldStateGenerator.Construct())));

            return true;
        }
    }
}