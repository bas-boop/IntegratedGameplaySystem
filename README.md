# Integrated Gameplay System
A HKU game project focused on working with design patterns. These 2 repos are practice for this project: [Domain Decomposition](https://github.com/bas-boop/DomainDecomposition) & [DesPat](https://github.com/bas-boop/DesPat).

# UML

A --> B     : solid arrow
A ..> B     : dashed arrow
A o-- B     : aggregation
A *-- B     : composition
A <|-- B    : inheritance
A <-- B     : reverse arrow

```mermaid
classDiagram

class State
class FSM
class DictWrapper
class SpriteMaker
class ShapeType
class PlayerMovement
class PlayerManager
class InputParser
class CameraFollower
class ObjectPool
interface IPoolable
class TheGame
class Tags
interface IGameobject
class GameobjectComponentLibrary
class Timer
class CoinFlip
class Shooter
class Bullet
class Health
class Wander
class Idle
class EnemyUi
class EnemyManager
class Attack
class Trigger
class SphereTrigger
class BoxTrigger
class ObserverEventType
class EventObserver

TheGame *-- PlayerManager
TheGame *-- EnemyManager
TheGame *-- EnemyUi
TheGame --> EventObserver

EnemyUi --> EventObserver

PlayerManager <|-- IGameobject
PlayerManager *-- InputParser
PlayerManager *-- PlayerMovement
PlayerManager *-- Shooter
PlayerManager *-- SphereTrigger
PlayerManager *-- Health
PlayerManager *-- CameraFollower
PlayerManager *-- Bullet
PlayerManager --> Tags
PlayerManager --> GameobjectComponentLibrary
PlayerManager --> SpriteMaker
PlayerManager --> EventObserver

PlayerMovement <|-- IGameobject

Bullet <|-- IPoolable
Bullet *-- SphereTrigger
Bullet --> Tags
Bullet --> GameobjectComponentLibrary
Bullet --> SpriteMaker

EnemyManager <|-- IGameobject
EnemyManager *-- BoxTrigger
EnemyManager *-- Health
EnemyManager *-- FSM
EnemyManager *-- State
EnemyManager --> SpriteMaker

Trigger <|-- SphereTrigger
Trigger <|-- BoxTrigger

FSM *-- DictWrapper
FSM --> State

Idle <|-- State
Wander <|-- State
Attack <|-- State

Idle --> CoinFlip
Idle *-- Timer
Attack *-- Timer

Shooter *-- Bullet
Shooter *-- ObjectPool
Shooter --> GameobjectComponentLibrary
Shooter --> SpriteMaker

```

