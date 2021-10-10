using System.Collections;
using CodeDk;
using UnityEngine;

public abstract class TransformSetter : MonoBehaviour
{
    protected Transform _target;

    public Vector3Reference variableToSet;

    public Vector3 constantBias;
    public Vector3 relativeBias;

    public bool lerp = false;
    public float lerpTime = 1.0f;

    private float _startTime;

    public void OnEnable()
    {
        _target = transform;

        SubscribeToReferenceEvents();
    }

    public void OnDisable()
    {
        UnsubscribeFromReferenceEvents();
    }

    private void SubscribeToReferenceEvents()
    {
        UnsubscribeFromReferenceEvents();

        variableToSet.SubscribeToEvents(HandleChanges);
    }

    private void UnsubscribeFromReferenceEvents()
    {
        variableToSet.UnsubscribeFromEvents(HandleChanges);
    }

    public void HandleChanges(object source, VariableReferenceEvent args)
    {
        if (args != VariableReferenceEvent.Empty)
            return;

        Vector3 toSet = TransformVariable(variableToSet.Value);
        _startTime = Time.time;

        if (lerp)
        {
            StopAllCoroutines();
            StartCoroutine(InterpolateTowards());
        }
        else
        {
            SetOnTransform(toSet);
        }
    }

    public IEnumerator InterpolateTowards()
    {
        Vector3 currentValue = GetFromTransform();
        Vector3 transformed = TransformVariable(variableToSet.Value);

        while (Time.time < _startTime + lerpTime)
        {
            float fraction = (Time.time - _startTime) / lerpTime;

            currentValue.x = Mathf.Lerp(currentValue.x, transformed.x, fraction);
            currentValue.y = Mathf.Lerp(currentValue.y, transformed.y, fraction);
            currentValue.z = Mathf.Lerp(currentValue.z, transformed.z, fraction);

            SetOnTransform(currentValue);

            yield return 0;
        }

        currentValue = transformed;
        SetOnTransform(currentValue);
    }

    private Vector3 TransformVariable(Vector3 value)
    {
        return Vector3.Scale(value, relativeBias) + constantBias;
    }


    protected abstract Vector3 GetFromTransform();
    protected abstract void SetOnTransform(Vector3 newValue);
}
