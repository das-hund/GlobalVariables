using System;
using UnityEngine;

namespace CodeDk
{
    [Serializable, CreateAssetMenu(fileName = "Vector2Variable", menuName = "Global Variables/Vector2", order = 250)]
    public class Vector2Variable : GlobalVariable<Vector2>
    { }
}
