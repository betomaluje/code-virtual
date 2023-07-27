using UnityEngine;

[CreateAssetMenu(menuName = "Maskin/Animals/Follow", fileName = "New Follow State")]
public class AnimalFollowState : State<AnimalBehaviour>
{
    [SerializeField] private float arrivalDistance = 1f;
    [SerializeField] private float speed = 10f;

    private Vector3 _targetPosition;
    private AnimalAnimations _animation;

    public void SetTargetPosition(Vector3 targetPosition)
    {
        _targetPosition = targetPosition;
    }

    public override void Enter(AnimalBehaviour parent)
    {
        base.Enter(parent);
        if (_animation == null) _animation = parent.Animation;

        _animation.PlayWalk();
    }

    public override void Tick(float deltaTime)
    {
        _runner.LookAt(_targetPosition);
        _runner.Move(_targetPosition, speed);
    }

    public override void ChangeState()
    {
        float dist = Vector3.Distance(_targetPosition, _runner.AnimalTransform.position);
        if (dist < arrivalDistance)
        {
            _runner.SetState(ScriptableObject.CreateInstance<AnimalIdleState>());
        }
    }

    public override void FixedTick(float fixedDeltaTime) { }
}
