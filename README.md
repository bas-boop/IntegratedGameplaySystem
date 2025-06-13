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
<<Enum>> ShapeType
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
class EventObserver
class ObserverEventType
<<Enum>> ObserverEventType

TheGame *-- PlayerManager
TheGame *-- EnemyManager
TheGame *-- EnemyUi
TheGame --> EventObserver

EnemyUi --> EventObserver

SpriteMaker *-- SpriteType
EventObserver *-- ObserverEventType

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

InputParser <|-- IGameobject

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
## Player
```mermaid
classDiagram

class IGameobject {
  +OnStart()
  +OnUpdate()
  +OnFixedUpdate()
}
<<interface>> IGameobject

class PlayerManager {
    -const string NAME
    -const string VISUAL
    -const string FIREPOINT
    -Rigidbody2D _rigidbody2D
    -BoxCollider2D _boxCollider2D
    -SpriteRenderer _spriteRenderer
    -InputParser _inputParser
    -PlayerMovement _playerMovement
    -Shooter _shooter
    -SphereTrigger _collider
    -Health _health
    -CameraFollower _cameraFollower
    -GameObject _thisGameObject
    -Bullet _bullet

    +OnStart()
    +OnUpdate()
    +OnFixedUpdate()

    -CreateComponents()
    -SetupComponents()
    -OnCol(GameObject other)
    -Die()
    -UpdateHealthUi()
}

class InputParser {
    -PlayerInput _playerInput
    -InputActionAsset _inputActionAsset
    -PlayerMovement _playerMovement
    -Shooter _shooter

    +OnStart()
    +OnUpdate()
    +OnFixedUpdate()
    +SetInputReferences(Dictionary~string, MonoBehaviour~ components)
    -GetReferences()
    -Init()
    -AddListeners()
    -RemoveListeners()
    -ShootAction(InputAction.CallbackContext context)
}

class PlayerMovement {
    -const float SPEED = 5f
    -const float ROTATION_SPEED = 5f
    -const float MAX_SPEED = 10f
    -Vector2 _input
    -Rigidbody2D _rigidbody2D
    -Transform _transform

    +SetInput(Vector2 input)
    +OnStart()
    +OnUpdate()
    +OnFixedUpdate()
}

class Shooter {
    -const string NAME = "Bullet"
    -const float SHOOT_INTERVAL = 0.5f
    -bool _isShooting
    -Bullet _bullet
    -Transform _firePoint
    -ObjectPool~Bullet~ _bulletPool

    +Init(Transform firePoint)
    +ActivateShoot()
    -Shoot()
    -SetIsShooting()
}

class SphereTrigger {
    +float radius = 0.5
    +IsColliding(Trigger other) (override)
    -OnDrawGizmos()
}

class Trigger {
    <<abstract>>
    +IsColliding(Trigger other)
}

class Health {
    +int CurrentHealth
    +int StartHealth
    -Action _onHeal
    -Action _onDamage
    -Action _onDie

    +Health(int startHealth)
    +AddHealListener(Action targetAction)
    +AddDamageListener(Action targetAction)
    +AddDieListener(Action targetAction)
    +Heal(int amount)
    +Damage(int amount)
    -Die()
}

class CameraFollower {
    -const float SMOOTH_SPEED = 5f
    -Transform _followTarget

    +OnStart()
    +OnUpdate()
    +OnFixedUpdate()
    +SetObjectToFollow(Transform followTarget)
}

class Bullet {
    -float _speed = 5
    -float _despawnTime = 20
    -SphereTrigger _collider
    -SpriteRenderer _spriteRenderer
    -Rigidbody2D _rb

    +ObjectPool~Bullet~ ObjectPool
    +string Name
    +Create(ObjectPool~Bullet~, string)
    +Delete()
    +Activate(Vector3, Quaternion)
    +Deactivate()
    +ReturnToPool()
    -OnCollide(GameObject)
}

class Tags{
  +const string PLAYER_TAG = "Player";
  +const string ENEMY_TAG = "Enemy";
  +const string BULLET_TAG = "Bullet";
}

class GameobjectComponentLibrary {
    -static Dictionary~string, GameObject~ _gameObjects

    +GameObject GetGameObject(string)
    +GameObject GetGameObject<T>()
    +GameObject[] GetGameObjects<T>()
    +T GetGameObjectComponent<T>()
    +T[] GetGameObjectComponents<T>()
    +bool RemoveGameobject(string)
    +T AddComponent<T>(string)
    +GameObject CreateGameObject(string)
    +void SetParent(string child, string parent)
    +GameObject AddCamera()
    +TMP_Text GetUiElement(string)
}

class SpriteMaker {
    <<static>>
    +void MakeSprite(SpriteRenderer spriteRenderer, ShapeType shapeType, Color color)
    -void DrawSquare(int, int, Texture2D, Color, Color)
    -void DrawTriangle(int, int, Texture2D, Color, Color)
    -void DrawCircle(int, int, Texture2D, Color, Color)
}

class EventObserver {
    <<static>>
    -static Dictionary~ObserverEventType, Action~ eventDict
    +void AddListener(ObserverEventType, Action)
    +void RemoveListener(ObserverEventType, Action)
    +void InvokeEvent(ObserverEventType)
}

class PlayerInput
<<UnityComponent>> PlayerInput

class InputActionAsset
<<UnityComponent>> InputActionAsset

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

InputParser <|-- IGameobject

InputParser *-- PlayerInput
InputParser *-- InputActionAsset
InputParser *-- PlayerMovement
InputParser *-- Shooter

SphereTrigger --|> Trigger
```
## Enemy with builder
```mermaid
classDiagram

class EnemyManager {
  -string _name
  -string _visual
  -Vector2 _startPosition
  -float _size
  -Rigidbody2D _rigidbody2D
  -BoxCollider2D _boxCollider2D
  -SpriteRenderer _spriteRenderer
  -GameObject _thisGameObject
  -BoxTrigger _collider
  -Health _health
  -FSM _fsm
  -List~State~ _states

  +void OnStart()
  +void OnUpdate()
  +void OnFixedUpdate()
  -void CreateComponents()
  -void SetupComponents()
  -void OnCol(GameObject other)
  -void Remove()
}

class EnemyBuilder {
  +SetName(string name) EnemyBuilder
  +SetStartPosition(Vector2 startPos) EnemyBuilder
  +SetSize(float size) EnemyBuilder
  +Build() EnemyManager
}

class TheGame {
  -PlayerManager _playerManager
  -EnemyManager _enemyManager
  -EnemyManager _enemyManager1
  -EnemyManager _enemyManager2
  -EnemyUi _enemyUi
  -List~IGameobject~ _gameobjects
  -bool _isStarting

  -void Update() : Entry-point
  -void FixedUpdate() : Entry-point
  -void OnStart()
  -void CreateObjects()
  -void AddObjects()
  -void UpdateCollision()
  -void AddEvents()
}

EnemyBuilder --> EnemyManager : builds
TheGame --> EnemyManager
```
## Objectpooling
```mermaid
classDiagram

class ObjectPool~T~ {
  -const int INITIAL_SIZE = 10
  -T _prefab
  -int _countPool
  -Queue~T~ _objectQueue
  -List~T~ _dequeuedObjects
  -Transform _parentTransform

  +ObjectPool(Transform parent, T prefab)
  +void OnStart()
  +void OnUpdate()
  +void OnFixedUpdate()
  +T GetObject(Vector3 position, Quaternion rotation, Transform targetParent)
  +List~T~ GetObjects(int amount, Vector3 position, Quaternion rotation, Transform targetParent)
  +void ReturnObject(T obj)
  +void ReturnObjects(List~T~ objs)
  +void ResetPool()

  -T CreateObject(Transform? targetParent)
  -void CreateObjects(int amount, Transform targetParent)
}

class IPoolable~T~ {
  +ObjectPoolT ObjectPool
  +string Name
  +void Create(ObjectPoolT objectPool, string name)
  +void Delete()
  +void Activate(Vector3 position, Quaternion rotation)
  +void Deactivate()
  +void ReturnToPool()
}
interface IPoolable~T~

class Shooter {
    -const string NAME = "Bullet"
    -const float SHOOT_INTERVAL = 0.5f
    -bool _isShooting
    -Bullet _bullet
    -Transform _firePoint
    -ObjectPool~Bullet~ _bulletPool

    +Init(Transform firePoint)
    +ActivateShoot()
    -Shoot()
    -SetIsShooting()
}

class Bullet {
    -float _speed = 5
    -float _despawnTime = 20
    -SphereTrigger _collider
    -SpriteRenderer _spriteRenderer
    -Rigidbody2D _rb

    +ObjectPool~Bullet~ ObjectPool
    +string Name
    +Create(ObjectPool~Bullet~, string)
    +Delete()
    +Activate(Vector3, Quaternion)
    +Deactivate()
    +ReturnToPool()
    -OnCollide(GameObject)
}

Bullet ..|> IPoolable~Bullet~
ObjectPool~Bullet~ <-- Shooter
Shooter --> Bullet
```
## Finite state machine - enemy
```mermaid
classDiagram

class FSM {
    -State _currentState
    +DictWrapper sharedData

    +FSM(IEnumerable~State~ states)
    +void UpdateState()
    +void FixedUpdateState()
    +void SwitchState(State targetState)
}

class State {
    +bool isInit
    +void Init(FSM, DictWrapper)
    #FSM p_owner
    #DictWrapper p_sharedData

    +void DoUpdate()
    +void DoFixedUpdate()
    +void DoEnter()
    +void DoExit()
}

class DictWrapper {
    +void Set(string key, object value)
    +T Get<T>(string key)
}

class EnemyManager {
  -string _name
  -string _visual
  -Vector2 _startPosition
  -float _size
  -Rigidbody2D _rigidbody2D
  -BoxCollider2D _boxCollider2D
  -SpriteRenderer _spriteRenderer
  -GameObject _thisGameObject
  -BoxTrigger _collider
  -Health _health
  -FSM _fsm
  -List~State~ _states

  +void OnStart()
  +void OnUpdate()
  +void OnFixedUpdate()
  -void CreateComponents()
  -void SetupComponents()
  -void OnCol(GameObject other)
  -void Remove()
}

class Idle {
  -Timer _idleTimer

  +void DoEnter()
  +void DoExit()
  +void DoUpdate()
  +void DoFixedUpdate()
  -void DoSomeThing()
}

class Wander {
  -Rigidbody2D _ourRb
  -Rect _wanderArea
  -Vector2 _positionToWanderTo

  +void DoEnter()
  +void DoExit()
  +void DoFixedUpdate()
  -void PickNewWanderPosition()
}

class Attack {
  -Rigidbody2D _ourRb
  -Transform _ourTransform
  -Transform _attackTransform
  -Timer _chaseTimer

  +void DoEnter()
  +void DoExit()
  +void DoUpdate()
  +void DoFixedUpdate()
  -void DoneChasing()
}

class Health {
    +int CurrentHealth
    +int StartHealth
    -Action _onHeal
    -Action _onDamage
    -Action _onDie

    +Health(int startHealth)
    +AddHealListener(Action targetAction)
    +AddDamageListener(Action targetAction)
    +AddDieListener(Action targetAction)
    +Heal(int amount)
    +Damage(int amount)
    -Die()
}

class IGameobject {
  +OnStart()
  +OnUpdate()
  +OnFixedUpdate()
}
<<interface>> IGameobject

class Timer {
  +Action OnTimerDone
  -float _duration
  -float _currentTime
  -bool _isRunning

  +Timer(float durationSeconds)
  +void SetTime(float seconds)
  +void Start()
  +void Stop()
  +void Reset()
  +void Tick(float deltaTime)
  +float GetCurrentTime()
  +bool IsDone()
}

class CoinFlip{
  +bool Flip()
}

FSM --> "1" State
FSM --> "1" DictWrapper
FSM --> "*" State

State --> FSM
State --> DictWrapper

EnemyManager --> FSM
EnemyManager --> "*" State
EnemyManager --> Health

Idle --|> State
Wander --|> State
Attack --|> State

class Rigidbody2D
<<UnityComponent>> Rigidbody2D

Idle --> Timer
Idle --> CoinFlip
Wander --> Rigidbody2D
Attack --> Timer
Attack --> Rigidbody2D

EnemyManager ..|> IGameobject
```
