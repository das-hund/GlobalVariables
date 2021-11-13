using System;
using CodeDk;
using UnityEngine;

[Serializable]
public class BallReflectionSystem : CodeDk.GlobalSystem
{
    public Vector3ClampingFunction clampingFunction;
    public Vector3Reference Direction;

    public override void RunOnce()
    { }

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

        SubscribeTo(clampingFunction);
    }

    private void UnsubscribeFromVariables()
    {
        UnsubscribeFrom(clampingFunction);
    }

    private void SubscribeTo(Vector3ClampingFunction function)
    {
        if (function == null)
        {
            return;
        }

        UnsubscribeFrom(function);

        function.ValueBreachedRangeX += ReflectWithXNormal;
        function.ValueBreachedRangeY += ReflectWithYNormal;
        function.ValueBreachedRangeZ += ReflectWithZNormal;
    }

    private void UnsubscribeFrom(Vector3ClampingFunction function)
    {
        if (function == null)
        {
            return;
        }

        function.ValueBreachedRangeX -= ReflectWithXNormal;
        function.ValueBreachedRangeY -= ReflectWithYNormal;
        function.ValueBreachedRangeZ -= ReflectWithZNormal;
    }

    private void ReflectWithXNormal(object source, RangeBreachedEvent args)
    {
        if (source is ClampingFunction clamping &&
            clamping.WrapMode != RangeWrapMode.PingPong)
        {
            return;
        }

        Vector3 currentDirection = Direction.Value;
        currentDirection.x = -currentDirection.x;
        Direction.Value = currentDirection;
    }

    private void ReflectWithYNormal(object source, RangeBreachedEvent args)
    {
        if (source is ClampingFunction clamping &&
            clamping.WrapMode != RangeWrapMode.PingPong)
        {
            return;
        }

        Vector3 currentDirection = Direction.Value;
        currentDirection.y = -currentDirection.y;
        Direction.Value = currentDirection;
    }

    private void ReflectWithZNormal(object source, RangeBreachedEvent args)
    {
        if (source is ClampingFunction clamping &&
            clamping.WrapMode != RangeWrapMode.PingPong)
        {
            return;
        }

        Vector3 currentDirection = Direction.Value;
        currentDirection.z = -currentDirection.z;
        Direction.Value = currentDirection;
    }
}
