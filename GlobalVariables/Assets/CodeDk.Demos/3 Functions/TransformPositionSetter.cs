using UnityEngine;

public class TransformPositionSetter : TransformSetter
{
    protected override Vector3 GetFromTransform()
    {
        return _target.position;
    }

    protected override void SetOnTransform(Vector3 newValue)
    {
        _target.position = newValue;
    }
}
