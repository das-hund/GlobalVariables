using UnityEngine;

public class TransformScaleSetter : TransformSetter
{
    protected override Vector3 GetFromTransform()
    {
        return _target.localScale;
    }

    protected override void SetOnTransform(Vector3 newValue)
    {
        _target.localScale = newValue;
    }
}
