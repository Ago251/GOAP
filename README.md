# GOAP
In this simulation, AI agents (animals) behave autonomously using GOAP a planning system that allows them to select the best sequence of actions to reach their goals based on the current world state.

### 🐇 Rabbits
- **Goal:** Survive and reproduce.
- **Behavior:** Search for food (plants, water), avoid predators (foxes) and reproduce when conditions allow.

### 🦊 Foxes
- **Goal:** Survive and ensure the continuation of their lineage.
- **Behavior:** Hunt rabbits, manage hunger and reproduce.

## ⚙️ GOAP Actions

Each animal uses a shared set of actions implemented with the GOAP system. These include:

| Action                    | Description                                               |
|---------------------------|-----------------------------------------------------------|
| `Eat`                     | Consume nearby food to reduce hunger                      |
| `Drink`                   | Seek and consume water to satisfy thirst                  |
| `Search`                  | Wander the environment to discover resources              |
| `Flee`                    | Escape from nearby threats (e.g., foxes for rabbits)      |
| `Chase`                   | Pursue prey (e.g., foxes chasing rabbits)                 |
| `MoveTo`                  | Navigate toward a specific target                         |
| `Procreate`               | Attempt to reproduce with a nearby compatible partner     |
| `SearchOppositeGender`    | Look for a mating partner                                 |
| `MoveToOppositeGender`    | Move toward a suitable mate                               |

These modular actions allow for dynamic and context-sensitive behaviors, with animals reacting to their surroundings and internal needs.
