using UnityEngine;

public class AnimalBehaviour : StateRunner<AnimalBehaviour>
{
    public AnimalAnimations Animation { get; private set; }

    public Transform AnimalTransform { get; private set; }

    public Health Health { get; private set; }

    public MousePosition3D MousePosition { get; private set; }

    public bool IsInmune;

    private Rigidbody _rb;

    private Vector3 _lastDestination;

    protected override void Awake()
    {
        base.Awake();

        AnimalTransform = transform;
        Animation = new AnimalAnimations(GetComponentInChildren<Animator>(), transform);
        Health = GetComponent<Health>();
        MousePosition = GetComponent<MousePosition3D>();
        _rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        Health.OnTakeDamage += HandleTakeDamage;
        Health.OnDie += HandleDeath;

        if (MousePosition != null)
        {
            MousePosition.OnMouseClick += HandleMouseClick;
        }
    }

    private void OnDisable()
    {
        Health.OnTakeDamage -= HandleTakeDamage;
        Health.OnDie -= HandleDeath;

        if (MousePosition != null)
        {
            MousePosition.OnMouseClick -= HandleMouseClick;
        }
    }

    private void HandleMouseClick(Vector3 mousePosition)
    {
        var followState = ScriptableObject.CreateInstance<AnimalFollowState>();
        followState.SetTargetPosition(mousePosition);
        SetState(followState);
    }

    private void HandleTakeDamage(int damage, int health)
    {
        SetState(ScriptableObject.CreateInstance<AnimalHitState>());
    }

    private void HandleDeath()
    {
        SetState(ScriptableObject.CreateInstance<AnimalDeadState>());
    }

    public void LookAt(Vector3 lookAt)
    {
        // LERP this
        var lookTowards = new Vector3(lookAt.x, AnimalTransform.position.y, lookAt.z);
        AnimalTransform.LookAt(lookTowards);
    }

    public void Move(Vector3 destination, float speed)
    {
        AnimalTransform.position = Vector3.MoveTowards(AnimalTransform.position, destination, speed * Time.deltaTime);
        _lastDestination = destination;
    }

    public void DestroyWhenDead()
    {
        Destroy(this);
    }

    private void OnDrawGizmosSelected()
    {
        if (_lastDestination == null) return;

        Gizmos.color = Color.grey;
        Gizmos.DrawWireSphere(_lastDestination, .5f);
    }
}
