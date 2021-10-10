using UnityEngine;

public class MoveResponse : MonoBehaviour
{
    public Transform target;
    public Vector3 targetPos;
    public int moverId;

    public void Awake()
    {
        targetPos = target.position;
    }

    public void ApplyZPosition(object sender, MoveEventArgs eventArgs)
    {
        if (eventArgs.MoverId != moverId)
        {
            return;
        }

        targetPos.z = eventArgs.Position;
        target.position = targetPos;
    }
}
