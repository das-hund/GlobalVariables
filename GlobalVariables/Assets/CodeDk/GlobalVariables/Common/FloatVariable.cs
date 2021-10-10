using System;
using UnityEngine;

namespace CodeDk
{
    [Serializable, CreateAssetMenu(fileName = "FloatVariable", menuName = "Global Variables/Float", order = 250)]
    public class FloatVariable : GlobalVariable<float>
    { }
}
