using CodeDk;
using UnityEngine;

public class RestrictedMover : MonoBehaviour
{
    public Vector3Reference direction;
    public FloatReference velocity;
    public Rigidbody targetRigidbody;

    public FloatVariable x_position;
    public FloatVariable y_position;
    public FloatVariable z_position;

    public ClampingFunction x_clamping;
    public ClampingFunction y_clamping;
    public ClampingFunction z_clamping;

    // We need to explicitly reference the calculations that live in project-space.
    // Otherwise, Unity will not load them for us!
    public LinearFunction[] maxBallComponentCalculations;

    public void Update()
    {
        if (targetRigidbody == null)
            return;

        Vector3 newPos = targetRigidbody.position + velocity.Value * direction.Value * Time.deltaTime;

        UpdatePositionReferences(newPos);
    }

    private void UpdatePositionReferences(Vector3 newPos)
    {
        if (x_position != null)
            x_position.Value = newPos.x;

        if (y_position != null)
            y_position.Value = newPos.y;

        if (z_position != null)
            z_position.Value = newPos.z;
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

        SubscribeTo(x_clamping);
        SubscribeTo(y_clamping);
        SubscribeTo(z_clamping);
    }


    private void UnsubscribeFromVariables()
    {
        UnsubscribeFrom(direction);
        UnsubscribeFrom(velocity);

        UnsubscribeFrom(x_clamping);
        UnsubscribeFrom(y_clamping);
        UnsubscribeFrom(z_clamping);
    }

    private void SubscribeTo(VariableReference varRef)
    {
        varRef.SubscribeToEvents(HandleReferenceEvents);
    }

    private void SubscribeTo(ClampingFunction function)
    {
        if (function == null)
            return;

        function.ValueBreachedRange -= HandleClampingEvents;
        function.ValueBreachedRange += HandleClampingEvents;
    }

    private void UnsubscribeFrom(VariableReference varRef)
    {
        varRef.UnsubscribeFromEvents(HandleReferenceEvents);
    }

    private void UnsubscribeFrom(ClampingFunction function)
    {
        if (function == null)
            return;

        function.ValueBreachedRange -= HandleClampingEvents;
    }

    private void HandleReferenceEvents(object source, VariableReferenceEvent args)
    {
        if (ReferenceEquals(source, direction))
            NormalizeDirection();
    }

    private void HandleClampingEvents(object source, RangeBreachedEvent args)
    {
        var clampingFunction = source as ClampingFunction;

        if (clampingFunction == null)
            return;

        if (clampingFunction == x_clamping && x_clamping.WrapMode == RangeWrapMode.PingPong)
        {
            ReflectWithXNormal();
        }
        else if (clampingFunction == y_clamping && y_clamping.WrapMode == RangeWrapMode.PingPong)
        {
            ReflectWithYNormal();
        }
        else if (clampingFunction == z_clamping && z_clamping.WrapMode == RangeWrapMode.PingPong)
        {
            ReflectWithZNormal();
        }
    }

    private void NormalizeDirection()
    {
        direction.Value = direction.Value.normalized;
    }

    private void ReflectWithXNormal()
    {
        Vector3 currentDirection = direction.Value;
        currentDirection.x = -currentDirection.x;
        direction.Value = currentDirection;
    }


    private void ReflectWithYNormal()
    {
        Vector3 currentDirection = direction.Value;
        currentDirection.y = -currentDirection.y;
        direction.Value = currentDirection;
    }


    private void ReflectWithZNormal()
    {
        Vector3 currentDirection = direction.Value;
        currentDirection.z = -currentDirection.z;
        direction.Value = currentDirection;
    }
}
