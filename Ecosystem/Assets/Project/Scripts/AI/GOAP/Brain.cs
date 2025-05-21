using System;
using System.Threading;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Ecosystem.AI.GOAP.Utility;
using Ecosystem.Enum;

namespace Ecosystem.AI.GOAP
{
    [RequireComponent(typeof(WorldStateGenerator), typeof(Planner))]
    public class Brain : MonoBehaviour
    {
        private CancellationTokenSource cancellationSource;
        private WorldStateGenerator worldStateGenerator;
        private Planner currentPlanner;
        [SerializeField]
        private Planner safePlanner;
        [SerializeField]
        private Planner dangerPlanner;

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
                if(cancellationToken.IsCancellationRequested) return;
                
                var currentState = worldStateGenerator.Construct();
                var status = currentState.GetValue<EStatus>(DictionaryKeys.Status());

                switch (status)
                {
                    case EStatus.Danger:
                        goal = new Goal();
                        goal.Conditions.Add(DictionaryKeys.Status(), EStatus.Safe);
                        currentPlanner = dangerPlanner;
                        break;
                    case EStatus.Safe:
                        goal = new Goal();
                        goal.Conditions.Add(DictionaryKeys.ChildTotal(), currentState.GetValue<int>(DictionaryKeys.ChildTotal()) + 1);
                        currentPlanner = safePlanner;
                        break;
                }

                if (currentPlanner.Plan(currentState, goal, new List<Action>(), out var path))
                {
                    foreach (var action in path)
                    {
                        var task = await UniTask.WhenAny(
                            action.Perform(gameObject, cancellationToken),
                            EvuluateCriticalConditions(cancellationToken));
                        if (!task.result1 || task.result2)
                            break;
                    }
                }

                await UniTask.NextFrame(cancellationToken);
            }
        }

        private async UniTask<bool> EvuluateCriticalConditions(CancellationToken cancellationToken)
        {
            do
            {
                cancellationToken.ThrowIfCancellationRequested();
                await UniTask.NextFrame(cancellationToken);
            } while (!worldStateGenerator.IsChangedCriticalCondition() && gameObject.activeInHierarchy);

            return true;
        }
    }
}