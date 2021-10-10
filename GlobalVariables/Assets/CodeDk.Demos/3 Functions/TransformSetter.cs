using System.Collections;
using CodeDk;
using UnityEngine;

public abstract class TransformSetter : MonoBehaviour
{
    protected Transform _target;

    public FloatReference xVar;
    public FloatReference yVar;
    public FloatReference zVar;

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

        xVar.SubscribeToEvents(HandleChanges);
        yVar.SubscribeToEvents(HandleChanges);
        zVar.SubscribeToEvents(HandleChanges);
    }

    private void UnsubscribeFromReferenceEvents()
    {
        xVar.UnsubscribeFromEvents(HandleChanges);
        yVar.UnsubscribeFromEvents(HandleChanges);
        zVar.UnsubscribeFromEvents(HandleChanges);
    }

    public void HandleChanges(object source, VariableReferenceEvent args)
    {
        if (args != VariableReferenceEvent.Empty)
            return;

        Vector3 toSet = GetFromTransform();
        Vector3 transformed = TransformVariable(new Vector3(xVar, yVar, zVar));

        if (ReferenceEquals(source, xVar))
        {
            toSet.x = transformed.x;
            _startTime = Time.time;
        }

        if (ReferenceEquals(source, yVar))
        {
            toSet.y = transformed.y;
            _startTime = Time.time;
        }

        if (ReferenceEquals(source, zVar))
        {
            toSet.z = transformed.z;
            _startTime = Time.time;
        }

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
        Vector3 toSet = GetFromTransform();
        Vector3 transformed = TransformVariable(new Vector3(xVar, yVar, zVar));

        while (Time.time < _startTime + lerpTime)
        {
            float fraction = (Time.time - _startTime) / lerpTime;

            toSet.x = Mathf.Lerp(toSet.x, transformed.x, fraction);
            toSet.y = Mathf.Lerp(toSet.y, transformed.y, fraction);
            toSet.z = Mathf.Lerp(toSet.z, transformed.z, fraction);

            SetOnTransform(toSet);

            yield return 0;
        }

        toSet = transformed;
        SetOnTransform(toSet);
    }

    private Vector3 TransformVariable(Vector3 value)
    {
        return Vector3.Scale(value, relativeBias) + constantBias;
    }


    protected abstract Vector3 GetFromTransform();
    protected abstract void SetOnTransform(Vector3 newValue);
}
