using CodeDk;
using UnityEngine;

public class RestrictedMover : MonoBehaviour
{
    public Vector3Reference direction;
    public FloatReference velocity;
    public Rigidbody targetRigidbody;

    public Vector3Reference ballPosition;

    public Vector3ClampingFunction clampingFunction;

    // We need to explicitly reference the calculations that live in project-space.
    // Otherwise, Unity will not load them for us!
    public Vector3LinearFunction[] maxBallComponentCalculations;

    public void Update()
    {
        if (targetRigidbody == null)
            return;

        Vector3 newPos = targetRigidbody.position + velocity.Value * direction.Value * Time.deltaTime;

        ballPosition.Value = newPos;
    }

    public void OnEnable()
    {
        SubscribeToVariables();
    }

    public void OnDisable()
    {
        UnsubscribeFromVariables();
    }

    private void SubscribeToVariables()
    {
        UnsubscribeFromVariables();

        SubscribeTo(direction);
        SubscribeTo(velocity);

        SubscribeTo(clampingFunction);
    }


    private void UnsubscribeFromVariables()
    {
        UnsubscribeFrom(direction);
        UnsubscribeFrom(velocity);

        UnsubscribeFrom(clampingFunction);
    }

    private void SubscribeTo(VariableReference varRef)
    {
        varRef.SubscribeToEvents(HandleReferenceEvents);
    }

    private void SubscribeTo(Vector3ClampingFunction function)
    {
        if (function == null)
            return;

        UnsubscribeFrom(function);

        function.ValueBreachedRangeX += ReflectWithXNormal;
        function.ValueBreachedRangeY += ReflectWithYNormal;
        function.ValueBreachedRangeZ += ReflectWithZNormal;
    }

    private void UnsubscribeFrom(VariableReference varRef)
    {
        varRef.UnsubscribeFromEvents(HandleReferenceEvents);
    }

    private void UnsubscribeFrom(Vector3ClampingFunction function)
    {
        if (function == null)
            return;

        function.ValueBreachedRangeX -= ReflectWithXNormal;
        function.ValueBreachedRangeY -= ReflectWithYNormal;
        function.ValueBreachedRangeZ -= ReflectWithZNormal;
    }

    private void HandleReferenceEvents(object source, VariableReferenceEvent args)
    {
        if (ReferenceEquals(source, direction))
            NormalizeDirection();
    }

    private void NormalizeDirection()
    {
        direction.Value = direction.Value.normalized;
    }

    private void ReflectWithXNormal(object source, RangeBreachedEvent args)
    {
        if (source is ClampingFunction clamping &&
            clamping.WrapMode != RangeWrapMode.PingPong)
            return;

        Vector3 currentDirection = direction.Value;
        currentDirection.x = -currentDirection.x;
        direction.Value = currentDirection;
    }


    private void ReflectWithYNormal(object source, RangeBreachedEvent args)
    {
        if (source is ClampingFunction clamping &&
            clamping.WrapMode != RangeWrapMode.PingPong)
            return;

        Vector3 currentDirection = direction.Value;
        currentDirection.y = -currentDirection.y;
        direction.Value = currentDirection;
    }


    private void ReflectWithZNormal(object source, RangeBreachedEvent args)
    {
        if (source is ClampingFunction clamping &&
            clamping.WrapMode != RangeWrapMode.PingPong)
            return;

        Vector3 currentDirection = direction.Value;
        currentDirection.z = -currentDirection.z;
        direction.Value = currentDirection;
    }
}
