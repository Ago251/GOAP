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

        private void Start()
        {
            BrainLoop().Forget();
        }

        private async UniTaskVoid BrainLoop()
        {
            while (true)
            {
                var currentState = worldStateGenerator.Construct();
                var status = currentState.GetValue<EStatus>(DictionaryKeys.Status());

                switch (status)
                {
                    case EStatus.Danger:
                        Debug.Log(gameObject.name + "Danger mode");
                        goal = new Goal();
                        goal.Conditions.Add(DictionaryKeys.Status(), EStatus.Safe);
                        currentPlanner = dangerPlanner;
                        break;
                    case EStatus.Safe:
                        Debug.Log(gameObject.name + "Safe mode");
                        goal = new Goal();
                        goal.Conditions.Add(DictionaryKeys.ChildTotal(), currentState.GetValue<int>(DictionaryKeys.ChildTotal()) + 1);
                        currentPlanner = safePlanner;
                        break;
                }

                if (currentPlanner.Plan(currentState, goal, new List<Action>(), out var path))
                {
                    foreach (var action in path)
                    {
                        var task = await UniTask.WhenAny(action.Perform(gameObject), EvuluateCriticalConditions());
                        if (!task.result1 || task.result2)
                            break;
                    }
                }

                await UniTask.NextFrame();
            }
        }

        private async UniTask<bool> EvuluateCriticalConditions()
        {
            do
            {
                await UniTask.NextFrame();
            } while (!worldStateGenerator.IsChangedCriticalCondition());

            return true;
        }
    }
}