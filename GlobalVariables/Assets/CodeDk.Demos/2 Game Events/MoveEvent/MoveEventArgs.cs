using System;
using UnityEngine;

[Serializable]
public class MoveEventArgs : EventArgs
{
    [SerializeField]
    private int _moverId;
    [SerializeField]
    private int _position;

    public MoveEventArgs(int id, int value)
    {
        _moverId = id;
        _position = value;
    }

    public int Position
    {
        get { return _position; }
        set { _position = value; }
    }

    public int MoverId
    {
        get { return _moverId; }
        set { _moverId = value; }
    }
}
