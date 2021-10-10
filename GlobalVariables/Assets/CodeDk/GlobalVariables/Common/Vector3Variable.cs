using System;
using UnityEngine;

namespace CodeDk
{
    [Serializable, CreateAssetMenu(fileName = "Vector3Variable", menuName = "Global Variables/Vector3", order = 250)]
    public class Vector3Variable : GlobalVariable<Vector3>
    { }
}
